using AppscoreAncestry.Domain.Models.PersonAggregate;
using AppscoreAncestry.Domain.Models.PlaceAggregate;

namespace AppscoreAncestry.Web.Models
{
    public class PersonViewModel
    {
        public PersonViewModel()
        {
            
        }
        public PersonViewModel(Person person, Place place)
        {
            Id = person.Id;
            Name = person.Name;
            Gender = person.PersonGender.Name;
            BirthPlace = place.Name;
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Gender { get; set; }
        public string BirthPlace { get; set; }
    }
}