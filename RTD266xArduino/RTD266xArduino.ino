// RTD266xArduino
// by floppes
// https://github.com/floppes/RTD266xFlash

#include "Wire.h"
#include "rtd266x.h"
#include "i2c.h"

#define BAUD_RATE 115200

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
  CMD_WRITE_STATUS = 6,
  CMD_GET_ERROR = 7
} cmd_t;

typedef enum
{
  RES_OK  = 0,
  RES_ERR = 1,
  RES_CHECKSUM = 2
} result_t;

typedef enum
{
  ERR_NONE = 0,
  ERR_NO_SLAVE = 1,
  ERR_ISP = 2,
  ERR_CHIP_DETECT = 3,
  ERR_SETUP_CMDS = 4
} error_t;

static uint8_t req[270];
static uint8_t res[270];
static uint16_t req_index;
static uint32_t last_data_received;
static error_t error;
static uint32_t error_info;
static uint32_t last_error_flash;
static uint32_t error_led_flash_time;
static bool error_led_on;
static uint8_t error_led_flashes;

static void send_res(uint16_t len)
{
  uint16_t i;
  uint8_t checksum;

  checksum = 0;

  for (i = 0; i < len; i++)
  {
    Serial.write(res[i]);

    checksum += res[i];
  }

  Serial.write(checksum);

  Serial.flush();
}

static bool verify_checksum(uint16_t len)
{
  uint16_t i;
  uint8_t checksum;

  checksum = 0;

  for (i = 0; i < len - 1; i++)
  {
    checksum += req[i];
  }

  return checksum == req[len - 1];
}

void setup(void) 
{
  uint8_t retries;
  uint32_t jedec_id;
  flash_desc_t* chip;
  
  pinMode(LED_BUILTIN, OUTPUT);

  req_index = 0;
  last_data_received = 0;
  error = ERR_NONE;
  error_info = 0;
  last_error_flash = 0;
  error_led_flash_time = 200;
  error_led_on = false;
  error_led_flashes = 0;
  
  Serial.begin(BAUD_RATE);

  while (!Serial);

  LED_ON;

  Wire.begin();

  // try to speak with I2C slave
  Wire.beginTransmission(RTD_I2CADDR);

  // the following call may hang indefinitely if the I2C slave is off, therefore we keep the LED on
  if (Wire.endTransmission())
  {
    // I2C slave not detected
    LED_OFF;
    
    error = ERR_NO_SLAVE;
    return;
  }

  LED_OFF;

  retries = 0;
  
  while (true)
  {
    if (retries == 5)
    {
      error = ERR_ISP;
      return;   
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
      error = ERR_CHIP_DETECT;
      error_info = jedec_id;
      return;
    }

    break;
  }

  if (!setup_chip_commands(chip->jedec_id))
  {
    error = ERR_SETUP_CMDS;
    error_info = jedec_id;
    return;
  }
}

void loop(void)
{
  while (Serial.available() > 0)
  {
    last_data_received = millis();
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

  if (millis() - last_data_received > 100)
  {
    // no new data received, reset buffer
    req_index = 0;
  }

  if ((req_index == 2) && (req[0] == CMD_GET_ERROR))
  {
    LED_ON;

    if (!verify_checksum(req_index))
    {
      res[0] = CMD_GET_ERROR;
      res[1] = RES_CHECKSUM;

      send_res(2);
    }
    else
    {
      res[0] = CMD_GET_ERROR;
      res[1] = RES_OK;
      res[2] = error;
      res[3] = (error_info >> 24) & 0xFF;
      res[4] = (error_info >> 16) & 0xFF;
      res[5] = (error_info >>  8) & 0xFF;
      res[6] = (error_info >>  0) & 0xFF;
      
      send_res(7);
    }
    
    req_index = 0;

    LED_OFF;
  }

  if (error != ERR_NONE)
  {
    if (millis() - last_error_flash > error_led_flash_time)
    {
      // flash LED to indicate an error
      last_error_flash = millis();

      error_led_flash_time = 200;
      
      if (error_led_on)
      {
        LED_ON;
      }
      else
      {
        LED_OFF;

        error_led_flashes++;

        if (error_led_flashes == 2)
        {
          error_led_flash_time = 800;
          error_led_flashes = 0;
        }
      }

      error_led_on = !error_led_on;
    }

    // ignore other commands because we are in error mode
    return;
  }

  if ((req_index == 2) && (req[0] == CMD_INFO))
  {
    LED_ON;
     
    if (!verify_checksum(req_index))
    {
      res[0] = CMD_INFO;
      res[1] = RES_CHECKSUM;

      send_res(2);
    }
    else
    {
      uint32_t id;
      uint32_t jedec_id;
  
      id = spi_common_command(SPI_CMD_READ, 0x90, 2, 3, 0);
      jedec_id = spi_common_command(SPI_CMD_READ, 0x9F, 3, 0, 0);
  
      res[0] = CMD_INFO;
      res[1] = RES_OK;
      res[2] = (id >> 8) & 0xFF; // manufacturer id
      res[3] = (id >> 0) & 0xFF; // device id
      res[4] = (jedec_id >> 16) & 0xFF; // manufacturer id
      res[5] = (jedec_id >>  8) & 0xFF; // memory type
      res[6] = (jedec_id >>  0) & 0xFF; // capacity
      res[7] = spi_common_command(SPI_CMD_READ, 0x35, 1, 0, 0); // status high
      res[8] = spi_common_command(SPI_CMD_READ, 0x05, 1, 0, 0); // status low
  
      send_res(9);
    }
    
    req_index = 0;

    LED_OFF;
  }

  if ((req_index == 8) && (req[0] == CMD_READ))
  {
    LED_ON;

    if (!verify_checksum(req_index))
    {
      res[0] = CMD_READ;
      res[1] = RES_CHECKSUM;

      send_res(2);
    }
    else
    {
      uint32_t address = ((uint32_t)req[1] << 16) | ((uint32_t)req[2] << 8) | (uint32_t)req[3];
      uint32_t len = ((uint32_t)req[4] << 16) | ((uint32_t)req[5] << 8) | (uint32_t)req[6];

      if (len > sizeof(res) - 3)
      {
        res[0] = CMD_READ;
        res[1] = RES_ERR;
  
        send_res(2);
      }
      else
      {
        res[0] = CMD_READ;
        res[1] = RES_OK;
  
        if (read_flash(address, len, &res[2]))
        {
          send_res(len + 2);
        }
        else
        {
          res[1] = RES_ERR;
  
          send_res(2);
        }
      }
    }
    
    req_index = 0;
      
    LED_OFF;
  }

  if ((req_index == 5) && (req[0] == CMD_ERASE_SECTOR))
  {
    LED_ON;

    if (!verify_checksum(req_index))
    {
      res[0] = CMD_ERASE_SECTOR;
      res[1] = RES_CHECKSUM;

      send_res(2);
    }
    else
    {
      uint32_t address = ((uint32_t)req[1] << 16) | ((uint32_t)req[2] << 8) | (uint32_t)req[3];

      erase_sector(address);
  
      res[0] = CMD_ERASE_SECTOR;
      res[1] = RES_OK;
  
      send_res(2);
    }
    
    req_index = 0;

    LED_OFF;
  }

  if ((req_index == 3) && (req[0] == CMD_ERASE_CHIP) && (req[1] == 0xFC))
  {
    LED_ON;

    if (!verify_checksum(req_index))
    {
      res[0] = CMD_ERASE_CHIP;
      res[1] = RES_CHECKSUM;

      send_res(2);
    }
    else
    {
      erase_chip();
  
      res[0] = CMD_ERASE_CHIP;
      res[1] = RES_OK;
  
      send_res(2);
    }

    req_index = 0;

    LED_OFF;
  }

  if ((req_index > 6) && (req[0] == CMD_WRITE))
  {
    uint32_t address = ((uint32_t)req[1] << 16) | ((uint32_t)req[2] << 8) | (uint32_t)req[3];
    uint32_t len = ((uint32_t)req[4] << 8) | (uint32_t)req[5];

    if (req_index == len + 7)
    {
      LED_ON;
      
      if (!verify_checksum(req_index))
      {
        res[0] = CMD_WRITE;
        res[1] = RES_CHECKSUM;

        send_res(2);
      }
      else
      {
        res[0] = CMD_WRITE;
    
        if (program_flash(address, len, &req[6]))
        {
          res[1] = RES_OK;
        }
        else
        {
          res[1] = RES_ERR;
        }
  
        send_res(2);
      }

      req_index = 0;

      LED_OFF;
    }
  }

  if ((req_index == 4) && (req[0] == CMD_WRITE_STATUS_LOW))
  {
    LED_ON;

    if (!verify_checksum(req_index))
    {
      res[0] = CMD_WRITE_STATUS_LOW;
      res[1] = RES_CHECKSUM;

      send_res(2);
    }
    else
    {
      spi_common_command(SPI_CMD_WRITE_AFTER_WREN, 0x01, 0, 1, req[1]);
  
      res[0] = CMD_WRITE_STATUS_LOW;
      res[1] = RES_OK;
  
      send_res(2);
    }

    req_index = 0;

    LED_OFF;
  }

  if ((req_index == 4) && (req[0] == CMD_WRITE_STATUS))
  {
    LED_ON;

    if (!verify_checksum(req_index))
    {
      res[0] = CMD_WRITE_STATUS;
      res[1] = RES_CHECKSUM;

      send_res(2);
    }
    else
    {
      spi_common_command(SPI_CMD_WRITE_AFTER_WREN, 0x01, 0, 2, (req[1] << 8) | req[2]);
  
      res[0] = CMD_WRITE_STATUS;
      res[1] = RES_OK;
  
      send_res(2);
    }

    req_index = 0;

    LED_OFF;
  }
}

