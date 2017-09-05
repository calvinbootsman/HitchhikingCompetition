using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using Plugin.Geolocator;
using System.Net.Http;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HitchhikingCompetition
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Settings : ContentPage
    {
        public Settings()
        {
            InitializeComponent();
        }
        async void TestIsClicked(object sender, EventArgs e)
        {
            var file = await FileHandling.getFile("InlogFolder", "login.txt");
            await file.DeleteAsync();
            Navigation.InsertPageBefore(new MainPage(), this);
            await Navigation.PopToRootAsync();
        }
        async void RemoveList()
        {
            var file = await FileHandling.getFile("Crazy88Data", "AssignmentList.txt");
            await file.DeleteAsync();
        }
        async public void GetLocation()
        {
            try
            {
                var locator = CrossGeolocator.Current;
                locator.DesiredAccuracy = 50;

                var position = await locator.GetPositionAsync(10000);
                var latitude = position.Latitude;
                Latitudelbl.Text = "Latitude: " + position.Latitude;
                Longitudelbl.Text = "Longitude: " + position.Longitude;
                Speedlbl.Text = "Speed: " + position.Speed;
                Debug.WriteLine("Position Status: {0}", position.Timestamp);
                Debug.WriteLine("Position Latitude: {0}", position.Latitude);
                Debug.WriteLine("Position Longitude: {0}", position.Longitude);

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
            }
            catch (Exception) { }
        }
    }
}