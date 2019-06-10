using AForge.Video.FFMPEG;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Forms;

namespace Suricate
{
    class Program
    {
        public static int ShotId;
        public static string path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\Suricate\\" + DateTime.Now.Date.ToShortDateString().Replace('/', '_');
        public static string keepImages = String.Empty;


        static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\r\n  ____                   _                  _          \r\n / ___|   _   _   _ __  (_)   ___    __ _  | |_    ___ \r\n \\___ \\  | | | | | '__| | |  / __|  / _` | | __|  / _ \\\r\n  ___) | | |_| | | |    | | | (__  | (_| | | |_  |  __/\r\n |____/   \\__,_| |_|    |_|  \\___|  \\__,_|  \\__|  \\___|\r\n                                                       \r\n");

            Console.WriteLine("Monitoring Console application \nVersion:1.0.0 \nFeatures: \n * Screenshots");
            Console.WriteLine("___________________________________________\n");

            ShotId = 0;
            //Time now
            var lastShot = DateTime.Now.TimeOfDay;


            Console.ResetColor();
            Console.WriteLine("Set Screen capture interval seconds: ");
            var intervals = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Would you like to keep Screenshots image files y/n?");
            keepImages = Console.ReadLine();

            Console.WriteLine("___________________________________________\n");
            Console.WriteLine("Press ESC to stop");
            do
            {
                while (!Console.KeyAvailable)
                {
                    Console.WriteLine("<-> " + lastShot);
                    TakeScreenShot(ShotId);
                    ShotId++;
                    System.Threading.Thread.Sleep((int)TimeSpan.FromSeconds(intervals).TotalMilliseconds);
                    //WriteLogReport("System info report:\n");
                    lastShot = DateTime.Now.TimeOfDay;
                }

            } while (Console.ReadKey(true).Key != ConsoleKey.Escape);

            Console.ForegroundColor = ConsoleColor.Red;

            TimeLapseConverter();
            System.Threading.Thread.Sleep(10000);
        }




        public static void TimeLapseConverter()
        {
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
            var dir = new DirectoryInfo(path);

            if (keepImages.Equals("y"))
            {
                dir.Delete(true); // true => recursive delete
                Console.WriteLine("Video Screenshots' folder removed...");
            }

        }

        public static void TakeScreenShot(int id)
        {

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
