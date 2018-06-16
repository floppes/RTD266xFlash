using System.ComponentModel;

namespace RTD266xFlash.BackgroundWorkers
{
    public class WriteWorker : BaseWorker
    {
        private readonly int _address;

        private readonly byte[] _data;

        private readonly bool _updateConsole;

        public delegate void WriteWorkerFinishedEvent(RTD266x.Result result);
        public event WriteWorkerFinishedEvent WriteWorkerFinished;

        public WriteWorker(RTD266x rtd, int address, byte[] data, bool updateConsole) : base(rtd)
        {
            _address = address;
            _data = data;
            _updateConsole = updateConsole;
        }

        protected override void _backgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            RTD266x.Result status = Write(_address, _data, _updateConsole);

            e.Result = status;
        }

        protected override void _backgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            WriteWorkerFinished?.Invoke((RTD266x.Result)e.Result);
        }
    }
}
