using System;
using System.Collections.Generic;
using System.Text;
using AppscoreAncestry.Common.Domain;
using AppscoreAncestry.Domain.Models.PersonAggregate;

namespace AppscoreAncestry.Infrastructure.Models
{
    public class PersonSearchModel: ISearchModel<Person>
    {
        public string Name { get; set; }
        public PersonGender Gender { get; set; }
    }
}
