require 'rubyserial'

class Hash
  def lookup(val)
    select { |k,v| v == val }.first.first
  end
end

class RTD266xArduino
  CMD = {
    INFO: 0,
    READ: 1,
    ERASE_SECTOR: 2,
    ERASE_CHIP: 3,
    WRITE: 4,
    WRITE_STATUS_LOW: 5,
    WRITE_STATUS: 6,
    GET_ERROR: 7
  }

  RES = {
    OK: 0,
    ERR: 1,
    CHECKSUM: 2,
  }

  ERR = {
    NONE: 0,
    NO_SLAVE: 1,
    ISP: 2,
    CHIP_DETECT: 3,
    SETUP_CMDS: 4,
  }

  def initialize(port: '/dev/cu.usbmodem3201', baud: 115200)
    @s = Serial.new(port, baud)
    @read_buf = +"".b
  end

  def read_error_code
    write_command([CMD[:GET_ERROR]])
    resp = read_response(7, timeout: 1)
    raise "mismatched reply" if resp[0] != CMD[:GET_ERROR]
    raise "res #{resp[1]}" if resp[1] != RES[:OK]
    [ERR.lookup(resp[2]), (resp[3] << 24) | (resp[4] << 16) | (resp[5] << 8) | resp[6]]
  end

  def read_segment(address, length)
    write_command([CMD[:READ], (address >> 16) & 0xFF, (address >> 8) & 0xFF, address & 0xFF, (length >> 16) & 0xFF, (length >> 8) & 0xFF, length & 0xFF])
    resp = read_response(2 + length, timeout: 5)
    raise "mismatched reply" if resp[0] != CMD[:READ]
    raise "res #{resp[1]}" if resp[1] != RES[:OK]
    resp[2..-1].pack('C*')
  end

  # Erases the sector _containing_ address. Sectors are probably 4096/0x1000 bytes.
  def erase_sector_containing(address)
    write_command([CMD[:ERASE_SECTOR], (address >> 16) & 0xFF, (address >> 8) & 0xFF, address & 0xFF])
    resp = read_response(2, timeout: 5)
    raise "mismatched reply" if resp[0] != CMD[:ERASE_SECTOR]
    raise "res #{resp[1]}" if resp[1] != RES[:OK]
    nil
  end

  def write_data(address, data)
    if data.length > 256
      raise "looks like i2c can only deal with 256 bytes at a time"
    end

    write_command([
      CMD[:WRITE],
      (address >> 16) & 0xFF, (address >> 8) & 0xFF, address & 0xFF,
      (data.length >> 8) & 0xFF, data.length & 0xFF,
      *data.bytes,
    ])

    resp = read_response(2, timeout: 1)
    raise "mismatched reply" if resp[0] != CMD[:WRITE]
    raise "res #{resp[1]}" if resp[1] != RES[:OK]
    nil
  end

  private

  def write_command(cmd)
    checksum = cmd.inject(0, &:+) % 256
    bytes = (cmd + [checksum]).pack('C*').b
    @s.write(bytes)
  end

  def read_response(len, timeout:)
    len += 1 # checksum

    start = Time.now
    while @read_buf.length < len && Time.now - start < timeout
      buf = @s.read(len - @read_buf.length).b
      @read_buf << buf
      if @read_buf.length < len
        sleep 0.005
      end
    end

    if @read_buf.length >= len
      r = @read_buf.slice!(0, len).bytes
      if r[0...-1].inject(0, &:+) % 256 != r[-1]
        raise "checksum error #{r.inspect}"
      end
      r[0...-1]
    else
      raise "timeout, incomplete read (wanted #{len-1}+1, got #{@read_buf.inspect} (#{@read_buf.length} bytes) so far)"
    end
  end
end

arduino = RTD266xArduino.new
retries = 5
puts "connecting ..."
begin
  code, info = arduino.read_error_code
rescue => e
  if retries > 0
    retries -= 1
    puts "* got #{e}, retrying #{retries} more times ..."
    retry
  end
  raise
end

if code != :NONE
  puts "connecting got #{code} (#{info})"
  exit 1
end

puts "connected!"

cmd = ARGV.shift

def spinner(addr, top, start, code: nil, length: 80)
  xs = addr * length / top
  spaces = length - xs
  print "["
  print("X" * xs)
  print(" " * spaces)
  print "] #{code ? "#{code} " : ""}#{"/-\\|"[(Time.now - start).to_i % 4]} #{addr.to_s.reverse.scan(/\d{3}|.+/).join(",").reverse.rjust(9)} \r"
  STDOUT.flush
end

if cmd == "dump"
  puts "dumping 4MiB of flash in 512 byte increments"
  f = File.open("flash-contents.bin", "wb")
  addr = 0
  top = 4 * 1024 * 1024
  start = Time.now
  while addr < top
    spinner(addr, top, start)
    segment = arduino.read_segment(addr, 512)
    f.write(segment)
    addr += 512
  end
  spinner(addr, top, start)
  print "\n"
  f.close

elsif cmd == "write"
  file = ARGV.shift
  raise "specify a filename" unless file

  content = File.open(file, "rb", &:read)
  puts "writing #{content.length} bytes to flash in 256 byte increments"

  addr = 0
  start = Time.now
  while addr < content.length
    if addr % 4096 == 0
      spinner(addr, content.length, start, code: 'E')
      arduino.erase_sector_containing(addr)
    end
    spinner(addr, content.length, start, code: 'W')
    arduino.write_data(addr, content[addr...addr+256])
    addr += 256
  end
  spinner(addr, content.length, start)
  print "\n"

elsif cmd == "verify"
  file = ARGV.shift
  raise "specify a filename" unless file

  content = File.open(file, "rb", &:read)
  puts "verifying #{content.length} bytes in flash in 512 byte"

  addr = 0
  start = Time.now
  while addr < content.length
    spinner(addr, content.length, start)
    segment = arduino.read_segment(addr, 512)
    if segment != content[addr...addr+512]
      print "\n"
      puts "verify error in bytes #{addr}...#{addr+512}"
    end
    addr += 512
  end
  spinner(addr, content.length, start)
  print "\n"
end
