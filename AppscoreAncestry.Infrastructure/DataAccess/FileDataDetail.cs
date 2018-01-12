using AppscoreAncestry.Domain.Models;
using AppscoreAncestry.Infrastructure.Config;
using Microsoft.Extensions.Options;

namespace AppscoreAncestry.Infrastructure.DataAccess
{
    public class FileDataDetail: IDataDetail
    {
        public string FileName { get; }

        public FileDataDetail(IOptions<Settings> options)
        {
            var settings = options.Value;
            FileName = settings.DataConnectionString;
        }

        internal FileDataDetail(string fileName)
        {
            FileName = fileName;
        }
    }
}