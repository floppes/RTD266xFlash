#ifndef _CRC_H_
#define _CRC_H_

void crc_init(void);

void crc_process(const uint8_t* data, uint32_t len);

uint8_t crc_get(void);

#endif
