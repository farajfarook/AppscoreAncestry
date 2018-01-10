using System;
using System.IO;
using System.Threading.Tasks;
using AppscoreAncestry.Domain.Exceptions;
using AppscoreAncestry.Infrastructure.Exceptions;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace AppscoreAncestry.Infrastructure.DataAccess
{
    public class FileDataAccess : IDataAccess
    {
        private JObject _jObject;
        private readonly ILogger<FileDataAccess> _logger;
        private readonly IDataDetail _detail;

        public FileDataAccess(ILogger<FileDataAccess> logger, IDataDetail detail)
        {
            _logger = logger;
            _detail = detail;
        }

        public Task LoadAsync()
        {
            if (!(_detail is FileDataDetail)) throw new DataAccessException("Invalid data access details");
            try
            {
                var fileDetails = (FileDataDetail)_detail;
                var content = File.ReadAllText(fileDetails.FileName);
                var fullRes = new DataResult(content);
                _jObject = fullRes.GetContent<JObject>();
                return Task.CompletedTask;
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Failed to load data");
                throw new DataAccessException("Failed to load data", e);
            }
        }

        public async Task<DataResult> FetchAsync(DataRequest request)
        {
            if (_jObject == null) await LoadAsync();
            try
            {
                var dataResult = _jObject[request?.DataSetName];
                return new DataResult(dataResult.ToString());
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Failed to fetch {request?.DataSetName}");
                throw new DataAccessException($"Failed to fetch {request?.DataSetName}", e);
            }
        }
    }
}