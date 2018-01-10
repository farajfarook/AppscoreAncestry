using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppscoreAncestry.Domain.Models.PlaceAggregate;
using AppscoreAncestry.Infrastructure.DataAccess;

namespace AppscoreAncestry.Infrastructure.Repositories
{
    public class PlaceRepository: IPlaceRepository
    {
        private const string Name = "places";

        private readonly IDataAccess _dataAccess;

        public PlaceRepository(IDataAccess dataAccess)
        {
            _dataAccess = dataAccess;
        }

        public async Task<Place> GetById(int id)
        {
            var places = await ListAsync();
            return places?.SingleOrDefault(p => p.Id == id);
        }

        public async Task<IEnumerable<Place>> ListAsync()
        {
            var data = await _dataAccess.FetchAsync(new DataRequest(Name));
            var places = data.GetContent<IEnumerable<Place>>();
            return places;
        }
    }
}
