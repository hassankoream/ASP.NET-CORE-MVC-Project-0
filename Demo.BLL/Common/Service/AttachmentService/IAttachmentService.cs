using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Demo.BLL.Common.Service.AttachmentService
{
    public interface IAttachmentService
    {
        //Upload, Delete


        public Task<string?> UploadAsync(IFormFile file, string FilePath);

        public bool Delete(string filePath);
    }
}
