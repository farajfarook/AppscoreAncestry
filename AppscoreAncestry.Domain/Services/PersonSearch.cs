using System;
using AppscoreAncestry.Common.Domain;
using AppscoreAncestry.Domain.Models.PersonAggregate;

namespace AppscoreAncestry.Domain.Services
{
    public class PersonSearch: ISearchModel<Person>
    {
        public string Name { get; set; }
        public PersonGender Gender { get; set; }
        public int? Skip { get; set; }
        public int? Take { get; set; }
    }
}
