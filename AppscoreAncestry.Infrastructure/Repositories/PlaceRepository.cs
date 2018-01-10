using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppscoreAncestry.Domain.Models.PlaceAggregate;
using AppscoreAncestry.Infrastructure.DataAccess;
using Microsoft.Extensions.Logging;

namespace AppscoreAncestry.Infrastructure.Repositories
{
    public class PlaceRepository: IPlaceRepository
    {
        private const string Name = "places";

        private readonly IDataAccess _dataAccess;
        private readonly ILogger<PlaceRepository> _logger;

        public PlaceRepository(IDataAccess dataAccess, ILogger<PlaceRepository> logger)
        {
            _dataAccess = dataAccess;
            _logger = logger;
        }

        public async Task<Place> GetByIdAsync(int id)
        {
            var places = await ListAsync();
            return places?.SingleOrDefault(p => p.Id == id);
        }

        public async Task<IEnumerable<Place>> ListAsync()
        {
            var data = await _dataAccess.FetchAsync(new DataRequest(Name));
            try
            {
                return data.GetContent<IEnumerable<Place>>();
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Invalid Data");
                return new List<Place>();   
            }
        }
    }
}
