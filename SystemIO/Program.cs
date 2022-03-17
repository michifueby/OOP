namespace SystemIO
{
    using System;
    using System.IO;
    using System.Text;

    class Program
    {
        static void Main(string[] args)
        {
            GetDriveInfo();

            // FileStream - Write
            FileStream writeStream = new FileStream("test.txt", FileMode.OpenOrCreate, FileAccess.Write);

            string content = "This is a test";
            byte[] byteArray = Encoding.UTF8.GetBytes(content);

            writeStream.Write(byteArray, 0, byteArray.Length);
            writeStream.Close();

            
            // FileStream - Read
            FileStream readStream = new FileStream("test.txt", FileMode.Open);

            byteArray = new byte[1024];
            int readBytes = readStream.Read(byteArray, 0, byteArray.Length);
            byte[] copyArray = new byte[readBytes];
            Array.Copy(byteArray, copyArray, readBytes);

            Console.WriteLine(Encoding.UTF8.GetString(copyArray));
            readStream.Close();
            

            // Copy from one file to another
            FileStream source = new FileStream("test.txt", FileMode.Open);
            FileStream destination = new FileStream(@"../test2.txt", FileMode.Create);

            // StreamWriter and StreamReader
            using (StreamReader reader = new StreamReader(source))
            using (StreamWriter writer = new StreamWriter(destination))
            {
                while (!reader.EndOfStream)
                {
                    string line = reader.ReadLine();
                    writer.WriteLine(line);
                }
            }

            FileStream fileStream = new FileStream("test3.txt", FileMode.Create);
            
            using (StreamReader reader = new StreamReader(fileStream))
            using (StreamWriter writer = new StreamWriter(fileStream))
            {
                content = "This is a new test";

                writer.WriteLine(content);

                fileStream.Seek(0, SeekOrigin.Begin);

                string endContent = reader.ReadLine();
                Console.WriteLine(endContent);
            }

            
            // Directory, Files and Path
            
            // Get Current working directory
            string currentWorkingDirectory = Directory.GetCurrentDirectory();
            Console.WriteLine(currentWorkingDirectory);

            if (!Directory.Exists(@"C:/Temp/Test"))
            {
                Directory.CreateDirectory(@"C:/Temp/Test/Test1");
            }
            else
            {
                Directory.Delete(@"C:/Temp/Test", true);
            }

            Directory.SetCreationTime("test.txt", new DateTime(2000, 11, 8, 15, 10, 7));
            Directory.SetCreationTimeUtc("test.txt", new DateTime(2000, 11, 8, 15, 10, 7));

            // Get filename
            string fileName = Path.GetFileName(@"C:/Temp/Test/test.txt");
            Console.WriteLine(fileName);

            // Get extension from file
            string extension = Path.GetExtension(@"C:/Temp/Test/test.txt");
            Console.WriteLine(extension);

            Console.ReadKey();
        }

        // Dicover Drives
        private static void GetDriveInfo()
        {
            DriveInfo[] drives = DriveInfo.GetDrives();

            foreach (var drive in drives)
            {
                try
                {
                    Console.WriteLine($"Drive name: {drive.Name}");
                    Console.WriteLine($"Drive type: {drive.DriveType}");
                    Console.WriteLine($"Available free space: {drive.AvailableFreeSpace}");
                    Console.WriteLine($"Total free space: {drive.TotalFreeSpace}");
                    Console.WriteLine($"Total size: {drive.TotalSize}");
                    Console.WriteLine($"Volume label: {drive.VolumeLabel}");
                }
                catch (Exception)
                {
                    Console.WriteLine("An error occurred while displaying all information!");
                }
            }
        }
    }
}
