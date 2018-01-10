using System.Collections.Generic;
using System.Threading.Tasks;
using AppscoreAncestry.Common.Domain;

namespace AppscoreAncestry.Domain.Models.PlaceAggregate
{
    public interface IPlaceRepository: IRepository<Place>
    {
        Task<Place> GetById(int id);
        Task<IEnumerable<Place>> ListAsync();
    }
}