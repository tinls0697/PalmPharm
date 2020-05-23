using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using tabbedpage.Views;
using tabbedpage.Data;

namespace tabbedpage
{
    public partial class App : Application
    {
        static AlarmDatabase database;

        public App()
        {
            InitializeComponent();

            //DependencyService.Register<MockDataStore>();
            MainPage = new MainPage();
        }

        public static AlarmDatabase Database
        {
            get
            {
                if (database == null)
                {
                    database = new AlarmDatabase();
                }
                return database;
            }
        }

        protected override void OnStart()
        {

        }

        protected override void OnSleep()
        {

        }

        protected override void OnResume()
        {

        }
    }
}
