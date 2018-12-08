#ifndef _RTD266X_H_
#define _RTD266X_H_

enum spi_cmd_t
{
  SPI_CMD_NOOP = 0,
  SPI_CMD_WRITE = 1,
  SPI_CMD_READ = 2,
  SPI_CMD_WRITE_AFTER_WREN = 3,
  SPI_CMD_WRITE_AFTER_EWSR = 4,
  SPI_CMD_ERASE = 5
};

struct flash_desc_t
{
  const char* device_name;
  uint32_t    jedec_id;
  uint32_t    size_kb;
  uint32_t    page_size;
  uint32_t    block_size_kb;
};

uint32_t spi_common_command(spi_cmd_t cmd, uint8_t cmd_code, uint8_t num_reads, uint8_t num_writes, uint32_t write_value);

flash_desc_t* find_chip(uint32_t jedec_id);

bool setup_chip_commands(uint32_t jedec_id);

bool read_flash(uint32_t start, uint32_t len, uint8_t* buf);

bool erase_chip(void);

void erase_sector(uint32_t address);

bool program_flash(uint32_t address, uint32_t len, uint8_t* data);

#endif
