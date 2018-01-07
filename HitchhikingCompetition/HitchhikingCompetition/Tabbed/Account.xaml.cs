using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HitchhikingCompetition
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Account : ContentPage
    {
        public Account()
        {
            InitializeComponent();
            int MoodLimit = 50;
            int MessageLimit = 140;

            MoodEntry.TextChanged += (sender, args) =>
            {
                string _text = MoodEntry.Text;      //Get Current Text
                if (_text.Length > MoodLimit)       //If it is more than your character restriction
                {
                    _text = _text.Remove(_text.Length - 1);  // Remove Last character
                    MoodEntry.Text = _text;        //Set the Old value
                }
            };

            MessageEntry.TextChanged += (sender, args) =>
            {
                string _text = MessageEntry.Text;      //Get Current Text
                if (_text.Length > MessageLimit)       //If it is more than your character restriction
                {
                    _text = _text.Remove(_text.Length - 1);  // Remove Last character
                    MessageEntry.Text = _text;        //Set the Old value
                }
            };
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            EnableDisabe(false);
            try { 
                var client = new System.Net.Http.HttpClient();
                var uri = new Uri("http://trickingnederland.nl/lift/Liftwedstrijd.php");

                //Post request vormen:
                var str = new FormUrlEncodedContent(new[]
                {
                    new KeyValuePair<string, string>("loginUsername", App.MainUsername),
                    new KeyValuePair<string, string>("mood", MoodEntry.Text),
                    new KeyValuePair<string, string>("message", MessageEntry.Text),
                });

                //Waardes doorsturen:
                var text = await client.PostAsync(uri, str);
                Debug.WriteLine("received: " + text.ToString());
            }
            catch (Exception) { }
            EnableDisabe(true);
        }

        private void EnableDisabe(bool x)
        {
            Activity.IsRunning = !x;
            MoodEntry.IsEnabled = x;
            MessageEntry.IsEnabled = x;
            SendButton.IsEnabled = x;
        }
    }
}