using System.ComponentModel;

namespace RTD266xFlash.BackgroundWorkers
{
    public class ReadWorker : BaseWorker
    {
        private readonly int _address;

        private readonly int _length;

        private readonly bool _updateConsole;

        public delegate void ReadWorkerFinishedEvent(RTD266x.Result result, byte[] data);
        public event ReadWorkerFinishedEvent ReadWorkerFinished;

        public ReadWorker(RTD266x rtd, int address, int length, bool updateConsole) : base(rtd)
        {
            _address = address;
            _length = length;
            _updateConsole = updateConsole;
        }

        protected override void _backgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            byte[] data;

            RTD266x.Result result = Read(_address, _length, out data, _updateConsole);

            e.Result = new object[] { result, data };
        }

        protected override void _backgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            object[] result = (object[])e.Result;

            ReadWorkerFinished?.Invoke((RTD266x.Result)result[0], (byte[])result[1]);
        }
    }
}
