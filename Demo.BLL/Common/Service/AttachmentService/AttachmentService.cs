using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Demo.BLL.Common.Service.AttachmentService
{
    public class AttachmentService : IAttachmentService
    {
        //Allowed extensions {.Png, .Jgp, .jpeg}
        public readonly List<string> _allowedextensions = new List<string>() { ".png", ".jgp", ".jpeg" };


        //Max size of file = 2MB
        public const int _maxAllowedSize = 2_097_152;
        public async Task<string?> UploadAsync(IFormFile file, string FolderName)
        {

            //1] Validate for Extensions { ".png", ".jgp", ".jpeg"}

            var extension = Path.GetExtension(file.FileName);
            if (!_allowedextensions.Contains(extension))
                return null;

            //2]Validate on MaxSize
            if (file.Length > _maxAllowedSize)
            {
                return null;

            }

            //3] Locate Folder path of the file
            //var FolderPath  = "D:\\2- programming\\Route\\Dot Net\\01-Demos\\06-ASP.NETCORE\\ASP.NET Core-MVC-Project\\Asp-MVC\\Demo.PL\\wwwroot\\files\\images\\"
            var FolderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\files",  FolderName);

            //4] Set Unique File Name
            var fileName = $"{Guid.NewGuid()}{extension}";

            //5] Get File path[FolderName + FileName] 
            var filePath = Path.Combine(FolderPath, fileName);

            //6]Save file as a stream [Data per time

            using var fileStream = new FileStream(filePath, FileMode.Create);

            //7] Copy file to the stream
           await file.CopyToAsync(fileStream);


            //8] return fileName
            return fileName;
            /*
            

            1]You open the Image File
            FileStream establish a Connection between your program and the image file.
            the file is opened in read, write, or both modes
            
            2] you read or write binary data

            3]You close the file after use

            the using keyword ensure your file proper closure
            
 
 
             */


            /*
             
             FileMode.Create: Creates a new file. Overwrites if it exists.

            FileMode.Open: Opens existing file.
            FileMode.Append: Open or create file if not exist, data is written at the end of the file .
            FileMode.turncate: Opens existing file and clear its content.
      

            FileAccess.Write: Grants write access.

            FileAccess.Read: Grants read access.

            FileShare.None: File cannot be shared.
             
             
             */

        }
        public bool Delete(string filePath)
        {
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
                return true;
                
            }
            return false;
        }


    }
}
