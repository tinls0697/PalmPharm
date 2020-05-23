using System;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using tabbedpage.Models;
using Plugin.LocalNotifications;

namespace tabbedpage.Views
{
    public partial class AlarmDetailPage : ContentPage
    {

        public AlarmDetailPage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            if (TimesTaken.Text != txtTimes.Text)
            {
                btnSetNoti.IsEnabled = true;
            }

            if (DoneDate.Text != DateTime.Today.ToString())
            {
                btnSetNoti.IsEnabled = true;
                TimesTakenPrint.Text = "Times taken today: 0";
                TimesTaken.Text = "0";
            }

            if (TimesTaken.Text != "0" && TimesTaken.Text != txtTimes.Text || Convert.ToDateTime(NextAlarm.Text) > DateTime.Now)
            {
                NextAlarmPrint.Text = "Next Alarm: " + NextAlarm.Text;
                NextAlarmPrint.IsVisible = true;
            }

        }            

        async void OnSetNotiClicked(object sender, EventArgs e)
        {
            double hoursinterval = Convert.ToDouble(Timeinterval.Text) / 2;
            CrossLocalNotifications.Current.Show("Time to take " + DrugName.Text , NickName.Text + " - " + QtyUnit.Text , Convert.ToInt32(ID.Text+"000"+TimesTaken.Text), DateTime.Now.AddHours(hoursinterval));
            await DisplayAlert("Reminder Set", "Notification will go off at " + String.Format("{0:t}",DateTime.Now.AddHours(hoursinterval * 2)), "OK");
            TimesTaken.Text = (Convert.ToInt32(TimesTaken.Text) + 1).ToString();
            DoneDate.Text = DateTime.Now.Date.ToString();
            NextAlarm.Text = DateTime.Now.AddHours(hoursinterval * 2).ToString("HH:mm");

            var alarmItem = (AlarmItem)BindingContext;
            await App.Database.SaveItemAsync(alarmItem);
            await Navigation.PopAsync();
        }

        async void OnDeleteClicked(object sender, EventArgs e)
        {
            bool answer = await DisplayAlert("Confirm", "Are you sure you want to delete this record?", "Yes", "No");
            if (answer == true)
            {
                var alarmItem = (AlarmItem)BindingContext;
                await App.Database.DeleteItemAsync(alarmItem);
                await Navigation.PopAsync();
            }
            
        }

        async void OnOKClicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }

    }
}