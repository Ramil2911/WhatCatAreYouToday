using System;
using Plugin.Media;
using Plugin.Media.Abstractions;
using SkiaSharp;
using Xamarin.Forms.Xaml;

namespace WhatCatAreYouToday
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CreateSelfieForm
    {
        private readonly MainPage _parent;

        public CreateSelfieForm(MainPage parent)
        {
            InitializeComponent();
            _parent = parent;
        }
        
        private async void TakeAPhoto(object sender, EventArgs eventArgs)
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

        private async void PickAPhoto(object sender, EventArgs eventArgs)
        {
            await CrossMedia.Current.Initialize();
            if (!CrossMedia.Current.IsPickPhotoSupported)
            {
                return;
            }
            var file = await CrossMedia.Current.PickPhotoAsync();
            if (file == null)
                return;
            
            _parent.ContentPresenter.Content = new CatClassifierForm(SKBitmap.Decode(file.Path));
        }
    }
}