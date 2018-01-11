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
            var currentPerson = people.SingleOrDefault(p => string.Equals(p.Name, search.Name, StringComparison.CurrentCultureIgnoreCase));
            IEnumerable<Person> filteredData = new List<Person>();
            switch (search.Mode)
            {
                case PersonSearch.SearchMode.Ancestors:
                    filteredData = ListAncestors(currentPerson);
                    break;
                case PersonSearch.SearchMode.Descendants:
                    filteredData = ListDescendants(currentPerson);
                    break;
                default:
                    if (!string.IsNullOrEmpty(search.Name)) 
                        filteredData = people.Where(m => m.Name.Contains(search.Name));
                    break;
            }            
            if (search.Genders?.Count > 0)
                filteredData = filteredData.Where(m => search.Genders.Contains(m.PersonGender));
            if (search.Skip > 0) 
                filteredData = filteredData.Skip(search.Skip ?? 0);
            if (search.Take > 0) 
                filteredData = filteredData.Take(search.Take ?? 10);
            return filteredData.ToList();
        }

        public IEnumerable<Person> ListAncestors(Person person)
        {
            var ancestors = new List<Person>();
            UpdateWithAncestors(person, ref ancestors);
            return ancestors;
        }

        private void UpdateWithAncestors(Person person, ref List<Person> ancestors)
        {
            var mother = (person.MotherId != null) ? _repository.GetByIdAsync(person.MotherId.Value).Result: null;
            var father = (person.FatherId != null) ? _repository.GetByIdAsync(person.FatherId.Value).Result : null;
            if (mother != null)
            {
                ancestors.Add(mother);
                UpdateWithAncestors(mother, ref ancestors);
            }
            if (father != null)
            {
                ancestors.Add(father);
                UpdateWithAncestors(father, ref ancestors);
            }
        }

        public IEnumerable<Person> ListDescendants(Person person)
        {
            var descendants = new List<Person>();
            UpdateWithDescendants(person, ref descendants);
            return descendants;
        }

        private void UpdateWithDescendants(Person person, ref List<Person> descendants)
        {
            var children = _repository.ListChildrenAsync(person.Id).Result;
            descendants.AddRange(children);
            foreach (var child in children)
            {
                UpdateWithDescendants(child, ref descendants);
            }            
        }
    }
}
