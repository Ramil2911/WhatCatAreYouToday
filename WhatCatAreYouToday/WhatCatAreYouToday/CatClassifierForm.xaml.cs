using SkiaSharp;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WhatCatAreYouToday
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CatClassifierForm : ContentView
    {
        public CatClassifierForm(SKBitmap bitmap)
        {
            InitializeComponent();
            CatImage.Source = Classifier.Classify(bitmap);
        }

    }
}