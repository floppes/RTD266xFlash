using System.Collections.Generic;
using System.ComponentModel;

namespace RTD266xFlash.BackgroundWorkers
{
    public abstract class BaseWorker
    {
        protected RTD266x _rtd;

        protected BackgroundWorker _backgroundWorker;

        public delegate void WorkerReportStatusEvent(string status);
        public event WorkerReportStatusEvent WorkerReportStatus;

        public BaseWorker(RTD266x rtd)
        {
            _rtd = rtd;

            _backgroundWorker = new BackgroundWorker();
            _backgroundWorker.DoWork += _backgroundWorker_DoWork;
            _backgroundWorker.RunWorkerCompleted += _backgroundWorker_RunWorkerCompleted;
        }

        public void Start()
        {
            _backgroundWorker.RunWorkerAsync();
        }

        protected abstract void _backgroundWorker_DoWork(object sender, DoWorkEventArgs e);

        protected abstract void _backgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e);

        protected void ReportStatus(string status)
        {
            WorkerReportStatus?.Invoke(status);
        }

        protected RTD266x.Result Read(int address, int length, out byte[] data, bool updateConsole)
        {
            data = null;
            List<byte> dataList = new List<byte>();
            int segmentLength;
            byte[] segment;
            int offset = 0;
            RTD266x.Result result;

            while (length - offset > 0)
            {
                if (length - offset >= 1024)
                {
                    segmentLength = 1024;
                }
                else
                {
                    segmentLength = length - offset;
                }

                result = _rtd.ReadSegment(address + offset, segmentLength, out segment);

                if (result != RTD266x.Result.Ok)
                {
                    return result;
                }

                dataList.AddRange(segment);

                offset += segmentLength;

                if (updateConsole)
                {
                    ReportStatus($"{offset} bytes read ({offset * 100 / length} %)\r\n");
                }
            }

            data = dataList.ToArray();

            return RTD266x.Result.Ok;
        }

        protected RTD266x.Result Write(int address, byte[] data, bool updateConsole)
        {
            int offset = 0;
            List<byte> segment = new List<byte>();
            RTD266x.Result result;

            while (data.Length - offset > 0)
            {
                if (address % 4096 == 0)
                {
                    if (updateConsole)
                    {
                        ReportStatus($"Erasing sector at address {address}... ");
                    }

                    result = _rtd.EraseSector(address);

                    if (result == RTD266x.Result.Ok)
                    {
                        if (updateConsole)
                        {
                            ReportStatus("done\r\n");
                        }
                    }
                    else
                    {
                        if (updateConsole)
                        {
                            ReportStatus(RTD266x.ResultToString(result) + "\r\n");
                        }

                        return result;
                    }
                }

                int length;

                if (data.Length - offset >= 256)
                {
                    length = 256;
                }
                else
                {
                    length = data.Length - offset;
                }

                if (updateConsole)
                {
                    ReportStatus($"Writing {length} bytes to address {address}... ");
                }

                segment.Clear();

                for (int i = 0; i < length; i++)
                {
                    segment.Add(data[offset + i]);
                }

                result = _rtd.WriteData(segment.ToArray(), address);

                offset += length;
                address += length;

                if (result == RTD266x.Result.Ok)
                {
                    if (updateConsole)
                    {
                        ReportStatus($"done ({offset * 100 / data.Length} %)\r\n");
                    }
                }
                else
                {
                    if (updateConsole)
                    {
                        ReportStatus(RTD266x.ResultToString(result) + "\r\n");
                    }

                    return result;
                }
            }

            return RTD266x.Result.Ok;
        }
    }
}
