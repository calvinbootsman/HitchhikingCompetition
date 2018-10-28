using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Plugin.Geolocator;
using Plugin.Geolocator.Abstractions;
using System.Threading.Tasks;
using System.Net.Http;
using System.Diagnostics;

namespace HitchhikingCompetition
{
    class LocationHandling
    {
        public async Task<Position> GetCurrentLocation()
        {
            Position position = null;
            try
            {
               var locator = CrossGeolocator.Current;
                locator.DesiredAccuracy = 100;

              //  position = await locator.GetLastKnownLocationAsync();

               /* if (position != null)
                {
                    //got a cached position, so let's use it.
                    return position;
                }
                */
                if (!locator.IsGeolocationAvailable || !locator.IsGeolocationEnabled)
                {
                    //not available or enabled
                    return position;
                }

                position = await locator.GetPositionAsync(TimeSpan.FromSeconds(20), null, true);

            }
            catch (Exception ex)
            {
             //THat   Debug.WriteLine("ERROR: " + ex.Message);
                position = null;
                return position;
            }

                return position;
        }


    public async Task<bool> UpdateLocation(Position position)
        {
            try
            {
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
                Application.Current.Properties["LastUpdate"] = position.Timestamp;
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }
}
