using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AppscoreAncestry.Common.Domain;

namespace AppscoreAncestry.Domain.Models.PersonAggregate
{
    public interface IPersonRepository: IRepository<Person>
    {        
        Task<Person> GetByIdAsync(int id);
        Task<IEnumerable<Person>> ListAsync(ISearchModel<Person> search);
        Task<IEnumerable<Person>> ListAsync();
    }
}
