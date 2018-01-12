using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppscoreAncestry.Common.Domain;
using AppscoreAncestry.Domain.Exceptions;
using AppscoreAncestry.Domain.Models;
using AppscoreAncestry.Domain.Models.PersonAggregate;
using AppscoreAncestry.Domain.Models.PlaceAggregate;
using Microsoft.Extensions.Options;

namespace AppscoreAncestry.Domain.Services
{
    public class PersonSearchService : IPersonSearchService
    {
        private readonly IPersonRepository _repository;
        private readonly IPlaceRepository _placeRepository;
        private readonly int _advanceSearchLimit;

        public PersonSearchService(IPersonRepository repository, IPlaceRepository placeRepository, IOptions<Settings> options)
        {
            _repository = repository;
            _placeRepository = placeRepository;
            _advanceSearchLimit = options.Value.AdvanceSearchLimit;
        }

        public async Task<PersonSearchResult> SearchAsync(PersonSearch search)
        {            
            var people = await _repository.ListAsync();
            var currentPerson = people.FirstOrDefault(p => string.Equals(p.Name, search.Name, StringComparison.CurrentCultureIgnoreCase));
            IEnumerable<Person> filteredData = new List<Person>();
            switch (search.Mode)
            {
                case PersonSearch.SearchMode.Ancestors:
                    filteredData = ListAncestors(currentPerson);
                    filteredData = filteredData.Take(10);
                    break;
                case PersonSearch.SearchMode.Descendants:
                    filteredData = ListDescendants(currentPerson);
                    filteredData = filteredData.Take(10);
                    break;
                default:
                    filteredData = !string.IsNullOrEmpty(search.Name) ? people.Where(m => m.Name.ToLower().Contains(search.Name.ToLower())) : people;
                    break;
            }
            if (search.Genders?.Count > 0)
                filteredData = filteredData.Where(m => search.Genders.Contains(m.PersonGender));

            var total = filteredData.Count();

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
            if (person != null) UpdateWithAncestors(person, ref ancestors);
            return ancestors;
        }

        private void UpdateWithAncestors(Person person, ref List<Person> ancestors)
        {
            if (ancestors.Count > _advanceSearchLimit) return;

            var mother = (person.MotherId != null) ? _repository.GetByIdAsync(person.MotherId ?? -1).Result : null;
            var father = (person.FatherId != null) ? _repository.GetByIdAsync(person.FatherId ?? -1).Result : null;

            if (mother != null) ancestors.Add(mother);
            if (father != null) ancestors.Add(father);

            if (mother != null) UpdateWithAncestors(mother, ref ancestors);
            if (father != null) UpdateWithAncestors(father, ref ancestors);
        }

        public IEnumerable<Person> ListDescendants(Person person)
        { 
            var descendants = new List<Person>();
            if (person != null) UpdateWithDescendants(person, ref descendants);
            return descendants;
        }

        private void UpdateWithDescendants(Person person, ref List<Person> descendants)
        {
            if (descendants.Count > _advanceSearchLimit) return;

            var children = _repository.ListChildrenAsync(person.Id).Result;
            descendants.AddRange(children);
            foreach (var child in children)
            {
                UpdateWithDescendants(child, ref descendants);
            }
        }
    }
}
