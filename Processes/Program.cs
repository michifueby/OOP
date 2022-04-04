namespace Processes
{
    using System;
    using System.Diagnostics;
    using System.Linq;

    public class Program
    {
        public static void Main(string[] args)
        {
            // Start process with arguments
            Process process = new Process();
            process.StartInfo.FileName = "ping.exe";
            process.StartInfo.Arguments = "-t -n 6 10.0.0.138";
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.CreateNoWindow = true;

            process.OutputDataReceived += ProcessOutputReceivedCB;

            process.Start();
            process.BeginOutputReadLine();
            process.WaitForExit();

            // Check if process is running (true or false)
            Console.WriteLine("Process Status, is process running:" + IsProcessRunning(process.StartInfo.FileName));

            // List all processes
            ShowProcessInfoFromAllProcesses();

            // Filter process
            FilterProcessByName("Chrome.exe");

            Console.ReadKey();
        }

        private static void ProcessOutputReceivedCB(object sender, DataReceivedEventArgs e)
        {
            if (e.Data == null)
            {
                return;
            }

            Console.WriteLine(e.Data);
        }

        private static bool IsProcessRunning(string processName)
        {
            return Process.GetProcessesByName(processName).Any();
        }

        private static void ShowProcessInfoFromAllProcesses()
        {
            Process[] allProcess = Process.GetProcesses();

            Console.WriteLine("Processes:");

            foreach (var process in allProcess)
            {
                Console.WriteLine(process.Id + ":" + process.ProcessName);
            }
        }

        private static void FilterProcessByName(string processName)
        {
            Process[] specificProcess = Process.GetProcesses().Where(p => p.ProcessName.Equals(processName)).ToArray();

            Process processId = Process.GetProcessById(0);

            Process[] processNames = Process.GetProcessesByName("ping.exe");

            Process currentProcess = Process.GetCurrentProcess();
        }
    }
}
