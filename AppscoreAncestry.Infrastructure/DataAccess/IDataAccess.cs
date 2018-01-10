using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AppscoreAncestry.Infrastructure.DataAccess
{
    public interface IDataAccess
    {
        Task<IDataResult> FetchAsync(IDataRequest request);
    }
}
