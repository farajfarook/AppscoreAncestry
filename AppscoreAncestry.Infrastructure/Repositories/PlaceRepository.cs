using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AppscoreAncestry.Domain.Models.PlaceAggregate;

namespace AppscoreAncestry.Infrastructure.Repositories
{
    public class PlaceRepository: IPlaceRepository
    {
        public Task<Place> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Place>> ListAsync()
        {
            throw new NotImplementedException();
        }
    }
}
