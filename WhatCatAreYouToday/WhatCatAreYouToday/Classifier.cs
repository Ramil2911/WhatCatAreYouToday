using System;
using System.Linq;
using Xamarin.Forms;
using Color = System.Drawing.Color;
using Image = Xamarin.Forms.Image;
using Rectangle = System.Drawing.Rectangle;
using SkiaSharp;
using Xamarin.Forms.Internals;

namespace WhatCatAreYouToday
{
    public static class Classifier
    {
        private static readonly Color[] Colors = new Color[]
        {
            Color.FromArgb(255, 142, 131, 124), //1 (0)
            Color.FromArgb(255, 92, 55, 51),
            Color.FromArgb(255, 147, 142, 139),
            Color.FromArgb(255, 165, 126, 111),
            Color.FromArgb(255, 71, 63, 60),
            Color.FromArgb(255, 151, 144, 136),
            Color.FromArgb(255, 91, 89, 94),
            Color.FromArgb(255, 166, 136, 105),
            Color.FromArgb(255, 133, 125, 118),
            Color.FromArgb(255, 110, 78, 68),
            Color.FromArgb(255, 73, 51, 42),
            Color.FromArgb(255, 120, 114, 113),
            Color.FromArgb(255, 125, 101, 68),
            Color.FromArgb(255, 110, 91, 75),
            Color.FromArgb(255, 97, 60, 29),
            Color.FromArgb(255, 123, 85, 44),
            Color.FromArgb(255, 64, 55, 47) //18 (17)
            //Average colors of pictures can be found by CatsClassifier;
        };
        
        public static ImageSource Classify(SKBitmap bitmap)
        {
            var bitmap256 = bitmap.Copy(SKColorType.Argb4444);

            var r = 0;
            var g = 0;
            var b = 0;
            var total = 0;

            for (var x = 0; x < bitmap256.Width; x++) 
            {
                for (var y = 0; y < bitmap256.Height; y++)
                {
                    var clr = bitmap256.GetPixel(x, y);

                    r += clr.Red;
                    g += clr.Green;
                    b += clr.Blue;

                    total++;
                }
            }//it can be paralleled

            //total may be null/zero, but only with specific image
            r /= total; //average red
            g /= total; //average green
            b /= total; //average blue
            
            //https://stackoverflow.com/questions/27374550/how-to-compare-color-object-and-get-closest-color-in-an-color
            //i dont think code below will work
            //but it works
            var target = Color.FromArgb(r, g, b);
            var colorDiffs = Colors.Select(n => ColorDiff(n, target)).Min(n =>n);
            var i =  Colors.IndexOf(n => ColorDiff(n, target) == colorDiffs);
            //Dispose
            bitmap.Dispose();
            bitmap256.Dispose();
            //Dispose
            return ImageSource.FromResource(i <= 2 ? $"WhatCatAreYouToday.cats.{i}.png" : $"WhatCatAreYouToday.cats.{i}.jpg");

        }
        //https://stackoverflow.com/questions/27374550/how-to-compare-color-object-and-get-closest-color-in-an-color
        private static int ColorDiff(Color c1, Color c2) 
        { 
            return  (int) Math.Sqrt((c1.R - c2.R) * (c1.R - c2.R) 
                                   + (c1.G - c2.G) * (c1.G - c2.G)
                                   + (c1.B - c2.B) * (c1.B - c2.B)); 
        }
    }
}