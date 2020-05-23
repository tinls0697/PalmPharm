using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using tabbedpage.Models;
using tabbedpage.Views;

namespace tabbedpage.Views
{
    public partial class AlarmPage : ContentPage
    {

        public AlarmPage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            AlarmlistView.ItemsSource = await App.Database.GetItemsAsync();
            var getAlarmCt = await App.Database.GetItemsAsync();
            var Alcount = getAlarmCt.Count;
            if (Alcount == 0)
            {
                AlarmlistView.IsVisible = false;
                EmptyMessage.IsVisible = true;
            }
            else
            {
                AlarmlistView.IsVisible = true;
                EmptyMessage.IsVisible = false;
            }
        }

        async void AddItem_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new NewAlarmPage
            {
                BindingContext = new AlarmItem()
            });
            
        }

        async void OnListItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem != null)
            {
                await Navigation.PushAsync(new AlarmDetailPage
                {
                    BindingContext = e.SelectedItem as AlarmItem
                });
            }
        }
    }
}