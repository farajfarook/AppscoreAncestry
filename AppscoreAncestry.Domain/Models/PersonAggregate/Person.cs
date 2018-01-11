using System;
using System.Collections.Generic;
using System.Text;
using AppscoreAncestry.Common.Domain;
using AppscoreAncestry.Domain.Models.PlaceAggregate;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace AppscoreAncestry.Domain.Models.PersonAggregate
{
    public class Person : IRootModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public PersonGender PersonGender { get; private set; }
        [JsonProperty("father_id")]        
        public int? FatherId { get; set; }
        [JsonProperty("mother_id")]
        public int? MotherId { get; set; }
        [JsonProperty("place_id")]
        public int PlaceId { get; set; }
        public Place Place { get; set; }
        public int Level { get; set; }
        
        public string Gender
        {
            get => PersonGender?.Id;
            set => PersonGender = PersonGender.FromValue<PersonGender>(value);
        }
    }
}
