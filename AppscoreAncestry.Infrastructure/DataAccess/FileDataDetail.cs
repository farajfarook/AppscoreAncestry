namespace AppscoreAncestry.Infrastructure.DataAccess
{
    public class FileDataDetail: IDataDetail
    {
        public string FileName { get; }

        public FileDataDetail(string filename)
        {
            FileName = filename;
        }
    }
}