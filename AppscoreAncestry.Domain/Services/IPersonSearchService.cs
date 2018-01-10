using System.Collections.Generic;
using System.Threading.Tasks;
using AppscoreAncestry.Domain.Models.PersonAggregate;

namespace AppscoreAncestry.Domain.Services
{
    public interface IPersonSearchService
    {
        Task<IEnumerable<Person>> ListAsync(PersonSearch search);
    }
}