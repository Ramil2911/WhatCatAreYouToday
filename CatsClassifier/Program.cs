using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CatsClassifier
{
    class Program
    {
        static void Main(string[] args)
        {
            
            
            var files = Directory.GetFiles("C:\\Users\\Ramil\\Desktop\\коты", "*.png").ToList();
            files.AddRange(Directory.GetFiles("C:\\Users\\Ramil\\Desktop\\коты", "*.jpg"));
            var data = new string[files.Count];
            Parallel.ForEach(files, file =>
            {
                File.Copy(file, $"C:\\Users\\Ramil\\Desktop\\коты\\passed\\{files.IndexOf(file)}{Path.GetExtension(file)}");
            });
            
            files = Directory.GetFiles("C:\\Users\\Ramil\\Desktop\\коты\\passed", "*.png").ToList();
            files.AddRange(Directory.GetFiles("C:\\Users\\Ramil\\Desktop\\коты\\passed", "*.jpg"));
            
            Parallel.ForEach(files, file =>
            {
                var bitmap = new Bitmap(file);
                var bitmap256 = bitmap.Clone(new Rectangle(0, 0, bitmap.Width, bitmap.Height),
                    PixelFormat.Format8bppIndexed);
                bitmap.Dispose();

                var r = 0;
                var g = 0;
                var b = 0;
                var total = 0;

                for (var x = 0; x < bitmap256.Width; x++)
                {
                    for (var y = 0; y < bitmap256.Height; y++)
                    {
                        var clr = bitmap256.GetPixel(x, y);

                        r += clr.R;
                        g += clr.G;
                        b += clr.B;

                        total++;
                    }
                }

                //total may be null/zero, but only with specific image
                r /= total;
                g /= total;
                b /= total;
                Console.WriteLine($"{Color.FromArgb(r, g, b)}\n{file}");
                
                bitmap256.Dispose();
                data[files.IndexOf(file)] = $"Color.FromArgb(255, {r}, {g}, {b})";
            });
            
            File.WriteAllLines(@"C:\Users\Ramil\Desktop\коты\passed\cats.txt", data);
        }
    }
}