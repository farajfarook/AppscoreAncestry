using AppscoreAncestry.Common.Domain;

namespace AppscoreAncestry.Domain.Models.PlaceAggregate
{
    public class Place: IRootModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
