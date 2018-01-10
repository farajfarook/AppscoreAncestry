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
                (p.Name.Contains(search.Name) || string.IsNullOrEmpty(search.Name))
                &&
                (p.Gender == search.Gender?.Id || search.Gender == null)
            );
            if (search.Skip != null) filteredData = filteredData.Skip(search.Skip ?? 0);
            if (search.Take != null) filteredData = filteredData.Take(search.Take ?? 10);
            return filteredData.ToList();
        }
    }
}
