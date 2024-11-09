using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.BLL.Common.Attachements
{
    public interface IAttachement
    {
        string Uplod(IFormFile file, string foldername);
        bool Delete(string PathFolder);
    }
}
