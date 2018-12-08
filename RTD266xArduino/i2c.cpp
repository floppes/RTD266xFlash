#include "Wire.h"
#include "i2c.h"

uint8_t i2c_read_reg(uint8_t reg)
{
  Wire.beginTransmission(RTD_I2CADDR);
  Wire.write(reg);
  Wire.endTransmission();
  Wire.requestFrom((uint8_t)RTD_I2CADDR, (uint8_t)1);
  
  return Wire.read();
}

void i2c_write_reg(uint8_t reg, uint8_t data)
{
  Wire.beginTransmission(RTD_I2CADDR);
  Wire.write(reg);
  Wire.write(data);
  Wire.endTransmission();
}

void i2c_read_bytes(uint8_t reg, uint8_t* data, uint8_t len)
{
  Wire.beginTransmission(RTD_I2CADDR);
  Wire.write(reg);
  Wire.endTransmission();

  Wire.requestFrom((uint8_t)RTD_I2CADDR, len);
  
  for (uint8_t i = 0; i < len; i++)
  {
    data[i] = Wire.read();
  }
}

void i2c_write_bytes(uint8_t reg, uint8_t* data, uint8_t len)
{
  Wire.beginTransmission(RTD_I2CADDR);
  Wire.write(reg);

  for (uint8_t i = 0; i < len; i++)
  {
    Wire.write(data[i]);
  }
  
  Wire.endTransmission();
}
