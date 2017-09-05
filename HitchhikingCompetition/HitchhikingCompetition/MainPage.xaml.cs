using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Http;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.Diagnostics;
using PCLStorage;

namespace HitchhikingCompetition
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            Debug.WriteLine("Hoi");
             
        }
        
        async void LoginClicked(object sender, EventArgs e)
        {
            var username = LoginUsername.Text;
            var password = LoginPassword.Text;
            var received = await PostInlog(username, password);     //send the typed in data to the server and waits for response
            var temp = "1";
            
            if (String.Compare(received, temp)+1 == 1)        //fucking C# with its string comparison: Checks if we got the user in the database
            {
                //Write the username in a file and to a variable
                var file = await FileHandling.getFile("InlogFolder", "login.txt");
                await file.WriteAllTextAsync(username);
                App.MainUsername = username;

                //Go to the next page
                Navigation.InsertPageBefore(new TabbedContent(), this);
                await Navigation.PopToRootAsync();
            }
            else
            {
                WrongCode.Text = "Login is not vallid";
            } 
        }

        async Task<string> PostInlog(string username, string password)
        {
            //Client intitialiseren:
            var client = new System.Net.Http.HttpClient();
            var uri = new Uri("http://trickingnederland.nl/lift/Liftwedstrijd.php");

            //Post request vormen:
            var str = new FormUrlEncodedContent(new[]
            {
                    new KeyValuePair<string, string>("loginUsername", username),
                    new KeyValuePair<string, string>("loginPassword", password)
                });

            //Waardes doorsturen:
            var response = await client.PostAsync(uri, str);
            var placesJson = response.Content.ReadAsStringAsync().Result;
            string replaceWith = "";
            placesJson = placesJson.Replace("\r\n", replaceWith).Replace("\n", replaceWith).Replace("\r", replaceWith);
            Debug.WriteLine("Dit is er ontvangen: " + placesJson);
            return placesJson;
        }
    }
}
