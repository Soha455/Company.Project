namespace Company.Project.PL.Helpers
{
    public static class DocumentSettings
    {
        // Upload 
        public static string UploadFile(IFormFile file, string foldername)
        {
            // 1.Get Folder Location
            var folderPath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\Files", foldername);

            // 2.Get File Name & Make it unique
            var fileName = $"{Guid.NewGuid()}{file.FileName}";

            // 3.Get File Path
            var filePath = Path.Combine(folderPath, fileName);
            using var filestream = new FileStream(filePath, FileMode.Create);
            file.CopyTo(filestream);

            return fileName;
        }

        // Download
        public static void DeleteFile(string filename, string foldername)
        {
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\Files", foldername, filename);
            if (File.Exists(filePath))
                File.Delete(filePath);
        }
    }
}
