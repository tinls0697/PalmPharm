using System;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace tabbedpage.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class AboutPage : ContentPage
    {
        public AboutPage()
        {
            InitializeComponent();
        }

        async void OnPDFClicked(object sender, EventArgs e)
        {
            var page = new WebViewPage();
            await Navigation.PushAsync(page);
        }

        async void OnPDFStorageClicked(object sender, EventArgs e)
        {
            var page = new WebViewStoragePdf();
            await Navigation.PushAsync(page);
        }

        async void OnTappedDisclaimer(object sender, EventArgs e)
        {
            var page = new WebViewStoragePdf();
            await Navigation.PushAsync(page);
        }
    }
}