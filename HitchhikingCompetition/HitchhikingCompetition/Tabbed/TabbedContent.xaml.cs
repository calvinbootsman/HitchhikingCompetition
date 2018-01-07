﻿using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Diagnostics;
using Plugin.Geolocator;

namespace HitchhikingCompetition
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TabbedContent : TabbedPage
    {
        public TabbedContent()
        {
            InitializeComponent();
            
            if (Device.RuntimePlatform != Device.iOS)
            {
                Children.Add(new Location() { Title = "Location" });
                //Children.Add(new StatisticsPage() { Title = "Ride" });
               // Children.Add(new Crazy88() { Title = "Crazy88" });
                Children.Add(new Account() { Title = "Account" });
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

            //UpdateLocation();
            try
            {
                Device.StartTimer(TimeSpan.FromMinutes(3), () =>
                {
                    // call your method to check for notifications here
                    UpdateLocation();
                    // Returning true means you want to repeat this timer
                    return true;
                });
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
            } 
        }

        public async void UpdateLocation()
        {
            if (App.AllowTracking)
            {
                var locationhandling = new LocationHandling();
                var position = await locationhandling.GetCurrentLocation();

                if (position != null)
                {
                    var SendControl = await locationhandling.UpdateLocation(position);
                    if (!SendControl)
                    {
#if DEBUG
                        Debug.WriteLine("Couldn't update the location");
#endif
                        var answer = await DisplayAlert("Could not send the location", "Please check your connection", "Retry", "Cancel");
                        if (answer)
                        {
                            UpdateLocation();
                        }
                    }
                }
            }
        }
    }
}