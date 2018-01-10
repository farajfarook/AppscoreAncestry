using AppscoreAncestry.Common.Domain;

namespace AppscoreAncestry.Domain.Models.PersonAggregate
{
    public class PersonGender: Enumeration<string>
    {
        public PersonGender()
        {
        }

        public PersonGender(string id, string name): base(id, name)
        {
        }

        public static PersonGender Male = new PersonGender("M", nameof(Male));
        public static PersonGender Female = new PersonGender("F", nameof(Female));
        public static PersonGender Other = new PersonGender("O", nameof(Other));
    }
}