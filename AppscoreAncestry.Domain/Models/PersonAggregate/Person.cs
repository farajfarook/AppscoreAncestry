using System;
using System.Collections.Generic;
using System.Text;
using AppscoreAncestry.Common.Domain;

namespace AppscoreAncestry.Domain.Models.PersonAggregate
{
    public class Person : IRootModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public PersonGender PersonGender { get; private set; }
        public int FatherId { get; set; }
        public int MotherId { get; set; }
        public int PlaceId { get; set; }
        public int Level { get; set; }

        public string Gender
        {
            get => PersonGender?.Id;
            set => PersonGender = PersonGender.FromValue<PersonGender>(value);
        }
    }
}
