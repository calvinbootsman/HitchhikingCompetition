using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HitchhikingCompetition
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Location : ContentPage
    {

        public Location()
        {
            InitializeComponent();
            var UpdateLocation = new Button()
            {
                Text = "Get Location"
            };
            UpdateLocation.Clicked += UpdateLocation_Clicked;
            MainStack.Children.Add(UpdateLocation);
            var test = new object();
            var test1 = new EventArgs();
            RefreshPage(test, test1);
            if (!App.AllowTracking)
            {
                TrackerNotEnabled.IsVisible = true;
            }
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            
            if (App.AllowTracking)
            {
                TrackerNotEnabled.IsVisible = false;
                LocationWebsite.IsVisible = true;
                
            }
            else
            {
                TrackerNotEnabled.IsVisible = true;
                LocationWebsite.IsVisible = false ;
            }
        }

        private void UpdateLocation_Clicked(object sender, EventArgs e)
        {
            //We can only track if they have given permission to let us track them.
            if (App.AllowTracking)
            {
                TrackerNotEnabled.IsVisible = false;
                var Settings = new Settings();
                Settings.GetLocation(sender, e);
                LocationWebsite.Source = (LocationWebsite.Source as UrlWebViewSource).Url;
            }
            else
            {
                TrackerNotEnabled.IsVisible = true;
            }
        }

        public void RefreshPage(object sender, EventArgs e)
        {
            if (App.AllowTracking)
            {
                TrackerNotEnabled.IsVisible = false;
                LocationWebsite.Source = "http://trickingnederland.nl/lift/maps.php"; 
            }
            else
            {
                TrackerNotEnabled.IsVisible = true;
            }
        }
    }
}