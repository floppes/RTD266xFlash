class CRC():
	def __init__(self):
		self.crc = 0
		
	def process(self, data):
		for data_byte in data:
			self.crc ^= data_byte << 8
			
			for i in range(8):
				if (self.crc & 0x8000):
					self.crc ^= 0x1070 << 3
					
				self.crc <<= 1
			
	def get_crc(self):
		return (self.crc >> 8)