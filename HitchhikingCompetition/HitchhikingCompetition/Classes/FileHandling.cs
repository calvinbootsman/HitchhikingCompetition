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
        async public static Task<IFile> GetFile(string Folder, string File)
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
            var file = await GetFile(Folder, File);
            var output = file.ReadAllTextAsync();
            return "";
        }
    }
}
