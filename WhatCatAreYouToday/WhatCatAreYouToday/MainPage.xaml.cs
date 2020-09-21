namespace WhatCatAreYouToday
{
    public partial class MainPage
    {
        public MainPage()
        {
            InitializeComponent();
            ContentPresenter.Content = new CreateSelfieForm(this);
        }
    }
}
