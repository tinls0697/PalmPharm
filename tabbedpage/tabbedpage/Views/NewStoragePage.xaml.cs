using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using tabbedpage.Models;
using Plugin.Media;
using Plugin.Media.Abstractions;

namespace tabbedpage.Views
{
    public partial class NewStoragePage : ContentPage
    {
        public NewStoragePage()
        {
            InitializeComponent();
            //------Two week minimum "used by" buffer------//
            DPicker.MinimumDate = DateTime.Now.AddDays(14);
            DPicker.Date = DateTime.Now.AddDays(14);
        }

        async void OnSaveClicked(object sender, EventArgs e)
        {
            //Check required fields
            if (String.IsNullOrWhiteSpace(txtDrug.Text) || String.IsNullOrWhiteSpace(txtNick.Text))
            {
                await DisplayAlert("Alert", "Drug name and it's nickname cannot be empty", "OK");
            } 
            else if (imagepath.Text == null) //ask user if he wants an image if no image input
            {
                bool answer = await DisplayAlert("Add image?", "Would you want to add an image? It could help you locate your meds.", "Yes", "No");

                if (answer == false) //insisted no for image, continue saving. Break if want to add image
                {
                    //------prevent DatePicker value null bug------//
                    DateEntry.Text = DPicker.Date.ToString();

                    if (String.IsNullOrWhiteSpace(txtNote.Text))
                    {
                        txtNote.Text = "N/A";
                    }

                    var storageItem = (StorageItem)BindingContext;
                    await App.Database.SaveSItemAsync(storageItem);
                    await Navigation.PopAsync();
                }
            }
            else if (imagepath.Text != null) //save if image provided, need "else-if" to prevent double assigning "storageItem"
            {
                //------prevent DatePicker value null bug------//
                DateEntry.Text = DPicker.Date.ToString();

                if (String.IsNullOrWhiteSpace(txtNote.Text))
                {
                    txtNote.Text = "N/A";
                }

                var storageItem = (StorageItem)BindingContext;
                await App.Database.SaveSItemAsync(storageItem);
                await Navigation.PopAsync();
            }      
        }

        async void OnCancelClicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
        private void DatePicker_DateSelected(object sender, DateChangedEventArgs e)
        {
            DateEntry.Text = e.NewDate.ToString();
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