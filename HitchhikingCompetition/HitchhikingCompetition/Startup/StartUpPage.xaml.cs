using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PCLStorage;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Diagnostics;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;

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
            App.TimerRunning = false;
            Permissionfunction();
            CheckIfLogedIn();

        }

        async void Permissionfunction()
        {
            try
            {
                var status = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Location);
                if (status != PermissionStatus.Granted)
                {
                    if (await CrossPermissions.Current.ShouldShowRequestPermissionRationaleAsync(Permission.Location))
                    {
                        await DisplayAlert("Need location", "Gunna need that location", "OK");
                    }

                    var results = await CrossPermissions.Current.RequestPermissionsAsync(Permission.Location);
                    //Best practice to always check that the key exists
                    if (results.ContainsKey(Permission.Location))
                        status = results[Permission.Location];

                    status  = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Location);

                    if (status == PermissionStatus.Granted)
                        {
                            App.AllowTracking = true;
                        }
                        else
                        {
                            App.AllowTracking = false;
                        }
                    
                }


            }
            catch (Exception ex)
            {

            }
        }

        async void CheckIfLogedIn()
        {            
            //Blijkbaar checken we eerst of we de locatie mogen versturen naar de website
            //Daarna checken we of we al weten of er is ingelogd. Dit gaat echter op de oude manier, 
            //dit moet nog veranderd worden, maar dat is voor later
            // ook gaan we niet naar LoginPage.xaml, maar naar MainPage.xaml. 
            if (Application.Current.Properties.ContainsKey("AllowTracking"))
            {
                App.AllowTracking = Convert.ToBoolean(Application.Current.Properties["AllowTracking"]);
            }
            else
            {
                App.AllowTracking = false;
            }
            //TODO: dit op een andere manier te doen. Waarom? Daarom!
            IFile file = await FileHandling.GetFile("Appdata", "login.txt");
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