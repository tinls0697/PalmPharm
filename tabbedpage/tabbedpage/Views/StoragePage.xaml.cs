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
    public partial class StoragePage : ContentPage
    {
        public StoragePage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            StoragelistView.ItemsSource = await App.Database.GetSItemsAsync();

            var timeleft = await App.Database.GetAlmostExpire(DateTime.Now.AddDays(7));
            if (Convert.ToInt32(timeleft.Count) > 0)
            {
                await DisplayAlert("Drug Expiring", timeleft.Count.ToString() + " drug(s) expiring soon!", "OK");
            }
            
            var getStorageCt = await App.Database.GetSItemsAsync();
            var StCount = getStorageCt.Count;
            if (StCount == 0)
            {
                StoragelistView.IsVisible = false;
                EmptyMessage.IsVisible = true;
            }
            else
            {
                StoragelistView.IsVisible = true;
                EmptyMessage.IsVisible = false;
            }
        }

        async void AddItem_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new NewStoragePage
            {
                BindingContext = new StorageItem()
            });
        }

        async void OnListSItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if(e.SelectedItem != null)
                {
                await Navigation.PushAsync(new StorageDetailsPage
                {
                    BindingContext = e.SelectedItem as StorageItem
                });
            }
        }
    }
}