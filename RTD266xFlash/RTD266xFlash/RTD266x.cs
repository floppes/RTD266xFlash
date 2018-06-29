using System;
using System.Collections.Generic;
using System.IO.Ports;

namespace RTD266xFlash
{
    public class RTD266x
    {
        /// <summary>
        /// Command codes
        /// </summary>
        private enum RtdCommand : byte
        {
            Info = 0,
            Read = 1,
            EraseSector = 2,
            EraseChip = 3,
            Write = 4,
            WriteStatusLow = 5,
            WriteStatus = 6
        }

        /// <summary>
        /// Result codes
        /// </summary>
        private enum RtdResult : byte
        {
            Ok = 0,
            Error = 1
        }

        /// <summary>
        /// Operation result
        /// </summary>
        public enum Result
        {
            Ok,
            NotOk,
            NotConnected,
            Timeout,
            CrcError,
            UnexpectedCommand,
            SerialReadError,
            InvalidParameters
        }

        public class StatusInfo
        {
            public int ManufacturerId;
            public int DeviceId;
            public int JedecManufacturerId;
            public int JedecMemoryType;
            public int JedecCapacity;
            public int Status;
            public string Manufacturer;
            public string Capacity;
            public string Type;
        }

        private readonly SerialPort _comPort;

        public RTD266x(SerialPort comPort)
        {
            _comPort = comPort;
        }

        public static string ResultToString(Result result)
        {
            switch (result)
            {
                case Result.Ok:
                    return "ok";

                case Result.NotOk:
                    return "not ok";

                case Result.NotConnected:
                    return "not connected";

                case Result.CrcError:
                    return "CRC error";

                case Result.SerialReadError:
                    return "serial read error";

                case Result.Timeout:
                    return "timeout";

                case Result.UnexpectedCommand:
                    return "unexpected command";

                case Result.InvalidParameters:
                    return "invalid parameters";
            }

            return "unknown result";
        }

        private Result ReadComPort(int length, out byte[] data, int timeout = 1000)
        {
            data = new byte[length];

            if (_comPort == null || !_comPort.IsOpen)
            {
                return Result.NotConnected;
            }

            int dataRead = 0;

            long startTicks = DateTime.Now.Ticks;

            while (dataRead != length)
            {
                if (_comPort.BytesToRead > 0)
                {
                    int input = _comPort.ReadByte();

                    if (input == -1)
                    {
                        return Result.SerialReadError;
                    }

                    data[dataRead] = (byte)input;
                    dataRead++;
                }

                if ((DateTime.Now.Ticks - startTicks) / 10000 > timeout)
                {
                    return Result.Timeout;
                }
            }

            return Result.Ok;
        }

        private void WriteComPort(byte[] buffer)
        {
            if (_comPort != null && _comPort.IsOpen)
            {
                _comPort.DiscardInBuffer();
                _comPort.Write(buffer, 0, buffer.Length);
            }
        }

        private byte[] IntToBytes(int value)
        {
            byte[] bytes = new byte[] { (byte)((value >> 16) & 0xFF), (byte)((value >> 8) & 0xFF), (byte)(value & 0xFF) };

            return bytes;
        }

        /// <summary>
        /// Read segment
        /// </summary>
        /// <param name="address">Address</param>
        /// <param name="length">Length</param>
        /// <param name="data">Segment data</param>
        /// <returns>Result</returns>
        public Result ReadSegment(int address, int length, out byte[] data)
        {
            data = null;
            byte[] response;

            WriteComPort(new [] { (byte)RtdCommand.Read, (byte)((address >> 16) & 0xFF), (byte)((address >> 8) & 0xFF), (byte)(address & 0xFF), (byte)((length >> 16) & 0xFF), (byte)((length >> 8) & 0xFF), (byte)(length & 0xFF) });
            Result result = ReadComPort(2 + length, out response, 5000);

            if (result != Result.Ok)
            {
                return result;
            }

            if (response[0] != (byte)RtdCommand.Read)
            {
                return Result.UnexpectedCommand;
            }

            if (response[1] != (byte)RtdResult.Ok)
            {
                return Result.NotOk;
            }

            data = new byte[response.Length - 2];

            Array.Copy(response, 2, data, 0, data.Length);

            return Result.Ok;
        }

        /// <summary>
        /// Read status
        /// </summary>
        /// <param name="status">Status</param>
        /// <returns>Result</returns>
        public Result ReadStatus(out StatusInfo status)
        {
            status = new StatusInfo();
            byte[] response;

            WriteComPort(new [] { (byte)RtdCommand.Info });
            Result result = ReadComPort(9, out response);

            if (result != Result.Ok)
            {
                return result;
            }

            if (response[0] != (byte)RtdCommand.Info)
            {
                return Result.UnexpectedCommand;
            }

            if (response[1] != (byte)RtdResult.Ok)
            {
                return Result.NotOk;
            }

            status.ManufacturerId = response[2];
            status.DeviceId = response[3];
            status.JedecManufacturerId = response[4];
            status.JedecMemoryType = response[5];
            status.JedecCapacity = response[6];
            status.Status = (response[7] << 8) | response[8];

            switch (status.ManufacturerId)
            {
                case 0xC8:
                    status.Manufacturer = "Bright Moon Semiconductor Co., Ltd";
                    break;

                default:
                    status.Manufacturer = "Unknown";
                    break;
            }

            if (((status.ManufacturerId == 0xC8) || (status.ManufacturerId == 0x1C)) && (status.JedecCapacity == 0x13))
            {
                status.Capacity = "512 KB";
            }
            else
            {
                status.Capacity = "Unknown";
            }

            if (((status.ManufacturerId == 0xC8) || (status.ManufacturerId == 0x1C)) && (status.DeviceId == 0x12))
            {
                status.Type = "T25S40";
            }
            else
            {
                status.Type = "Unknown";
            }

            return Result.Ok;
        }

        /// <summary>
        /// Write status bytes
        /// </summary>
        /// <param name="statusHigh">Status byte high</param>
        /// <param name="statusLow">Status byte low</param>
        /// <returns>Result</returns>
        public Result WriteStatus(byte statusHigh, byte statusLow)
        {
            byte[] response;

            WriteComPort(new [] { (byte)RtdCommand.WriteStatus, statusHigh, statusLow });
            Result result = ReadComPort(2, out response);

            if (result != Result.Ok)
            {
                return result;
            }

            if (response[0] != (byte)RtdCommand.WriteStatus)
            {
                return Result.UnexpectedCommand;
            }

            if (response[1] != (byte)RtdResult.Ok)
            {
                return Result.NotOk;
            }

            return Result.Ok;
        }

        /// <summary>
        /// Erase sector containing the specified address
        /// </summary>
        /// <param name="address">Address</param>
        /// <returns>Result</returns>
        public Result EraseSector(int address)
        {
            byte[] addressBytes = IntToBytes(address);
            byte[] response;

            WriteComPort(new [] { (byte)RtdCommand.EraseSector, addressBytes[0], addressBytes[1], addressBytes[2] });
            Result result = ReadComPort(2, out response);

            if (result != Result.Ok)
            {
                return result;
            }

            if (response[0] != (byte)RtdCommand.EraseSector)
            {
                return Result.UnexpectedCommand;
            }

            if (response[1] != (byte)RtdResult.Ok)
            {
                return Result.NotOk;
            }

            return Result.Ok;
        }

        /// <summary>
        /// Erase the entire chip
        /// </summary>
        /// <returns>Result</returns>
        public Result EraseChip()
        {
            byte[] response;

            WriteComPort(new [] { (byte)RtdCommand.EraseChip, (byte)~RtdCommand.EraseChip });
            Result result = ReadComPort(2, out response, 5000);

            if (result != Result.Ok)
            {
                return result;
            }

            if (response[0] != (byte)RtdCommand.EraseChip)
            {
                return Result.UnexpectedCommand;
            }

            if (response[1] != (byte)RtdResult.Ok)
            {
                return Result.NotOk;
            }

            return Result.Ok;
        }

        /// <summary>
        /// Write data to address (256 bytes maximum)
        /// </summary>
        /// <param name="data">Data</param>
        /// <param name="address">Address</param>
        /// <returns>Result</returns>
        public Result WriteData(byte[] data, int address)
        {
            byte[] addressBytes = IntToBytes(address);
            byte[] response;
            List<byte> command = new List<byte>();

            if (data == null || data.Length == 0 || data.Length > 256 || address < 0)
            {
                return Result.InvalidParameters;
            }

            command.Add((byte)RtdCommand.Write);
            command.Add(addressBytes[0]);
            command.Add(addressBytes[1]);
            command.Add(addressBytes[2]);
            command.Add((byte)((data.Length >> 8) & 0xFF));
            command.Add((byte)((data.Length >> 0) & 0xFF));

            foreach (byte dataByte in data)
            {
                command.Add(dataByte);
            }

            WriteComPort(command.ToArray());
            Result result = ReadComPort(2, out response);

            if (result != Result.Ok)
            {
                return result;
            }

            if (response[0] != (byte)RtdCommand.Write)
            {
                return Result.UnexpectedCommand;
            }

            if (response[1] != (byte)RtdResult.Ok)
            {
                return Result.CrcError;
            }

            return Result.Ok;
        }

    }
}
