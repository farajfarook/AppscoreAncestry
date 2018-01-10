using System.IO;
using System.Threading.Tasks;
using AppscoreAncestry.Domain.Exceptions;

namespace AppscoreAncestry.Infrastructure.DataAccess
{
    public class FileDataAccess : IDataAccess
    {
        public Task<IDataResult> FetchAsync(IDataRequest request)
        {
            if(!(request is FileDataRequest)) throw new DomainException("Invalid data request");
            var fileReq = (FileDataRequest) request;
            var content = File.ReadAllText(fileReq.FileName);
            var result = new FileDataResult(content);
            return Task.FromResult((IDataResult) result);
        }
    }
}