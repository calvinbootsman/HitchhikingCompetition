using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PCLStorage;
namespace HitchhikingCompetition
{
    class FileHandling
    {
        async public static Task<IFile> getFile(string Folder, string File)
        {
            IFolder rootFolder = FileSystem.Current.LocalStorage;
            IFolder folder = await rootFolder.CreateFolderAsync(Folder,
                CreationCollisionOption.OpenIfExists);
            IFile file = await folder.CreateFileAsync(File,
                CreationCollisionOption.OpenIfExists);
            return file;
        }

        async public Task<string> readline(string Folder, string File, int line)
        {
            var file = await getFile(Folder, File);
            var output = file.ReadAllTextAsync();
            return "";
        }

        async public void InitAppSettings()
        {
            var file = await getFile("AppSettings", "Settings");
            var FileText = await file.ReadAllTextAsync();
            if (FileText == "")
            {
                await file.WriteAllTextAsync("Username=#Tracking=#LastLongitude=#LastLatitude=#Mood=#Message=");
                App.MainUsername = "";
            }
            else
            {
                var TextSplitted = FileText.Split('#');
                App.MainUsername = TextSplitted[0].Split('=')[1];
            }
        }

        async public void AddData(string data, int position)
        {
            var file = await getFile("AppSettings", "Settings");
            var FileText = await file.ReadAllTextAsync();
            if (FileText == "")
            {
            }
            else
            {
                var TextSplitted = FileText.Split('#');
                var KeyAndValue = TextSplitted[position].Split('=');
                TextSplitted[position] = KeyAndValue[0] + "=" + KeyAndValue[1];
                FileText = "";
                foreach(string x in TextSplitted)
                {
                    FileText += x;
                }
                await file.WriteAllTextAsync(FileText);
            }
        }
    }
}
