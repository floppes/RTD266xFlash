#include "Arduino.h"
#include "crc.h"

static unsigned crc = 0;

void crc_init(void)
{
  crc = 0;
}

void crc_process(const uint8_t* data, uint32_t len)
{
  int32_t i, j;
  
  for (j = len; j; j--, data++)
  {
    crc ^= (*data << 8);
    
    for (i = 8; i; i--)
    {
      if (crc & 0x8000)
      {
        crc ^= (0x1070 << 3);
      }
      
      crc <<= 1;
    }
  }
}

uint8_t crc_get(void)
{
  return (uint8_t)(crc >> 8);
}
