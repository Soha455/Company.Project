namespace Company.Project.PL.Helpers
{
    public class DocumentSettings
    {
        // Upload 
        public static string UploadFile(IFormFile file ,string foldername) 
        {
            // 1.Get Folder Location
            var folderpath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\files", foldername);

            // 2.Get File Name & Make it unique
            var filename = $"{Guid.NewGuid()}{file.FileName}";

            // 3.Get File Path
            var filepath = Path.Combine(folderpath, filename);
            using var fileStream = new FileStream(filepath, FileMode.Create);
            file.CopyTo(fileStream);

            return filename;



            ////1.Get folder location
            ////string folderPath = "C:\\Users\\fatma\\source\\repos\\Company.Fatma01\\Company.Fatma01\\wwwroot\\files\\Images\\" + foldername;
            ////var folderPath =Directory.GetCurrentDirectory()+"\\wwwroot\\files" + foldername;
            //var folderPath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\files", foldername);
            ////2. get FileName and make it unique

            //var fileName = $"{Guid.NewGuid()}{file.FileName}";

            ////3.file path
            //var filepath = Path.Combine(folderPath, fileName);
            //using var filestream = new FileStream(filepath, FileMode.Create);
            //file.CopyTo(filestream);

            //return fileName;
        }

        // Download

        public static void DeleteFile(string filename, string foldername) 
        { 
            var filePath = Path.Combine(Directory.GetCurrentDirectory(),@"wwwroot\files",foldername, filename);
            if (File.Exists(filePath))
            {
                File.Delete(filePath);  
            }
        }
    }
}
