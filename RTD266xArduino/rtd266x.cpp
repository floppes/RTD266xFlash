#include "Arduino.h"
#include "rtd266x.h"
#include "crc.h"
#include "i2c.h"

// EEPROM types
// most are commented in order to save RAM
static const flash_desc_t flash_devices[] =
{
  // name,        JEDEC ID,    sizeK, page size, block sizeK
  // Manufacturer: Adesto/Atmel
  /*{"AT25DF041A" , 0x1F4401,      512,       256, 64},
  {"AT25DF161"  , 0x1F4602, 2 * 1024,       256, 64},
  {"AT26DF081A" , 0x1F4501, 1 * 1024,       256, 64},
  {"AT26DF0161" , 0x1F4600, 2 * 1024,       256, 64},
  {"AT26DF161A" , 0x1F4601, 2 * 1024,       256, 64},
  {"AT25DF321" ,  0x1F4701, 4 * 1024,       256, 64},
  {"AT25DF512B" , 0x1F6501,       64,       256, 32},
  {"AT25DF512B" , 0x1F6500,       64,       256, 32},
  {"AT25DF021"  , 0x1F3200,      256,       256, 64},
  {"AT26DF641" ,  0x1F4800, 8 * 1024,       256, 64},*/
  // Manufacturer: ST 
  /*{"M25P05"     , 0x202010,       64,       256, 32},
  {"M25P10"     , 0x202011,      128,       256, 32},
  {"M25P20"     , 0x202012,      256,       256, 64},
  {"M25P40"     , 0x202013,      512,       256, 64},
  {"M25P80"     , 0x202014, 1 * 1024,       256, 64},
  {"M25P16"     , 0x202015, 2 * 1024,       256, 64},
  {"M25P32"     , 0x202016, 4 * 1024,       256, 64},
  {"M25P64"     , 0x202017, 8 * 1024,       256, 64},*/
  // Manufacturer: Winbond 
  /*{"W25X10"     , 0xEF3011,      128,       256, 64},
  {"W25X20"     , 0xEF3012,      256,       256, 64},
  {"W25X40"     , 0xEF3013,      512,       256, 64},
  {"W25X80"     , 0xEF3014, 1 * 1024,       256, 64},*/
  // Manufacturer: Macronix 
  /*{"MX25L512"   , 0xC22010,       64,       256, 64},
  {"MX25L3205"  , 0xC22016, 4 * 1024,       256, 64},
  {"MX25L6405"  , 0xC22017, 8 * 1024,       256, 64},
  {"MX25L8005"  , 0xC22014,     1024,       256, 64},*/
  {"MX25L4005"  , 0xC22013,     1024,       256, 64},
  // Manufacturer: Microchip
  {"SST25VF512" , 0xBF4800,       64,       256, 32},
  {"SST25VF032" , 0xBF4A00, 4 * 1024,       256, 32},
  // Manufacturer: Bright Moon
  {"T25S40"     , 0xC84013,      512,       256, 32},
  // Manufacturer: Puya
  {"P25Q40H"    , 0x856013, 4 * 1024,       256, 32},
  // Unknown
  {"T25S40"     , 0x1C3113,      512,       256, 32},
  {"T25S40"     , 0x5E6013,      512,       256, 32},
  {NULL , 0, 0, 0, 0}
};

static uint8_t buffer[128];

static bool should_program_page(uint8_t* data, uint32_t size)
{
  for (uint32_t i = 0; i < size; i++)
  {
    if (data[i] != 0xFF)
    {
      return true;
    }
  }
  
  return false;
}

static uint8_t spi_compute_crc(uint32_t start, uint32_t end)
{
  uint8_t status;
  
  i2c_write_reg(0x64, (uint8_t)((start >> 16) & 0xFF));
  i2c_write_reg(0x65, (uint8_t)((start >>  8) & 0xFF));
  i2c_write_reg(0x66, (uint8_t)((start >>  0) & 0xFF));

  i2c_write_reg(0x72, (uint8_t)((end >> 16) & 0xFF));
  i2c_write_reg(0x73, (uint8_t)((end >>  8) & 0xFF));
  i2c_write_reg(0x74, (uint8_t)((end >>  0) & 0xFF));

  i2c_write_reg(0x6F, 0x84);
  
  do
  {
    status = i2c_read_reg(0x6F);
  } while (!(status & 0x02));
  
  return i2c_read_reg(0x75);
}

static void wait_write_done(void)
{
  uint8_t status;

  do
  {
    status = i2c_read_reg(0x6F);
  } while (status & 0x40);
}

static void spi_read(uint32_t address, uint8_t* data, int32_t len)
{
  uint8_t reg;
  
  i2c_write_reg(0x60, 0x46);
  i2c_write_reg(0x61, 0x03);
  i2c_write_reg(0x64, (address >> 16) & 0xFF);
  i2c_write_reg(0x65, (address >>  8) & 0xFF);
  i2c_write_reg(0x66, (address >>  0) & 0xFF);
  i2c_write_reg(0x60, 0x47); // execute the command

  do
  {
    reg = i2c_read_reg(0x60);
  } while (reg & 1);
  
  while (len > 0)
  {
    int32_t read_len = len;
    
    if (read_len > 32)
    {
      // max 32 bytes at a time
      read_len = 32;
    }
      
    i2c_read_bytes(0x70, data, (uint8_t)read_len);
    
    data += read_len;
    len -= read_len;
  }
}

uint32_t spi_common_command(spi_cmd_t cmd, uint8_t cmd_code, uint8_t num_reads, uint8_t num_writes, uint32_t write_value)
{
  uint8_t reg_value;
  uint8_t status;

  num_reads &= 0x03;
  num_writes &= 0x03;
  write_value &= 0xFFFFFF;
  reg_value = (cmd << 5) | (num_writes << 3) | (num_reads << 1);

  i2c_write_reg(0x60, reg_value);
  i2c_write_reg(0x61, cmd_code);
  
  switch (num_writes)
  {
    case 3:
      i2c_write_reg(0x64, (uint8_t)((write_value >> 16) & 0xFF));
      i2c_write_reg(0x65, (uint8_t)((write_value >>  8) & 0xFF));
      i2c_write_reg(0x66, (uint8_t)((write_value >>  0) & 0xFF));
      break;
      
    case 2:
      i2c_write_reg(0x64, (uint8_t)((write_value >> 8) & 0xFF));
      i2c_write_reg(0x65, (uint8_t)((write_value >> 0) & 0xFF));
      break;
      
    case 1:
      i2c_write_reg(0x64, (uint8_t)(write_value & 0xFF));
      break;
  }
  
  i2c_write_reg(0x60, reg_value | 0x01); // execute the command
  
  do
  {
    status = i2c_read_reg(0x60);
  } while (status & 0x1);
  
  switch (num_reads)
  {
    case 0:
      return 0;
      
    case 1:
      return (uint32_t)i2c_read_reg(0x67);
      
    case 2:
      return ((uint32_t)i2c_read_reg(0x67) << 8) | (uint32_t)i2c_read_reg(0x68);
      
    case 3:
      return ((uint32_t)i2c_read_reg(0x67) << 16) | ((uint32_t)i2c_read_reg(0x68) << 8) | (uint32_t)i2c_read_reg(0x69);
  }
  
  return 0;
}

flash_desc_t* find_chip(uint32_t jedec_id)
{
  flash_desc_t* chip = (flash_desc_t*)flash_devices;

  while (chip->jedec_id != 0)
  {
    if (chip->jedec_id == jedec_id)
    {
      return chip;
    }
    
    chip++;
  }

  return NULL;
}

bool setup_chip_commands(uint32_t jedec_id)
{
  uint8_t manufacturer_id = (jedec_id >> 16) & 0xFF;
  
  switch (manufacturer_id)
  {
    case 0xEF: // Winbond
    case 0xC8: // Bright Moon
    case 0xC2: // Macronix
    case 0x1C: // Unknown
    case 0x5E: // Unknown
    case 0x85: // STMicroelectronics
      i2c_write_reg(0x62, 0x06); // flash write enable op code
      i2c_write_reg(0x63, 0x50); // flash write enable for volatile status register op code
      i2c_write_reg(0x6A, 0x03); // flash read op code
      i2c_write_reg(0x6B, 0x0B); // flash fast read op code
      i2c_write_reg(0x6D, 0x02); // flash page program op code
      i2c_write_reg(0x6E, 0x05); // flash read status low op code
      return true;
  }

  return false;
}

bool read_flash(uint32_t start, uint32_t len, uint8_t* buf)
{
  uint32_t addr;
  uint32_t i;
  uint32_t read_len;
  uint32_t buf_offset;
  
  crc_init();

  addr = start;
  buf_offset = 0;

  do
  {
    read_len = start + len - addr;

    if (read_len > sizeof(buffer))
    {
      read_len = sizeof(buffer);
    }
    
    spi_read(addr, buffer, read_len);

    for (i = 0; i < read_len; i++)
    {
      buf[buf_offset] = buffer[i];
      buf_offset++;
    }

    crc_process(buffer, read_len);
    addr += read_len;
  } while (addr < start + len);

  uint8_t data_crc = crc_get();
  uint8_t chip_crc = spi_compute_crc(start, start + len - 1);

  return data_crc == chip_crc;
}

bool erase_chip(void)
{
  // unprotect the status register
  spi_common_command(SPI_CMD_WRITE_AFTER_EWSR, 0x01, 0, 1, 0); 

  // unprotect the flash
  spi_common_command(SPI_CMD_WRITE_AFTER_WREN, 0x01, 0, 1, 0); 

  // erase chip
  spi_common_command(SPI_CMD_ERASE, 0xC7, 0, 0, 0);

  return true;
}

void erase_sector(uint32_t address)
{
  // unprotect the status register
  spi_common_command(SPI_CMD_WRITE_AFTER_EWSR, 0x01, 0, 1, 0); 

  // unprotect the flash
  spi_common_command(SPI_CMD_WRITE_AFTER_WREN, 0x01, 0, 1, 0); 

  // erase sector
  spi_common_command(SPI_CMD_WRITE_AFTER_WREN, 0x20, 0, 3, address);

  wait_write_done();
}

bool program_flash(uint32_t address, uint32_t len, uint8_t* data)
{
  crc_init();

  // unprotect the status register
  spi_common_command(SPI_CMD_WRITE_AFTER_EWSR, 0x01, 0, 1, 0); 

  // unprotect the flash
  spi_common_command(SPI_CMD_WRITE_AFTER_WREN, 0x01, 0, 1, 0); 

  if (should_program_page(data, len))
  {
    // set program size - 1, 255 maximum
    i2c_write_reg(0x71, (len - 1) & 0xFF);
  
    // Set the programming address
    i2c_write_reg(0x64, (address >> 16) & 0xFF);
    i2c_write_reg(0x65, (address >>  8) & 0xFF);
    i2c_write_reg(0x66, (address >>  0) & 0xFF);
  
    // write the content to register 0x70
    // we can only write 16 bytes at a time
    for (uint32_t i = 0; i < len; i += 16)
    {
      if (len - i >= 16)
      {
        i2c_write_bytes(0x70, &data[i], 16);
      }
      else
      {
        i2c_write_bytes(0x70, &data[i], len - i);
      }
    }
  
    // start programming
    i2c_write_reg(0x6F, 0xA0);
  }
  
  crc_process(data, len);

  wait_write_done();

  uint8_t data_crc = crc_get();
  uint8_t chip_crc = spi_compute_crc(address, address + len - 1);
 
  return data_crc == chip_crc;
}
