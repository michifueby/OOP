namespace SystemIO
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
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

            // Set CreationTime
            Directory.SetCreationTime("test.txt", new DateTime(2000, 11, 8, 15, 10, 7));
            Directory.SetCreationTimeUtc("test.txt", new DateTime(2000, 11, 8, 15, 10, 7));

            // Move directory
            Directory.Move(@"C:\Temp\Test\Test1", @"C:\Temp\test4");

            // Get filename
            string fileName = Path.GetFileName(@"C:/Temp/Test/test.txt");
            Console.WriteLine(fileName);

            // Get extension from file
            string extension = Path.GetExtension(@"C:/Temp/Test/test.txt");
            Console.WriteLine(extension);

            // Find all directories from specific path
            string[] directories = Directory.GetDirectories(@"C:\Temp\test", "*", SearchOption.AllDirectories);

            // Find all entries from path
            string[] directoriesAndFiles = Directory.GetFileSystemEntries(@"C:\Temp\test", "*", SearchOption.AllDirectories);

            // Get all directories with IEnumerable
            var directories2 = Directory.EnumerateDirectories(@"C:\Temp\test");
            // or
            IEnumerable<string> allDirectories = Directory.EnumerateDirectories(@"C:\Temp\test");

            // Get all directories and files with IEnumerable
            var directoriesAndFiles2 = Directory.EnumerateFileSystemEntries(@"C:\Temp\test");
            // or
            IEnumerable<string> directoriesAndFiles3 = Directory.EnumerateDirectories(@"C:\Temp\test");

            // Hide a directory
            DirectoryInfo directoryInfo = new DirectoryInfo(@"C:\Temp\test");
            directoryInfo.Attributes = FileAttributes.Directory | FileAttributes.Hidden;

            // Unhide a directory
            directoryInfo.Attributes = FileAttributes.Directory;

            // Paths

            // Get directory path seperator
            char directoryPathSeperator = Path.DirectorySeparatorChar;

            // Get path seperator
            char pathSeperator = Path.PathSeparator;

            // Get volume seperator
            char volumeSeperator = Path.VolumeSeparatorChar;

            // Temp file name
            string tempFileName = Path.GetTempFileName();

            // Temp file path
            string tempFilePath = Path.GetTempPath();

            // Move file
            File.Move(@"C:\Temp\Test\Test1\test.txt", @"C:\Temp\test4\test.txt");
           
            // Set creation time of file
            File.SetCreationTime("test.txt", new DateTime(2000, 11, 8, 15, 10, 7));

            // Hide a file
            FileInfo fileInfo = new FileInfo(@"C:\Temp\test4\test.txt");
            fileInfo.Attributes = FileAttributes.Normal | FileAttributes.Hidden;

            // Unhide a file
            fileInfo.Attributes = FileAttributes.Normal;

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
