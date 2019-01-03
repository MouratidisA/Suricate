using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Management;
using System.Net.NetworkInformation;
using System.Windows.Forms;
using AForge.Video.FFMPEG;

namespace Suricate
{
    class Program
    {
        public static int ShotId;


        static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\r\n  ____                   _                  _          \r\n / ___|   _   _   _ __  (_)   ___    __ _  | |_    ___ \r\n \\___ \\  | | | | | '__| | |  / __|  / _` | | __|  / _ \\\r\n  ___) | | |_| | | |    | | | (__  | (_| | | |_  |  __/\r\n |____/   \\__,_| |_|    |_|  \\___|  \\__,_|  \\__|  \\___|\r\n                                                       \r\n");

            Console.WriteLine("Monitoring Console application \nVersion:1.0.0 \nFeatures: \n * Screenshots \n * System information log reports");
            Console.WriteLine("___________________________________________\n");


            //Properties  
            int AmountOfShots = 5; // optional feature
            double intervals = 5; // seconds
            ShotId = 0;
            //Time now
            var lastShot = DateTime.Now.TimeOfDay;

            Console.ResetColor();

            Console.WriteLine("Press ESC to stop");
            do
            {
                while (!Console.KeyAvailable)
                {
                    Console.WriteLine("<-> " + lastShot);
                    TakeScreenShot(ShotId);
                    WriteLogReport("Screenshot Captured !!");
                    ShotId++;
                    System.Threading.Thread.Sleep((int)TimeSpan.FromSeconds(intervals).TotalMilliseconds);
                    //WriteLogReport("System info report:\n");
                    lastShot = DateTime.Now.TimeOfDay;
                }

                if (ShotId == AmountOfShots)
                    break;

            } while (Console.ReadKey(true).Key != ConsoleKey.Escape);

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Finishing reports...");
            System.Threading.Thread.Sleep(1000);
            //write general sys report log 
            Console.ResetColor();
            WriteLogReport("=============================");
            Console.WriteLine("VideoControllerReport");
            VideoControllerReport();
            Console.WriteLine("DrivesReport");
            DrivesReport();
            Console.WriteLine("ProcessorsReport");
            ProcessorsReport();
            Console.WriteLine("NetworkInterfacesReport");
            NetworkInterfacesReport();
            Console.WriteLine("AudioDevicesReport");
            AudioDevicesReport();
            Console.WriteLine("PrintersReport");
            PrintersReport();

            TimeLapseConverter();
            System.Threading.Thread.Sleep(10000);
            Console.ReadKey();
        }

        public static void VideoControllerReport()
        {
            ManagementObjectSearcher myVideoObject = new ManagementObjectSearcher("select * from Win32_VideoController");

            foreach (var o in myVideoObject.Get())
            {
                var obj = (ManagementObject)o;
                string message = "\n" + "Win32_VideoController: \n";
                Console.WriteLine("Name  -  " + obj["Name"]);
                Console.WriteLine("Status  -  " + obj["Status"]);
                Console.WriteLine("Caption  -  " + obj["Caption"]);
                Console.WriteLine("DeviceID  -  " + obj["DeviceID"]);
                Console.WriteLine("AdapterRAM  -  " + obj["AdapterRAM"]);
                Console.WriteLine("AdapterDACType  -  " + obj["AdapterDACType"]);
                Console.WriteLine("Monochrome  -  " + obj["Monochrome"]);
                Console.WriteLine("InstalledDisplayDrivers  -  " + obj["InstalledDisplayDrivers"]);
                Console.WriteLine("DriverVersion  -  " + obj["DriverVersion"]);
                Console.WriteLine("VideoProcessor  -  " + obj["VideoProcessor"]);
                Console.WriteLine("VideoArchitecture  -  " + obj["VideoArchitecture"]);
                Console.WriteLine("VideoMemoryType  -  " + obj["VideoMemoryType"]);

                message += "Name - " + obj["Name"] + "\n";
                message += "Status  -  " + obj["Status"] + "\n";
                message += "Caption  -  " + obj["Caption"] + "\n";
                message += "DeviceID  -  " + obj["DeviceID"] + "\n";
                message += "AdapterRAM  -  " + obj["AdapterRAM"] + "\n";
                message += "AdapterDACType  -  " + obj["AdapterDACType"] + "\n";
                message += "Monochrome  -  " + obj["Monochrome"] + "\n";
                message += "InstalledDisplayDrivers  -  " + obj["InstalledDisplayDrivers"] + "\n";
                message += "DriverVersion  -  " + obj["DriverVersion"] + "\n";
                message += "VideoProcessor  -  " + obj["VideoProcessor"] + "\n";
                message += "VideoArchitecture  -  " + obj["VideoArchitecture"] + "\n";
                message += "VideoMemoryType  -  " + obj["VideoMemoryType"] + "\n";


                message += "\n" + "AdapterRAM: \n"; ;
                message += ("AdapterRAM  -  " + ((long)Convert.ToDouble(obj["AdapterRAM"])) + "\n");

                WriteLogReport(message);
            }
        }

        public static void ProcessorsReport()
        {
            ManagementObjectSearcher myProcessorObject = new ManagementObjectSearcher("select * from Win32_Processor");

            foreach (var obj in myProcessorObject.Get())
            {
                string message = "\n" + "Processor: \n";
                Console.WriteLine("Name  -  " + obj["Name"]);
                Console.WriteLine("DeviceID  -  " + obj["DeviceID"]);
                Console.WriteLine("Manufacturer  -  " + obj["Manufacturer"]);
                Console.WriteLine("CurrentClockSpeed  -  " + obj["CurrentClockSpeed"]);
                Console.WriteLine("Caption  -  " + obj["Caption"]);
                Console.WriteLine("NumberOfCores  -  " + obj["NumberOfCores"]);
                Console.WriteLine("NumberOfEnabledCore  -  " + obj["NumberOfEnabledCore"]);
                Console.WriteLine("NumberOfLogicalProcessors  -  " + obj["NumberOfLogicalProcessors"]);
                Console.WriteLine("Architecture  -  " + obj["Architecture"]);
                Console.WriteLine("Family  -  " + obj["Family"]);
                Console.WriteLine("ProcessorType  -  " + obj["ProcessorType"]);
                Console.WriteLine("Characteristics  -  " + obj["Characteristics"]);
                Console.WriteLine("AddressWidth  -  " + obj["AddressWidth"]);

                message += ("Name  -  " + obj["Name"]);
                message += ("DeviceID  -  " + obj["DeviceID"]);
                message += ("Manufacturer  -  " + obj["Manufacturer"]);
                message += ("CurrentClockSpeed  -  " + obj["CurrentClockSpeed"]);
                message += ("Caption  -  " + obj["Caption"]);
                message += ("NumberOfCores  -  " + obj["NumberOfCores"]);
                message += ("NumberOfEnabledCore  -  " + obj["NumberOfEnabledCore"]);
                message += ("NumberOfLogicalProcessors  -  " + obj["NumberOfLogicalProcessors"]);
                message += ("Architecture  -  " + obj["Architecture"]);
                message += ("Family  -  " + obj["Family"]);
                message += ("ProcessorType  -  " + obj["ProcessorType"]);
                message += ("Characteristics  -  " + obj["Characteristics"]);
                message += ("AddressWidth  -  " + obj["AddressWidth"]);

                WriteLogReport(message);
            }
        }

        public static void DrivesReport()
        {
            DriveInfo[] allDrives = DriveInfo.GetDrives();



            foreach (var d in allDrives)
            {
                string message = "\n" + "Driver: \n";
                Console.WriteLine("Drive {0}", d.Name);
                Console.WriteLine("Drive type: {0}", d.DriveType);
                if (d.IsReady == true)
                {
                    Console.WriteLine("  Volume label: {0}", d.VolumeLabel);
                    Console.WriteLine("  File system: {0}", d.DriveFormat);
                    Console.WriteLine("  Available space to current user:{0, 15} bytes", d.AvailableFreeSpace);
                    Console.WriteLine("  Total available space:          {0, 15} bytes", d.TotalFreeSpace);
                    Console.WriteLine("  Total size of drive:            {0, 15} bytes ", d.TotalSize);
                    Console.WriteLine("  Root directory:            {0, 12}", d.RootDirectory);

                    Console.WriteLine("  Available space to current user:{0, 15}", (d.AvailableFreeSpace));
                    Console.WriteLine("  Total available space:          {0, 15}", (d.TotalFreeSpace));
                    Console.WriteLine("  Total size of drive:            {0, 15} ", (d.TotalSize));

                    message += ("Drive: " + d.Name + " \n");
                    message += ("Drive type: " + d.DriveType + " \n");

                    message += ("Volume label:  " + d.VolumeLabel + " \n");
                    message += ("File system:  " + d.DriveFormat + " \n");
                    message += ("Available space to current user: " + d.AvailableFreeSpace + " bytes \n");
                    message += ("Total available space: " + d.TotalFreeSpace + " bytes \n");
                    message += ("Total size of drive: " + d.TotalSize + " bytes \n");
                    message += ("Root directory: " + d.RootDirectory + "\n");

                    message += ("Available space to current user: " + (d.AvailableFreeSpace) + "\n");
                    message += ("Total available space: " + (d.TotalFreeSpace) + "\n");
                    message += ("Total size of drive: " + (d.TotalSize) + "\n");


                    WriteLogReport(message);
                }

            }
        }

        public static void NetworkInterfacesReport()
        {
            NetworkInterface[] nics = NetworkInterface.GetAllNetworkInterfaces();

            if (nics.Length < 1)
            {
                Console.WriteLine("  No network interfaces found.");
            }
            else
            {
                foreach (NetworkInterface adapter in nics)
                {
                    string message = "\n" + "NetworkInterface: \n";

                    IPInterfaceProperties properties = adapter.GetIPProperties();
                    Console.WriteLine();
                    Console.WriteLine(adapter.Description);
                    Console.WriteLine(String.Empty.PadLeft(adapter.Description.Length, '='));
                    Console.WriteLine("  Interface type .......................... : {0}", adapter.NetworkInterfaceType);
                    Console.WriteLine("  Physical Address ........................ : {0}", adapter.GetPhysicalAddress().ToString());
                    Console.WriteLine("  Operational status ...................... : {0}", adapter.OperationalStatus);

                    message += (String.Empty.PadLeft(adapter.Description.Length, '=')) + "\n";
                    message += ("Interface type .......................... : " + adapter.NetworkInterfaceType) + "\n";
                    message += ("Physical Address ........................ : " + adapter.GetPhysicalAddress().ToString()) + "\n";
                    message += ("Operational status ...................... : " + adapter.OperationalStatus) + "\n";

                    WriteLogReport(message);
                }
            }
        }

        public static void AudioDevicesReport()
        {
            ManagementObjectSearcher myAudioObject = new ManagementObjectSearcher("select * from Win32_SoundDevice");

            foreach (var obj in myAudioObject.Get())
            {
                string message = "\n" + "AudioDevice: \n";

                Console.WriteLine("Name  -  " + obj["Name"]);
                Console.WriteLine("ProductName  -  " + obj["ProductName"]);
                Console.WriteLine("Availability  -  " + obj["Availability"]);
                Console.WriteLine("DeviceID  -  " + obj["DeviceID"]);
                Console.WriteLine("PowerManagementSupported  -  " + obj["PowerManagementSupported"]);
                Console.WriteLine("Status  -  " + obj["Status"]);
                Console.WriteLine("StatusInfo  -  " + obj["StatusInfo"]);
                Console.WriteLine(String.Empty.PadLeft(obj["ProductName"].ToString().Length, '='));

                message += ("Name  -  " + obj["Name"]) + "\n";
                message += ("ProductName  -  " + obj["ProductName"]) + "\n";
                message += ("Availability  -  " + obj["Availability"]) + "\n";
                message += ("DeviceID  -  " + obj["DeviceID"]) + "\n";
                message += ("PowerManagementSupported  -  " + obj["PowerManagementSupported"]) + "\n";
                message += ("Status  -  " + obj["Status"]) + "\n";
                message += ("StatusInfo  -  " + obj["StatusInfo"]) + "\n";
                message += (String.Empty.PadLeft(obj["ProductName"].ToString().Length, '=')) + "\n";

                WriteLogReport(message);
            }
        }

        public static void PrintersReport()
        {
            ManagementObjectSearcher myPrinterObject = new ManagementObjectSearcher("select * from Win32_Printer");

            foreach (var obj in myPrinterObject.Get())
            {
                string message = "\n" + "Printer: \n";

                Console.WriteLine("Name  -  " + obj["Name"]);
                Console.WriteLine("Network  -  " + obj["Network"]);
                Console.WriteLine("Availability  -  " + obj["Availability"]);
                Console.WriteLine("Is default printer  -  " + obj["Default"]);
                Console.WriteLine("DeviceID  -  " + obj["DeviceID"]);
                Console.WriteLine("Status  -  " + obj["Status"]);

                Console.WriteLine(String.Empty.PadLeft(obj["Name"].ToString().Length, '='));

                message += ("Name  -  " + obj["Name"]) + "\n";
                message += ("Network  -  " + obj["Network"]) + "\n";
                message += ("Availability  -  " + obj["Availability"]) + "\n";
                message += ("Is default printer  -  " + obj["Default"]) + "\n";
                message += ("DeviceID  -  " + obj["DeviceID"]) + "\n";
                message += ("Status  -  " + obj["Status"]) + "\n";
                message += (String.Empty.PadLeft(obj["Name"].ToString().Length, '=')) + "\n";

                WriteLogReport(message);
            }
        }

        public static void TimeLapseConverter()
        {

            string path = AppDomain.CurrentDomain.BaseDirectory + "\\Screenshots\\" + DateTime.Now.Date.ToShortDateString().Replace('/', '_');

            var imageCount = Directory.GetFiles(path).Length;

            using (var videoWriter = new VideoFileWriter())
            {
                videoWriter.Open(path + DateTime.Now.Date.ToShortDateString().Replace('/', '_') + "_TimeLapse.avi", Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height, 15, VideoCodec.MPEG4, 1000000);

                for (int imageFrame = 0; imageFrame < imageCount; imageFrame++)
                {
                    var imgPath = String.Format("{0}/Screenshot_{1}.bmp", path, imageFrame);

                    using (Bitmap image = Bitmap.FromFile(imgPath) as Bitmap)
                    {
                        videoWriter.WriteVideoFrame(image);
                    }
                }

                videoWriter.Close();
            }

            Console.WriteLine("Video ready!");
        }


        public static void WriteLogReport(string message)
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + "\\Logs";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            string filepath = AppDomain.CurrentDomain.BaseDirectory + "\\Logs\\ServiceLog_" + DateTime.Now.Date.ToShortDateString().Replace('/', '_') + ".txt";
            if (!File.Exists(filepath))
            {
                // Create a file to write to.   
                using (StreamWriter sw = File.CreateText(filepath))
                {
                    sw.WriteLine(message);
                }
            }
            else
            {
                using (StreamWriter sw = File.AppendText(filepath))
                {
                    sw.WriteLine(message);
                }
            }
        }


        public static void TakeScreenShot(int id)
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + "\\Screenshots\\" + DateTime.Now.Date.ToShortDateString().Replace('/', '_');
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            Bitmap bitmap = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height, PixelFormat.Format32bppArgb);

            Graphics graphics = Graphics.FromImage(bitmap);
            graphics.CopyFromScreen(0, 0, 0, 0, bitmap.Size);
            bitmap.Save(path + "/Screenshot_" + id + ".bmp", ImageFormat.Bmp);
        }

    }

}
