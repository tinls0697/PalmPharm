using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using tabbedpage.Models;

namespace tabbedpage.Views
{
    public partial class StorageDetailsPage : ContentPage
    {
        public StorageDetailsPage()
        {
            InitializeComponent();
        }

        public StorageDetailsPage(object obj)
        {
            BindingContext = obj;
            InitializeComponent();
        }

        async void OnEditClicked(object sender, EventArgs e)
        {
            var page = new EditStoragePage(BindingContext);
            await Navigation.PushAsync(page);
        }

        public async void OnDeleteClicked(object sender, EventArgs e)
        {
            bool answer = await DisplayAlert("Confirm", "Are you sure you want to delete this record?", "Yes", "No");
            if (answer == true)
            {
                var storageItem = (StorageItem)BindingContext;
                await App.Database.DeleteSItemAsync(storageItem);
                await Navigation.PopAsync();
            }            
        }

        async void OnOKClicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}