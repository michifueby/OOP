namespace NetworkProgramming
{
    using NetworkProgramming.EnhancedStream;
    using System;
    using System.Net.Sockets;
    using System.Text;
    class Program
    {
        static void Main(string[] args)
        {
            TcpClient client = new TcpClient();

            byte[] sendDataBuffer;

            client.Connect("www.fhwn.ac.at", 80);

            NetworkStream stream = client.GetStream();

            string message = "GET / HTTP/1.1\r\nHost:www.fhwn.ac.at\r\n\r\n";

            EnhancedNetworkStream enhancedNetworkStream = new EnhancedNetworkStream(stream);
            enhancedNetworkStream.OnDataReceived += DataReceivedCB;
            enhancedNetworkStream.StartListening();

            sendDataBuffer = Encoding.ASCII.GetBytes(message);
            stream.Write(sendDataBuffer, 0, sendDataBuffer.Length);

            Console.ReadKey();
        }

        public static void DataReceivedCB(object sender, DataReceivedEventArgs e)
        {
            string receivedData = Encoding.ASCII.GetString(e.Data, 0, e.Data.Length);

            Console.WriteLine(receivedData);
        }
    }
}
