import sys
from rtd266x.rtd266x import RTD266x
from argparse import ArgumentParser
from argparse import RawTextHelpFormatter

def save_file(filename, data):
	file = open(filename, "wb")
	file.write(bytearray(data))
	file.close()
	
def load_file(filename):
	file = open(filename, "rb")
	data = file.read()
	file.close()
	
	return list(bytearray(data))

def cmp(a, b):
	return (a > b) - (a < b) 

parser = ArgumentParser(description = "A tool to read and write the flash of an RTD266x display driver IC via I2C", epilog='''Examples:

Read 512 KB to file out.bin:
rtd266x_flash -r 524288 -f out.bin

Read 1024 bytes to file out.bin using I2C interface 2:
rtd266x_flash -i 2 -r 1024 -f out.bin

Write all bytes from file in.bin:
rtd266x_flash -w -f in.bin

Write differences between base file base.bin and file with modifications mod.bin:
rtd266x_flash -d base.bin -f mod.bin

Write all bytes from file in.bin, starting at offset 2048:
rtd266x_flash -w -o 2048 -f in.bin

Erase the entire chip:
rtd266x_flash -c''', formatter_class = RawTextHelpFormatter)

parser.add_argument("-f", "--file", help = "File name", metavar = "file", required = False)
parser.add_argument("-i", "--interface", help = "Interface number (default = 2)", metavar = "x", type = int, default = 2)
parser.add_argument("-r", "--read", help = "Reads x bytes from the device to a file", metavar = "x", type = int)
parser.add_argument("-w", "--write", help = "Writes all bytes from a file to the device", action = "store_true")
parser.add_argument("-d", "--write_diff", help = "Writes all sectors from file which are different from file_base", metavar = "file_base")
parser.add_argument("-o", "--offset", help = "Read/write offset of x bytes", metavar = "x", type = int, default = 0)
parser.add_argument("-c", "--chip_erase", help = "Erases the entire chip", action = "store_true")
parser.add_argument("-s", "--no_reset", help = "Don't reset the device", action = "store_true")

args = parser.parse_args()

if ((args.read or args.write or args.write_diff) and args.file == None):
    print("File parameter is missing!")
    parser.print_help()
    sys.exit(1)

if (args.read == None and args.write_diff == None and args.write == False and args.chip_erase == False):
	parser.print_help()
	sys.exit(1)

rtd = RTD266x(args.interface, 0x4A)

if (not args.no_reset):
	print("Resetting device...")
	rtd.reset()

print("Entering ISP mode...")
	
if (not rtd.enter_isp_mode()):
	print("Error: cannot enter ISP mode!")
	sys.exit(2)

print("Reading chip data...")
	
jedec_id = rtd.read_jedec_id()
print("JEDEC ID: " + hex(jedec_id))

if (not rtd.setup_chip_commands(jedec_id)):
	print("Cannot setup chip commands! The flash chip id is unknown.")
	sys.exit(3)

print("ID: " + hex(rtd.read_id()))
print("Status: " + hex(rtd.read_status()))
	
print("Clearing lock bits...")
rtd.write_status(0)

print("Status: " + hex(rtd.read_status()))

if (args.chip_erase):
	print("Erasing chip...")
	rtd.erase_chip()

if (args.read != None):
	print("Reading " + str(args.read) +  " bytes from offset " + hex(args.offset) + " to file " + args.file + "...")
	
	bytes = []
	block_size = 1024
	
	for i in range(args.offset, args.offset + args.read, block_size):
		if (i + block_size > args.offset + args.read):
			read_len = args.offset + args.read - i
		else:
			read_len = block_size
	
		bytes += rtd.read_flash(i, read_len)
	
		if (bytes == None):
			print("Error: cannot read flash!")
			sys.exit(4)
		
	save_file(args.file, bytes)

if (args.write):
	print("Writing data from file " + args.file + " to offset " + str(args.offset) + "...")
	data = load_file(args.file)
	
	if (not rtd.program_flash(args.offset, data)):
		print("Error: cannot program flash!")

if (args.write_diff != None):
	print("Writing sectors from file " + args.file + " which differ from sectors of file " + args.write_diff + "...")
	data = load_file(args.file)
	data_base = load_file(args.write_diff)
	
	if (len(data) != len(data_base)):
		print("Error: file lengths are different but must be equal!")
		sys.exit(5)
		
	for i in range(0, len(data), rtd.SECTOR_SIZE):
		sector = data[i:i + rtd.SECTOR_SIZE]
		sector_base = data_base[i:i + rtd.SECTOR_SIZE]
		
		if (cmp(sector, sector_base) != 0):
			rtd.program_flash(i, sector)
			
if (not args.no_reset):
	print("Resetting device...")
	rtd.reset()