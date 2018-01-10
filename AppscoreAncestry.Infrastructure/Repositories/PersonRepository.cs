using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppscoreAncestry.Common.Domain;
using AppscoreAncestry.Domain.Exceptions;
using AppscoreAncestry.Domain.Models.PersonAggregate;
using AppscoreAncestry.Infrastructure.DataAccess;
using AppscoreAncestry.Infrastructure.Models;
using Microsoft.Extensions.Logging;

namespace AppscoreAncestry.Infrastructure.Repositories
{
    public class PersonRepository: IPersonRepository
    {
        private const string Name = "people";
        private readonly IDataAccess _dataAccess;
        private readonly ILogger<PersonRepository> _logger;

        public PersonRepository(IDataAccess dataAccess, ILogger<PersonRepository> logger)
        {
            _dataAccess = dataAccess;
            _logger = logger;
        }

        public async Task<Person> GetByIdAsync(int id)
        {
            var people = await ListAsync();
            return people.SingleOrDefault(p => p.Id == id);
        }

        public async Task<IEnumerable<Person>> ListAsync()
        {
            try
            {
                var data = await _dataAccess.FetchAsync(new DataRequest(Name));
                return data.GetContent<IEnumerable<Person>>();
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Invalid data");
                return new List<Person>();
            }
        }
    }
}
