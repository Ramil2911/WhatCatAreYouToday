using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using Plugin.Media;
using Plugin.Media.Abstractions;

namespace WhatCatAreYouToday
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            ContentPresenter.Content = new CreateSelfieForm(this);
        }
    }
}
