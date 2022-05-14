using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.IO;
using System.Net;

namespace Notus_File_Uploader_Test
{
    class Program
    {
        static void Main(string[] args)
        {
            const int bufferSize = 2048;
            string url = @"https://cdn.pixabay.com/photo/2015/04/23/22/00/tree-736885__480.jpg";
            WebClient client = new WebClient();
            Stream imgstream = client.OpenRead(url);

            byte[] buffer = new byte[4096];
            using MemoryStream ms = new MemoryStream();

            int read;
            while ((read = imgstream.Read(buffer, 0, buffer.Length)) > 0)
            {
                ms.Write(buffer, 0, read);
            }

            Console.WriteLine($"FileName : {System.IO.Path.GetFileName(new Uri(url).LocalPath)},\nFileSize : {ms.Length}");
            Uploader chunk = new Uploader();
            chunk.Send(System.IO.Path.GetFileName(new Uri(url).LocalPath), ms.ToArray(), ms.Length, bufferSize);
            Console.WriteLine("All process has been completed");
            Console.WriteLine("Press enter for exit");
            Console.ReadLine();
        }
    }
}