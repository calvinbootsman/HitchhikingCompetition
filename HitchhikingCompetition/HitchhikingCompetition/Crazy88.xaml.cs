using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Diagnostics;
using PCLStorage;
//COVERED!
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

        async void Updaten()
        {
            string text = "";
           
                foreach (Crazy88Assignments dinges in Data.crazy88list)
                {
                    Debug.WriteLine("Item: " + dinges.Item + " ID: " + dinges.IsChecked);
                    text += dinges.Item + "*" + dinges.Description + "*" + dinges.Points + "*" + dinges.IsChecked + ";";
                }
            try
            {
                IFile file = await FileHandling.getFile("Crazy88Data", "AssignmentList.txt");
                await file.WriteAllTextAsync(text);
            
            }
			catch (Exception e)
			{
				await DisplayAlert("Updaten 2", e.ToString(), "Ok");
			}
        }
    }
}