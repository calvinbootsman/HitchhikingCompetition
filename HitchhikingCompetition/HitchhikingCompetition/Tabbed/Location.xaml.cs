using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Forms.Maps;
using HitchhikingCompetition.Classes;
using System.Diagnostics;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using Plugin.Geolocator;

namespace HitchhikingCompetition
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Location : ContentPage
    {

        public Location()
        {

            InitializeComponent();


            MyMap.MoveToRegion(
                MapSpan.FromCenterAndRadius(
                    new Position(51.415560, 9.192118), Distance.FromMiles(200)));
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
                //LocationWebsite.IsVisible = true;
                var test = new object();
                var test1 = new EventArgs();
                RefreshPage(test, test1);
            }
            else
            {
                TrackerNotEnabled.IsVisible = true;
                //LocationWebsite.IsVisible = false ;
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
               // LocationWebsite.Source = "http://trickingnederland.nl/lift/maps.php";
            }
            else
            {
                TrackerNotEnabled.IsVisible = true;
            }
        }

        public async void RefreshPage(object sender, EventArgs e)
        {
            if (App.AllowTracking)
            {
                Markers markers = new Markers();
                //var MarkersString = 
                    var list = await markers.GetMarkers();
                foreach (Markers x in list)
                {
                    string label = x.coupleName + "\r\n" + x.Mood;
                    var position = new Position(Convert.ToDouble(x.longitude), Convert.ToDouble(x.latitude)); // Latitude, Longitude
                    var pin = new Pin
                    {
                        Type = PinType.Generic,
                        Position = position,
                        Label = label
                    };
                    MyMap.Pins.Add(pin);
                }
                //Debug.WriteLine(MarkersString);

                //TrackerNotEnabled.IsVisible = false;
               // LocationWebsite.Source = "http://trickingnederland.nl/lift/maps.php"; 
            }
            else
            {
                TrackerNotEnabled.IsVisible = true;
            }
        }

        
    }
}