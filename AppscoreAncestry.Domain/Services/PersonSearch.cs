using System;
using System.Collections.Generic;
using AppscoreAncestry.Common.Domain;
using AppscoreAncestry.Domain.Models.PersonAggregate;

namespace AppscoreAncestry.Domain.Services
{
    public class PersonSearch: ISearchModel<Person>
    {

        public PersonSearch()
        {
            
        }

        public PersonSearch(string name, SearchMode mode, uint page, uint pagesize)
        {
            Name = name;
            Genders = new List<PersonGender>();
            Mode = mode;
            Skip = ((int) page - 1) * (int) pagesize;
            Take = (int) pagesize;
        }

        public enum SearchMode
        {
            Default,
            Ancestors,
            Descendants
        }
        
        public string Name { get; set; }
        public List<PersonGender> Genders { get; set; }
        public int? Skip { get; set; }
        public int? Take { get; set; }
        public SearchMode Mode { get; set; } = SearchMode.Default;
    }
}
