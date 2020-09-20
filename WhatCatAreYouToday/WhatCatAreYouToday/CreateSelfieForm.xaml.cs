using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Plugin.Media;
using Plugin.Media.Abstractions;
using SkiaSharp;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WhatCatAreYouToday
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CreateSelfieForm : ContentView
    {
        private MainPage _parent;

        public CreateSelfieForm(MainPage parent)
        {
            InitializeComponent();
            _parent = parent;
        }
        
        public async void TakeAPhoto(object sender, EventArgs eventArgs)
        {
            await CrossMedia.Current.Initialize();
            if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
            {
                return;
            }
            var file = await CrossMedia.Current.TakePhotoAsync(new StoreCameraMediaOptions
            {
                Directory = "Photos",
                Name = $"{DateTime.Now:dd.MM.yyyy_hh.mm.ss}.jpg"
            });

            if (file == null)
                return;
            
            _parent.ContentPresenter.Content = new CatClassifierForm(SKBitmap.Decode(file.Path));
        }
    }
}