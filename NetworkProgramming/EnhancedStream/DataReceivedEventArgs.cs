namespace NetworkProgramming
{
    using System;

    public class DataReceivedEventArgs : EventArgs
    {
        public DataReceivedEventArgs(byte[] data)
        {
            this.Data = data;
        }

        public byte[] Data
        {
            get;
            private set;
        }
    }
}