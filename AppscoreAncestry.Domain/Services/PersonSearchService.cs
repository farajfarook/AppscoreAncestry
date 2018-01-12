using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppscoreAncestry.Common.Domain;
using AppscoreAncestry.Domain.Exceptions;
using AppscoreAncestry.Domain.Models.PersonAggregate;
using AppscoreAncestry.Domain.Models.PlaceAggregate;

namespace AppscoreAncestry.Domain.Services
{
    public class PersonSearchService : IPersonSearchService
    {
        private readonly IPersonRepository _repository;
        private readonly IPlaceRepository _placeRepository;

        public PersonSearchService(IPersonRepository repository, IPlaceRepository placeRepository)
        {
            _repository = repository;
            _placeRepository = placeRepository;
        }

        public async Task<PersonSearchResult> SearchAsync(PersonSearch search)
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
                    filteredData = !string.IsNullOrEmpty(search.Name) ? people.Where(m => m.Name.Contains(search.Name)) : people;
                    break;
            }

            var total = filteredData.Count();
            if (search.Genders?.Count > 0)
                filteredData = filteredData.Where(m => search.Genders.Contains(m.PersonGender));
            if (search.Skip > 0) 
                filteredData = filteredData.Skip(search.Skip ?? 0);
            if (search.Take > 0) 
                filteredData = filteredData.Take(search.Take ?? 10);

            var data = filteredData.ToList().Select(d =>
            {
                d.Place = _placeRepository.GetByIdAsync(d.PlaceId).Result;
                return d;
            }).ToList();
            
            return new PersonSearchResult(data, total, search.Skip, search.Take);
        }

        public IEnumerable<Person> ListAncestors(Person person)
        {
            var ancestors = new List<Person>();
            UpdateWithAncestors(person, ref ancestors);
            return ancestors;
        }

        private void UpdateWithAncestors(Person person, ref List<Person> ancestors)
        {
            var mother = (person.MotherId != null) ? _repository.GetByIdAsync(person.MotherId ?? -1).Result: null;
            var father = (person.FatherId != null) ? _repository.GetByIdAsync(person.FatherId ?? -1).Result : null;
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
