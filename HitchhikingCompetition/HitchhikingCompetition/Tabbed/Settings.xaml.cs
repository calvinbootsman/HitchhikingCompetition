using System;
using System.Collections.Generic;
using System.Diagnostics;
using Plugin.Geolocator;
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
            EnableDisableButtons(false);
            try
            {
                //button click, so only once
                var locationhandling = new LocationHandling();
                var position = await locationhandling.GetCurrentLocation();

                //If we get a value back which we can do something with
                if (position != null)
                {
                    var SendResult = await locationhandling.UpdateLocation(position);
                    if (!SendResult)
                    {
#if DEBUG
                        Debug.WriteLine("Couldn't update the location");
#endif
                        var answer = await DisplayAlert("Could not send the location", "Please check your connection", "Retry", "Cancel");
                        if (answer)
                        {
                          //  GetLocation();
                        }
                    }
                    else
                    {
                        Latitudelbl.Text = "Latitude: " + position.Latitude;
                        Longitudelbl.Text = "Longitude: " + position.Longitude;
                        Speedlbl.Text = "Speed: " + position.Speed;
#if DEBUG
                        Debug.WriteLine("Position Status: {0}", position.Timestamp);
                        Debug.WriteLine("Position Latitude: {0}", position.Latitude);
                        Debug.WriteLine("Position Longitude: {0}", position.Longitude);
#endif
                    }
                }
                else
                {
                    await DisplayAlert("Oh Oh", "We were not able to retrieve the location. Make sure it's enabled!","Ok");
                }
                EnableDisableButtons(true);
            }
            catch (Exception es)
            {
                await DisplayAlert("Error", es.Message, "Ok");
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

        private void TrackingSwitch_Toggled(object sender, ToggledEventArgs e)
        {
            if (TrackingSwitch.IsToggled)
            {
                Application.Current.Properties["AllowTracking"] = "true";
                App.AllowTracking = true;
            }
            else
            {
                Application.Current.Properties["AllowTracking"] = "false";
                App.AllowTracking = false;
            }
           
        }

        private void EnableDisableButtons(bool x)
        {
            Test.IsEnabled = x;
            RemoveCrazy.IsEnabled = x;
            LocationBtn.IsEnabled = x;
        }
    }
}