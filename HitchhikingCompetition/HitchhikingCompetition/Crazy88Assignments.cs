using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using PCLStorage;
using System.Diagnostics;
using System.Net.Http;

namespace HitchhikingCompetition
{
	class Crazy88Assignments
	{
		public string Item { get; set; }
		public string Description { get; set; }
		public string Points { get; set; }
		public bool IsChecked { get; set; }
	}
	class Data
	{
        //In this collection we will put every assignment. 
		public static ObservableCollection<Crazy88Assignments> crazy88list = new ObservableCollection<Crazy88Assignments>();

        //We will get the assignements from a file.
        //If this file doesn't exist, we will get all the data from the file from the webs. 
		public async Task<int> ReadTheFile()
		{
			//First we make the folder
			string Folder = "Crazy88Data";
			string File = "AssignmentList.txt";
			IFolder rootFolder = FileSystem.Current.LocalStorage;
			IFolder folder = await rootFolder.CreateFolderAsync(Folder,
				CreationCollisionOption.OpenIfExists);

			//Then we're going to check if the file exist
			var fileExistance = folder.CheckExistsAsync(File);
			var fileexist = ExistenceCheckResult.FileExists;

			//File exists
			if (fileExistance.Result == fileexist)
			{
				Debug.WriteLine("File does exist!");
				IFile file = await folder.CreateFileAsync(File,
					CreationCollisionOption.OpenIfExists);
				MakeCollection(file);
				// await file.DeleteAsync();
			}

			//File doesn't exists
			else
			{
				Debug.WriteLine("File doesn't exist!");
				IFile file = await folder.CreateFileAsync(File,
					CreationCollisionOption.OpenIfExists);

				//Getting the file from the website
				var client = new HttpClient();
				var uri = new Uri("http://trickingnederland.nl/lift/Assignment.php");
				var response = await client.GetAsync(uri);
				var placesJson = response.Content.ReadAsStringAsync().Result;
				string replaceWith = "";
				placesJson = placesJson.Replace("\r\n", replaceWith).Replace("\n", replaceWith).Replace("\r", replaceWith);

				//Make the file and make it into a oberservablecollection
				await file.WriteAllTextAsync(placesJson);
				MakeCollection(file);

			}
			return 1;

		}

		async void MakeCollection(IFile File)
		{
			var text = await File.ReadAllTextAsync();
			try
			{
				string[] DataArray = text.Split(';');

				foreach (string seperated in DataArray)
				{
					
					string[] stringcollection = seperated.Split('*');
					Debug.WriteLine("Lets see if it works: " + stringcollection[1]);
					crazy88list.Add(new Crazy88Assignments { Item = stringcollection[0], Description = stringcollection[1], Points = stringcollection[2], IsChecked = Convert.ToBoolean(stringcollection[3]) });
#if DEBUG
                    Debug.WriteLine(seperated);
                    Debug.WriteLine("Lets see if it works: " + stringcollection[1]);
#endif
                }
			}
			catch (Exception e)
			{
				Debug.WriteLine(e);
			}
		}
	}

}
