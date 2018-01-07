using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Diagnostics;
using PCLStorage;

namespace HitchhikingCompetition
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Crazy88 : ContentPage
    {
        public Crazy88()
        {
            InitializeComponent();
            Crazy88List.ItemsSource = Data.crazy88list;
        }

        //MakeUpToDate will be called when there's a change in the accomplished task from the crazy88.
        //
        async void MakeUpToDate()
        {
            string text = "";
           
                foreach (Crazy88Assignments dinges in Data.crazy88list)
                {
#if DEBUG
                Debug.WriteLine("Item: " + dinges.Item + " ID: " + dinges.IsChecked);
                    text += dinges.Item + "*" + dinges.Description + "*" + dinges.Points + "*" + dinges.IsChecked + ";";
#endif
            }

                try
                {
               //We will rewrite the file
                IFile file = await FileHandling.GetFile("Crazy88Data", "AssignmentList.txt");
                await file.WriteAllTextAsync(text);
            
            }
			catch (Exception e)
			{
				await DisplayAlert("Updaten 2", e.ToString(), "Ok");
			}
        }
    }
}