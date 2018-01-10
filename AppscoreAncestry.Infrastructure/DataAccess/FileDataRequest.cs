namespace AppscoreAncestry.Infrastructure.DataAccess
{
    public class FileDataRequest : IDataRequest
    {
        public string FileName { get; }

        public FileDataRequest(string fileName)
        {
            FileName = fileName;
        }
    }
}