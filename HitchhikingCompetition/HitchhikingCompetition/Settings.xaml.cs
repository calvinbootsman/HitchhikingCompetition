using System;
using System.Collections.Generic;
using System.Diagnostics;
using Plugin.Geolocator;
using System.Net.Http;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using PCLStorage;

namespace HitchhikingCompetition
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Settings : ContentPage
    {
        public Settings()
        {
            InitializeComponent();
            TrackingSwitch.IsToggled = App.AllowTracking;
        }

        async public void GetLocation(object sender, EventArgs e)
        {
            if (App.TimerRunning == false)
            {
                App.TimerRunning = true;
                try
                {
                    if (App.AllowTracking)
                    {
                        var locator = CrossGeolocator.Current;
                    locator.DesiredAccuracy = 100;

                    var position = await locator.GetPositionAsync(30000);
                    var latitude = position.Latitude;
                    Latitudelbl.Text = "Latitude: " + position.Latitude;
                    Longitudelbl.Text = "Longitude: " + position.Longitude;
                    Speedlbl.Text = "Speed: " + position.Speed;
#if DEBUG
                    Debug.WriteLine("Position Status: {0}", position.Timestamp);
                    Debug.WriteLine("Position Latitude: {0}", position.Latitude);
                    Debug.WriteLine("Position Longitude: {0}", position.Longitude);
#endif
                    var client = new System.Net.Http.HttpClient();
                    var uri = new Uri("http://trickingnederland.nl/lift/Liftwedstrijd.php");

                    //Post request vormen:
                    var str = new FormUrlEncodedContent(new[]
                    {
                        new KeyValuePair<string, string>("loginUsername", App.MainUsername),
                        new KeyValuePair<string, string>("latitude", position.Latitude.ToString()),
                        new KeyValuePair<string, string>("longitude", position.Longitude.ToString()),
                        new KeyValuePair<string, string>("speed", position.Speed.ToString())
                    });

                    //Waardes doorsturen:
                    await client.PostAsync(uri, str);
                    
                        Device.StartTimer(TimeSpan.FromMinutes(1), () =>
                        {
                            // call your method to check for notifications here
                            App.TimerRunning = false;
                            // Returning true means you want to repeat this timer
                            return false;
                        });
                    }
                }

                catch (Exception) { }
            }
        }

        async void TestIsClicked(object sender, EventArgs e)
        {
            try
            {
                //here we want to log out. This will create an error, but it's okay for now.
                var file = await FileHandling.GetFile("Appdata", "login.txt");
                await file.DeleteAsync();
                Navigation.InsertPageBefore(new MainPage(), this);
                await Navigation.PopAsync();
            }
			catch (Exception es)
			{
                //todo make sure this error doensn't happen
				await DisplayAlert("Error", es.ToString(), "Ok");
			}
        }
        async void RemoveList(object sender, EventArgs e)
        {
            try
            {
                var file = await FileHandling.GetFile("Crazy88Data", "AssignmentList.txt");
                await file.DeleteAsync();
            }
			catch (Exception es)
			{
				await DisplayAlert("Error", es.ToString(), "Ok");
			}
        }

        private async void TrackingSwitch_Toggled(object sender, ToggledEventArgs e)
        {
            if (TrackingSwitch.IsToggled)
            {
                try
                {
                    IFile file = await FileHandling.GetFile("Appdata", "AllowTracking.txt");
                    await file.WriteAllTextAsync("1");
                    Device.StartTimer(TimeSpan.FromMinutes(3), () =>
                    {

                        // call your method to check for notifications here
                        var tabbed = new TabbedContent();
                        tabbed.UpdateLocation();
                        // Returning true means you want to repeat this timer
                        return App.AllowTracking;
                    });
                    
                    App.AllowTracking = true;
                }
                catch (Exception es) { }
            }
            else
            {
                IFile file = await FileHandling.GetFile("Appdata", "AllowTracking.txt");
                await file.WriteAllTextAsync("0");
                App.AllowTracking = false;
            }
        }
    }
}