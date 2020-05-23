using System;
using System.Collections.Generic;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using tabbedpage.Models;
using Plugin.Media;
using Plugin.Media.Abstractions;

namespace tabbedpage.Views
{
    public partial class NewAlarmPage : ContentPage
    {
        public NewAlarmPage()
        {
            InitializeComponent();
            TimesTaken.Text = 0.ToString();
        }

        void OnPickerSelectedIndexChanged(object sender, EventArgs e)
        {
            var picker = (Picker)sender;
            int selectedIndex = picker.SelectedIndex;

            if (selectedIndex != -1)
            {
                UnitEntry.Text = picker.Items[selectedIndex];
            }
        }

        async void OnSaveClicked(object sender, EventArgs e)
        {
            if (String.IsNullOrWhiteSpace(txtDrug.Text)  || String.IsNullOrWhiteSpace(txtNick.Text) || txtQty.Text == "0" || String.IsNullOrWhiteSpace(UnitEntry.Text) || txtTimes.Text == "0" || txtint.Text == "0")
            {
                await DisplayAlert("Alert", "All fields except Notes cannot be empty", "OK");
            } 
            else if ((Convert.ToInt32(txtTimes.Text) * Convert.ToInt32(txtint.Text)) > 16)
            {
                await DisplayAlert("Alert", "The hours needed to finish today's course exceed possible range (16 hours MAX)!", "OK");
            }
            else
            {
                DoneDate.Text = DateTime.Today.AddDays(-1).ToString();
                var alarmItem = (AlarmItem)BindingContext;
                await App.Database.SaveItemAsync(alarmItem);
                await Navigation.PopAsync();
            }            
        }

        async void OnCancelClicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }

        //Photo related
        async void OnClickTakePhoto(object sender, EventArgs e)
        {
            if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
            {
                DisplayAlert("No Camera", ":(No camera available.", "OK");
                return;
            }

            var file = await CrossMedia.Current.TakePhotoAsync(new StoreCameraMediaOptions
            {
                Directory = "Directory",
                SaveToAlbum = true,
                CompressionQuality = 75,
                CustomPhotoSize = 50,
                PhotoSize = PhotoSize.MaxWidthHeight,
                MaxWidthHeight = 2000,
                DefaultCamera = CameraDevice.Front
            });

            if (file == null)
            {
                return;
            }

            imagepath.Text = file.Path;

            image.Source = ImageSource.FromStream(() =>
            {
                var stream = file.GetStream();
                file.Dispose();
                return stream;
            });
        }

        async void OnClickPickPhoto(object sender, EventArgs e)
        {
            if (!CrossMedia.Current.IsPickPhotoSupported)
            {
                DisplayAlert("Photos Not Supported", ":( Permission not granted to photos.", "OK");
                return;
            }
            var file = await CrossMedia.Current.PickPhotoAsync(new PickMediaOptions
            {
                PhotoSize = PhotoSize.Medium,

            });


            if (file == null)
            {
                return;
            }

            imagepath.Text = file.Path;

            image.Source = ImageSource.FromStream(() =>
            {
                var stream = file.GetStream();
                file.Dispose();
                return stream;
            });
        }
    }
}