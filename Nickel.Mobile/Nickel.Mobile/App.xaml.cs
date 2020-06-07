using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Nickel.Mobile
{
    public partial class App : Application  
    {
        public static string ApiAddress { get => "http://10.10.0.1:81/NickelWeb"; }

        public App()
        {
            InitializeComponent();

            MainPage = new MainPage();
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
