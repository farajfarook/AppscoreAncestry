using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AppscoreAncestry.Domain.Seedwork;

namespace AppscoreAncestry.Domain.Models.PersonAggregate
{
    public interface IPersonRepository: IRepository<Person>
    {
        Task<Person> GetById(int id);
    }
}
