// RTD266xArduino
// by floppes
// https://github.com/floppes/RTD266xFlash

#define TWI_FREQ  100000 //   5000     // original: 200000, only changed on AVR

#include "Wire.h"
#include "rtd266x.h"
#include "i2c.h"

#if defined(__AVR__)
  extern "C"
  {
    #include "utility/twi.h"  // from Wire library, so we can do bus scanning
  }
#endif

#define LED_ON  digitalWrite(LED_BUILTIN, HIGH)
#define LED_OFF digitalWrite(LED_BUILTIN, LOW)

typedef enum
{
  CMD_INFO = 0,
  CMD_READ = 1,
  CMD_ERASE_SECTOR = 2,
  CMD_ERASE_CHIP = 3,
  CMD_WRITE = 4,
  CMD_WRITE_STATUS_LOW = 5,
  CMD_WRITE_STATUS = 6
} cmd_t;

typedef enum
{
  RES_OK  = 0,
  RES_ERR = 1
} result_t;

static uint8_t req[270];
static uint16_t req_index;

static void error(void)
{
  while (1)
  {
    // flash LED endlessly
    LED_ON;
    delay(200);
    LED_OFF;
    delay(200);
  }
}

void setup(void) 
{
  uint8_t retries;
  uint8_t data;
  uint32_t jedec_id;
  flash_desc_t* chip;
  
  pinMode(LED_BUILTIN, OUTPUT);

  req_index = 0;
  
  while (!Serial);
  
  Serial.begin(115200);

  Wire.begin();

#if defined(__AVR__)
  if (twi_writeTo(0x4A, &data, 0, 1, 1))
  {
    // I2C slave not detected
    error();
  }
  
  TWBR = ((F_CPU / TWI_FREQ) - 16) / 2;
#endif

  retries = 0;
  
  while (1)
  {
    if (retries == 5)
    {
      error();    
    }

    retries++;

    // enter ISP mode
    i2c_write_reg(0x6F, 0x80);

    if ((i2c_read_reg(0x6F) & 0x80) == 0)
    {
      // ISP mode not entered, try again
      continue;
    }

    // identify EEPROM
    jedec_id = spi_common_command(SPI_CMD_READ, 0x9F, 3, 0, 0);

    chip = find_chip(jedec_id);

    if (chip == NULL)
    {
      error();
    }

    break;
  }

  if (!setup_chip_commands(chip->jedec_id))
  {
    error();
  }
}

void loop(void)
{
  while (Serial.available() > 0)
  {
    req[req_index] = Serial.read();
    req_index++;

    if (req_index == sizeof(req))
    {
      // avoid buffer overflow
      req_index = 0;
    }
  }

  if ((req_index > 0) && (req[0] == 0xFF))
  {
    // discard garbage data
    req_index = 0;
  }

  if ((req_index == 1) && (req[0] == CMD_INFO))
  {
    uint32_t id;
    uint32_t jedec_id;

    LED_ON;

    id = spi_common_command(SPI_CMD_READ, 0x90, 2, 3, 0);
    jedec_id = spi_common_command(SPI_CMD_READ, 0x9F, 3, 0, 0);

    Serial.write(CMD_INFO);
    Serial.write(RES_OK);
    Serial.write((id >> 8) & 0xFF); // manufacturer id
    Serial.write((id >> 0) & 0xFF); // device id
    Serial.write((jedec_id >> 16) & 0xFF); // manufacturer id
    Serial.write((jedec_id >>  8) & 0xFF); // memory type
    Serial.write((jedec_id >>  0) & 0xFF); // capacity
    Serial.write(spi_common_command(SPI_CMD_READ, 0x35, 1, 0, 0)); // status high
    Serial.write(spi_common_command(SPI_CMD_READ, 0x05, 1, 0, 0)); // status low

    LED_OFF;

    req_index = 0;
  }

  if ((req_index == 7) && (req[0] == CMD_READ))
  {
    uint32_t address = ((uint32_t)req[1] << 16) | ((uint32_t)req[2] << 8) | (uint32_t)req[3];
    uint32_t len = ((uint32_t)req[4] << 16) | ((uint32_t)req[5] << 8) | (uint32_t)req[6];

    LED_ON;

    Serial.write(CMD_READ);
    Serial.write(RES_OK);

    read_flash(address, len);

    LED_OFF;
    
    req_index = 0;
  }

  if ((req_index == 4) && (req[0] == CMD_ERASE_SECTOR))
  {
    uint32_t address = ((uint32_t)req[1] << 16) | ((uint32_t)req[2] << 8) | (uint32_t)req[3];

    LED_ON;

    erase_sector(address);

    Serial.write(CMD_ERASE_SECTOR);
    Serial.write(RES_OK);

    LED_OFF;

    req_index = 0;
  }

  if ((req_index == 2) && (req[0] == CMD_ERASE_CHIP) && (req[1] == 0xFC))
  {
    LED_ON;

    erase_chip();

    Serial.write(CMD_ERASE_CHIP);
    Serial.write(RES_OK);

    LED_OFF;

    req_index = 0;
  }

  if ((req_index > 6) && (req[0] == CMD_WRITE))
  {
    uint32_t address = ((uint32_t)req[1] << 16) | ((uint32_t)req[2] << 8) | (uint32_t)req[3];
    uint32_t len = ((uint32_t)req[4] << 8) | (uint32_t)req[5];

    if (req_index == len + 6)
    {
      LED_ON;
  
      if (program_flash(address, len, &req[6]))
      {
        Serial.write(CMD_WRITE);
        Serial.write(RES_OK);
      }
      else
      {
        Serial.write(CMD_WRITE);
        Serial.write(RES_ERR);
      }
  
      LED_OFF;
  
      req_index = 0;
    }
  }

  if ((req_index == 2) && (req[0] == CMD_WRITE_STATUS_LOW))
  {
    LED_ON;

    spi_common_command(SPI_CMD_WRITE_AFTER_WREN, 0x01, 0, 1, req[1]);

    Serial.write(CMD_WRITE_STATUS_LOW);
    Serial.write(RES_OK);

    LED_OFF;

    req_index = 0;
  }

  if ((req_index == 3) && (req[0] == CMD_WRITE_STATUS))
  {
    LED_ON;

    spi_common_command(SPI_CMD_WRITE_AFTER_WREN, 0x01, 0, 2, (req[1] << 8) | req[2]);

    Serial.write(CMD_WRITE_STATUS);
    Serial.write(RES_OK);

    LED_OFF;
    
    req_index = 0;
  }
}

