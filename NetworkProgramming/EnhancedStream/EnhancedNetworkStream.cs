namespace NetworkProgramming.EnhancedStream
{
    using System;
    using System.Net.Sockets;
    using System.Threading;

    public class EnhancedNetworkStream
    {
        private Thread listenerThread;

        private NetworkStream stream;

        private ListenerThreadArguments listenerThreadArguments;

        public event EventHandler<DataReceivedEventArgs> OnDataReceived;

        public EnhancedNetworkStream(NetworkStream networkStream)
        {
            this.Stream = networkStream;

            listenerThreadArguments = new ListenerThreadArguments(Stream);
        }

        public NetworkStream Stream
        {
            get
            {
                return this.stream;
            }

            private set
            {
                if (value == null)
                {
                    throw new ArgumentNullException(nameof(this.Stream), "The specific network stream must not be null!");
                }

                this.stream = value;
            }
        }

        // Listener worker, check if data available
        public void ListenerWorker(object data)
        {
            if (!(data is ListenerThreadArguments))
            {
                throw new ArgumentOutOfRangeException(nameof(data), $"The specified data must be an instance of the {nameof(ListenerThreadArguments)} class.");
            }

            ListenerThreadArguments args = (ListenerThreadArguments)data;

            DataReceivedEventArgs e;

            byte[] receivedDataBuffer = new byte[1024];
            int receivedBytes;

            while (!args.Exit)
            {
                if (!Stream.DataAvailable)
                {
                    Thread.Sleep(args.PollDelay);
                    continue;
                }

                if (Stream.CanRead)
                {
                    receivedBytes = Stream.Read(receivedDataBuffer, 0, receivedDataBuffer.Length);

                    e = new DataReceivedEventArgs(receivedDataBuffer);
                    this.FireDataReceived(this, e);
                }
            }

            Stream.Close();
        }

        public void StartListening()
        {
            if (this.listenerThread != null && this.listenerThread.IsAlive)
            {
                return;
            }

            this.listenerThread = new Thread(ListenerWorker);
            this.listenerThreadArguments.Exit = false;
            this.listenerThread.Start(this.listenerThreadArguments);
        }

        public void StopListening()
        {
            if (this.listenerThread == null || this.listenerThread.IsAlive)
            {
                return;
            }

            this.listenerThreadArguments.Exit = true;
            this.listenerThread.Join();
        }

        protected void FireDataReceived(object sender, DataReceivedEventArgs args)
        {
            if (OnDataReceived != null)
            {
                this.OnDataReceived(sender, args);
            }
        }
    }
}
