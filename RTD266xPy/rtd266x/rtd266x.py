from crc import CRC
from smbus import SMBus
import time

class RTD266x():
	SPI_CMD_NOOP = 0
	SPI_CMD_WRITE = 1
	SPI_CMD_READ = 2
	SPI_CMD_WRITE_AFTER_WREN = 3
	SPI_CMD_WRITE_AFTER_EWSR = 4
	SPI_CMD_ERASE = 5
	
	# time for register reading
	TIMEOUT = 0.1

	# flash sector size
	SECTOR_SIZE = 4096
	
	# segment size
	SEGMENT_SIZE = 256

	def __init__(self, interface, address):
		self.addr = address
		self.i2c = SMBus(interface)
		
	def _i2c_read_reg(self, reg):
		return self.i2c.read_byte_data(self.addr, reg)
		
	def _i2c_write_reg(self, reg, value):
		self.i2c.write_byte_data(self.addr, reg, value)
		
	def _i2c_read_bytes(self, reg, len):
		data = self.i2c.read_i2c_block_data(self.addr, reg)
		
		return data[0:len]
		
	def _i2c_write_bytes(self, reg, values):
		self.i2c.write_i2c_block_data(self.addr, reg, values)
		
	def _wait_for_reg_bit(self, reg, mask, expected):
		start = time.time()
	
		while (True):
			value = self._i2c_read_reg(reg)
			
			if ((value & mask) == expected):
				return True
				
			if (time.time() - start > self.TIMEOUT):
				print "Timeout when waiting for register " + hex(reg) + "!"
				return False
		
	def _wait_write_done(self):
		return self._wait_for_reg_bit(0x6F, 0x40, 0x00)

	def _should_program_page(self, data):
		for data_byte in data:
			if (data_byte != 0xFF):
				return True
				
		return False
		
	def spi_common_command(self, cmd, cmd_code, num_reads, num_writes, write_value):
		num_reads &= 0x03
		num_writes &= 0x03
		write_value &= 0xFFFFFF
		reg_value = (cmd << 5) | (num_writes << 3) | (num_reads << 1)
		
		self._i2c_write_reg(0x60, reg_value)
		self._i2c_write_reg(0x61, cmd_code)
		
		if (num_writes == 3):
			self._i2c_write_reg(0x64, (write_value >> 16) & 0xFF)
			self._i2c_write_reg(0x65, (write_value >>  8) & 0xFF)
			self._i2c_write_reg(0x66, (write_value >>  0) & 0xFF)
		
		if (num_writes == 2):
			self._i2c_write_reg(0x64, (write_value >>  8) & 0xFF)
			self._i2c_write_reg(0x65, (write_value >>  0) & 0xFF)
			
		if (num_writes == 1):
			self._i2c_write_reg(0x64, (write_value >>  0) & 0xFF)
			
		# execute the command
		self._i2c_write_reg(0x60, reg_value | 0x01);
			
		if (not self._wait_for_reg_bit(0x60, 0x01, 0x00)):
			return 0

		if (num_reads == 0):
			return 0
			
		if (num_reads == 1):
			return self._i2c_read_reg(0x67)
			
		if (num_reads == 2):
			return (self._i2c_read_reg(0x67) << 8) | self._i2c_read_reg(0x68)
			
		if (num_reads == 3):
			return (self._i2c_read_reg(0x67) << 16) | (self._i2c_read_reg(0x68) << 8) | self._i2c_read_reg(0x69)
			
		return 0
		
	def spi_compute_crc(self, start, end):
		self._i2c_write_reg(0x64, (start >> 16) & 0xFF)
		self._i2c_write_reg(0x65, (start >>  8) & 0xFF)
		self._i2c_write_reg(0x66, (start >>  0) & 0xFF)

		self._i2c_write_reg(0x72, (end >> 16) & 0xFF)
		self._i2c_write_reg(0x73, (end >>  8) & 0xFF)
		self._i2c_write_reg(0x74, (end >>  0) & 0xFF)
		
		self._i2c_write_reg(0x6F, 0x84)
		
		if (not self._wait_for_reg_bit(0x6F, 0x02, 0x02)):
			return 0

		return self._i2c_read_reg(0x75)
		
	def spi_read(self, address, len):
		self._i2c_write_reg(0x60, 0x46)
		self._i2c_write_reg(0x61, 0x03)
		self._i2c_write_reg(0x64, (address >> 16) & 0xFF)
		self._i2c_write_reg(0x65, (address >>  8) & 0xFF)
		self._i2c_write_reg(0x66, (address >>  0) & 0xFF)
		
		# execute the command
		self._i2c_write_reg(0x60, 0x47)
		
		if (not self._wait_for_reg_bit(0x60, 0x01, 0x00)):
			return []

		data = []
		
		while (len > 0):
			read_len = len
			
			if (read_len > 32):
				read_len = 32
				
			data += self._i2c_read_bytes(0x70, read_len)
			len -= read_len
			address += read_len
			
		return data
		
	def reset(self):
		self._i2c_write_reg(0x6F, 0x01)
		
		time.sleep(2)
		
		self._i2c_write_reg(0x6F, 0x00)
		
	def enter_isp_mode(self):
		self._i2c_write_reg(0x6F, 0x80)

		if (not self._wait_for_reg_bit(0x6F, 0x80, 0x80)):
			return False
		
		return True
	
	def read_id(self):
		return self.spi_common_command(self.SPI_CMD_READ, 0x90, 2, 3, 0)
	
	def read_jedec_id(self):
		return self.spi_common_command(self.SPI_CMD_READ, 0x9F, 3, 0, 0)
	
	def read_status(self):
		return (self.spi_common_command(self.SPI_CMD_READ, 0x35, 1, 0, 0) << 8) + (self.spi_common_command(self.SPI_CMD_READ, 0x05, 1, 0, 0))

	def write_status(self, status):
		self.spi_common_command(self.SPI_CMD_WRITE_AFTER_WREN, 0x01, 0, 2, status)
	
	def erase_chip(self):
		# unprotect the status register
		self.spi_common_command(self.SPI_CMD_WRITE_AFTER_EWSR, 0x01, 0, 1, 0)

		# unprotect the flash
		self.spi_common_command(self.SPI_CMD_WRITE_AFTER_WREN, 0x01, 0, 1, 0)

		# erase chip
		self.spi_common_command(self.SPI_CMD_ERASE, 0xC7, 0, 0, 0)

		return True
		
	def erase_sector(self, address):
		# unprotect the status register
		self.spi_common_command(self.SPI_CMD_WRITE_AFTER_EWSR, 0x01, 0, 1, 0)

		# unprotect the flash
		self.spi_common_command(self.SPI_CMD_WRITE_AFTER_WREN, 0x01, 0, 1, 0)

		# erase sector
		self.spi_common_command(self.SPI_CMD_WRITE_AFTER_WREN, 0x20, 0, 3, address)

		if (not self._wait_write_done()):
			return False
		
		return True
		
	def setup_chip_commands(self, jedec_id):
		manufacturer_id = (jedec_id >> 16) & 0xFF
		
		if (manufacturer_id == 0xEF or manufacturer_id == 0xC8 or manufacturer_id == 0xC2 or manufacturer_id == 0x1C or manufacturer_id == 0x5E):
			self._i2c_write_reg(0x62, 0x06) # flash write enable op code
			self._i2c_write_reg(0x63, 0x50) # flash write enable for volatile status register op code
			self._i2c_write_reg(0x6A, 0x03) # flash read op code
			self._i2c_write_reg(0x6B, 0x0B) # flash fast read op code
			self._i2c_write_reg(0x6D, 0x02) # flash page program op code
			self._i2c_write_reg(0x6E, 0x05) # flash read status low op code
			
			return True
			
		return False
	
	def read_flash(self, start, len):
		crc = CRC()
		
		print "Reading " + str(len) + " bytes from address " + hex(start)
		
		data = self.spi_read(start, len)
		
		crc.process(data)
		
		local_crc = crc.get_crc()
		dev_crc = self.spi_compute_crc(start, start + len - 1)
		
		if (local_crc != dev_crc):
			print "CRC validation failed! Expected " + hex(local_crc) + ", received " + hex(dev_crc)
			return None
			
		return data
	
	def program_flash(self, address, data):
		length = len(data)
		start_address = (address / self.SECTOR_SIZE) * self.SECTOR_SIZE
	
		if (address % self.SECTOR_SIZE != 0):
			# start at a sector address
			sector = self.read_flash(start_address, address - start_address)
			
			if (sector == None):
				return False
			
			data = sector + data
			
		if (len(data) % self.SECTOR_SIZE != 0):
			# end at a sector address
			sector = self.read_flash(address + length, self.SECTOR_SIZE - (len(data) % self.SECTOR_SIZE))
			
			if (sector == None):
				return False
			
			data = data + sector

		for i in range(0, len(data), self.SEGMENT_SIZE):
			address = start_address + i
			
			if (address % self.SECTOR_SIZE == 0):
				print "Erasing sector at address " + hex(address)
				
				if (not self.erase_sector(address)):
					print "Error erasing sector at address " + hex(address)
					return False
		
			segment_data = data[i:i + 256]
		
			print "Writing " + str(len(segment_data)) + " bytes to address " + hex(address)
			
			if (not self.write_segment(address, segment_data)):
				return False

		return True
	
	def write_segment(self, address, data):
		crc = CRC()
		length = len(data)
		
		# unprotect the status register
		self.spi_common_command(self.SPI_CMD_WRITE_AFTER_EWSR, 0x01, 0, 1, 0)

		# unprotect the flash
		self.spi_common_command(self.SPI_CMD_WRITE_AFTER_WREN, 0x01, 0, 1, 0)

		if (self._should_program_page(data)):
			# set program size - 1, 255 maximum
			self._i2c_write_reg(0x71, (length - 1) & 0xFF)

			# set the programming address
			self._i2c_write_reg(0x64, (address >> 16) & 0xFF)
			self._i2c_write_reg(0x65, (address >>  8) & 0xFF)
			self._i2c_write_reg(0x66, (address >>  0) & 0xFF)

			# write the content to register 0x70
			# we can only write 16 bytes at a time
			for i in range(0, length, 16):
				if (length - i >= 16):
					self._i2c_write_bytes(0x70, data[i:i + 16])
				else:
					self._i2c_write_bytes(0x70, data[i:i + length - i])
			
			# start programming
			self._i2c_write_reg(0x6F, 0xA0)
			
		crc.process(data)
		
		if (not self._wait_write_done()):
			return False
		
		local_crc = crc.get_crc()
		dev_crc = self.spi_compute_crc(address, address + length - 1)
		
		if (local_crc != dev_crc):
			print "CRC validation failed! Expected " + hex(local_crc) + ", received " + hex(dev_crc)
			return False
		
		return True
