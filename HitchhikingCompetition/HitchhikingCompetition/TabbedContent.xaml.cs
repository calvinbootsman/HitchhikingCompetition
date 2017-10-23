using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Diagnostics;
using PCLStorage;

namespace HitchhikingCompetition
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TabbedContent : TabbedPage
    {
        public TabbedContent()
        {
            InitializeComponent();
            CheckTracking();
            if (Device.RuntimePlatform != Device.iOS)
            {
                Children.Add(new Location() { Title = "Location" });
                //Children.Add(new StatisticsPage() { Title = "Ride" });
                Children.Add(new Crazy88() { Title = "Crazy88" });
                Children.Add(new Settings() { Title = "Settings" });
                //Children.Add(new AddAdventurePage(){ Title="Adventure Page"});
            }
            else
            {
                Children.Add(new Location() { Icon = "Location/ic_location_on_36pt.png" });
                //Children.Add(new StatisticsPage(){Icon = "Car/ic_directions_car_48pt.png"});
                Children.Add(new Crazy88() { Icon = "List/ic_format_list_numbered_36pt.png" });
                //Children.Add(new Settings(){Icon = "Settings/ic_settings_36pt.png" });
                //Children.Add(new AddAdventurePage() { Title = "Adventure Page" });
            }

            UpdateLocation();
            try
            {
                if (App.AllowTracking)
                {
                    Device.StartTimer(TimeSpan.FromMinutes(3), () =>
                    {

                        // call your method to check for notifications here
                        UpdateLocation();
                        // Returning true means you want to repeat this timer
                        return App.AllowTracking;
                    });
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
            } 
        }
       public void UpdateLocation()
        {
            var test = new object();
            var test1 = new EventArgs();
            Settings setting = new Settings();
            setting.GetLocation(test,test1);
        }
        async void CheckTracking()
        {
            IFile file = await FileHandling.GetFile("Bools", "AllowTracking");
            var temp = await file.ReadAllTextAsync();
            if (temp == "1") App.AllowTracking = true;
            else App.AllowTracking = false;
        }
    }
}