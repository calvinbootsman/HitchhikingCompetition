using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PCLStorage;
using Xamarin.Forms;
using System.Threading.Tasks;

namespace HitchhikingCompetition
{
    public partial class App : Application
    {
        public static string MainUsername { get; set; }
        public static bool AllowTracking{ get; set; }
        public App()
        {
            InitializeComponent();
            
            MainPage = new NavigationPage (new StartUpPage()); 
            
        }
        
        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
