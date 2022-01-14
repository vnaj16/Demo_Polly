namespace Demo_Polly.Services
{
    public class FileReaderService
    {
        public string ReadFile()
        {
            string filePath = @"G:\Repositorios GitHub\Demo_Polly\Demo_Polly\filtetoread.txt";

            return File.ReadAllText(filePath);
        }
    }
}
