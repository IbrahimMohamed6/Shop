using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.BLL.Common.Attachements
{
    public class Attachement : IAttachement
    {
        //private readonly List<string> _AllowedExtension = new() { ".png", ".jpg", ".jpeg" };
        //private const int _AllowedMAxSize = 2_233_333;
        public string Uplod(IFormFile file, string foldername)
        {
            //var Extension = Path.GetExtension(file.FileName);
            //if (!_AllowedExtension.Contains(Extension))
            //    return null;
            //if (file.Length > _AllowedMAxSize)
            //    return null;
            var FolderPathe=Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Fils",foldername);
            if(!File.Exists(FolderPathe))
                Directory.CreateDirectory(FolderPathe);
            var FileName = $"{Guid.NewGuid()}-{file.FileName}";
            var FilePathe=Path.Combine(FolderPathe,FileName);//Create File Location
            var FileStream = new FileStream(FilePathe, FileMode.Create);
            file.CopyTo(FileStream);
            return FileName;


           
        }

        public bool Delete(string PathFolder)
        {
            if (File.Exists(PathFolder))
            {
                File.Delete(PathFolder);
                return true;
            }
            return false;
        }
    }
}
