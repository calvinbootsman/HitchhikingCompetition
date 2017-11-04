using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PCLStorage;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Diagnostics;

namespace HitchhikingCompetition
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class StartUpPage : ContentPage
    {
        public StartUpPage()
        {
            InitializeComponent();
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
           
            CheckIfLogedIn();
        }

        async void CheckIfLogedIn()
        {
            var data = new Data();
            var check = await data.ReadTheFile();
            while (check != 1) { };
            //First we check if theres a file with something in it. 
            //If there's nothing in it we go to the log in  
            IFile file = await FileHandling.GetFile("Appdata", "AllowTracking.txt");
            var temp = await file.ReadAllTextAsync();
            if (temp == "1") App.AllowTracking = true;
            else App.AllowTracking = false;
             file = await FileHandling.GetFile("Appdata", "login.txt");
            var username = await file.ReadAllTextAsync();
            if (username.Equals(""))
            {
                try
                {
                    Navigation.InsertPageBefore(new MainPage(), this);
                    await Navigation.PopToRootAsync();
                }
                catch(Exception e)
                {
                    Debug.WriteLine("{0} Exception found", e);
                }
            }

            //If there's something in it, we go to the main app.
            else
            {
                Debug.WriteLine("Username is: " + username);
                App.MainUsername = username;
                try { 
                Navigation.InsertPageBefore(new TabbedContent(), this);
                await Navigation.PopToRootAsync(); }
            catch(Exception e)
            {
                Debug.WriteLine("{0} Exception found", e);
                    
                    //await Navigation.PushAsync(new TabbedContent());
            }
            }
        }
    }
}