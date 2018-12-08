#ifndef _I2C_H_
#define _I2C_H_

#define RTD_I2CADDR 0x4A

uint8_t i2c_read_reg(uint8_t reg);

void i2c_write_reg(uint8_t reg, uint8_t data);

void i2c_read_bytes(uint8_t reg, uint8_t* data, uint8_t len);

void i2c_write_bytes(uint8_t reg, uint8_t* data, uint8_t len);

#endif
