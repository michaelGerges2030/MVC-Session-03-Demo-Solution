using Microsoft.AspNetCore.Http;
using System;
using System.IO;

namespace MVC_Session_03_Demo.Helpers
{
    public static class DocumentSettings
    {
        
        public static string UploadFile(IFormFile file, string folderName)
        {
            //string folderPath = $"E:\\Desktop\\Route Academy\\Full Stack\\02 BackEnd\\MVC\\MVC Session 03\\MVC Session 03 Demo Solution\\MVC Session 03 Demo\\wwwroot\\files\\{FolderName}";
            //string folderPath = $"{Directory.GetCurrentDirectory()}\\wwwroot\\files\\{FolderName}";
            string folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\files", folderName);


            if (!Directory.Exists(folderPath))
                Directory.CreateDirectory(folderPath);

            string fileName = $"{Guid.NewGuid()}{file.FileName}";

            string filePath = Path.Combine(folderPath, fileName);

            using var fileStream = new FileStream(filePath, FileMode.Create);

            file.CopyTo(fileStream);

            return fileName;
        }

        public static void DeleteFile(string fileName, string folderName)
        {
            string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\files", folderName, fileName);
            if (File.Exists(filePath))
                File.Delete(filePath);
        }

    }
}
