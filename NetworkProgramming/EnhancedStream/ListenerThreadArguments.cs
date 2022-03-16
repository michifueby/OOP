namespace NetworkProgramming.EnhancedStream
{
    using System;
    using System.Net.Sockets;

    public class ListenerThreadArguments
    {
        private int pollDelay;

        private int readBufferSize;

        private NetworkStream stream;

        public ListenerThreadArguments(NetworkStream networkStream)
        {
            this.Stream = networkStream;

            this.Exit = false;

            this.PollDelay = 200;
        }

        public bool Exit
        {
            get;
            set;
        }

        public int PollDelay
        {
            get
            {
                return this.pollDelay;
            }

            set
            {
                if (value < 1)
                {
                    throw new ArgumentOutOfRangeException(nameof(this.PollDelay), "The specific poll delay must not be smaller than 1!");
                }

                this.pollDelay = value;
            }
        }

        public int ReadBufferSize
        {
            get
            {
                return this.readBufferSize;
            }

            set
            {
                if (readBufferSize < 1)
                {
                    throw new ArgumentOutOfRangeException(nameof(this.ReadBufferSize), "The specific read buffer size must not be smaller than 1!");
                }

                this.readBufferSize = value;
            }
        }

        public NetworkStream Stream
        {
            get
            {
                return this.stream;
            }

            private set
            {
                this.stream = value;
            }
        }
    }
}