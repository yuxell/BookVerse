namespace BookWebApp.Business.Utilities
{
    public static class Uploads
    {
        public static string SaveImage(IFormFile imageFile)
        {
            string fileName = Guid.NewGuid().ToString() + "_" + imageFile.FileName;
            string fileNameWithPath = "wwwroot/img/books/" + fileName;

            FileStream fs = new FileStream(fileNameWithPath, FileMode.Create);
            imageFile.CopyTo(fs);
            fs.Close();

            return fileName;
        }
    }
}
