using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppscoreAncestry.Common.Domain;
using AppscoreAncestry.Domain.Exceptions;
using AppscoreAncestry.Domain.Models.PersonAggregate;

namespace AppscoreAncestry.Domain.Services
{
    public class PersonSearchService : IPersonSearchService
    {
        private readonly IPersonRepository _repository;

        public PersonSearchService(IPersonRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Person>> SearchAsync(PersonSearch search)
        {            
            var people = await _repository.ListAsync();
            var filteredData = people.Where(p =>
                (p.Name == search.Name || string.IsNullOrEmpty(search.Name))
                ||
                (p.Gender == search.Gender.Id || search.Gender == null)
            ).Skip(search.Skip).Take(search.Take);
            return filteredData;
        }
    }
}
