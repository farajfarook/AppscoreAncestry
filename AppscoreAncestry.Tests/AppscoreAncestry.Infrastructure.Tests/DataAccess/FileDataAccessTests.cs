using System;
using System.Collections.Generic;
using System.Text;
using AppscoreAncestry.Common.UnitTest;
using AppscoreAncestry.Infrastructure.DataAccess;
using AppscoreAncestry.Infrastructure.Exceptions;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace AppscoreAncestry.Infrastructure.Tests.DataAccess
{
    public class FileDataAccessTests
    {
        [Theory]
        [InlineAutoMoqData("./Mocks/data_small.json")]
        public async void LoadhAsync_ValidFile_Success(string filename, FileDataAccess dataAccess)
        {
            await dataAccess.LoadAsync(new FileDataDetail(filename));            
        }

        [Theory]
        [InlineAutoMoqData(null)]
        [InlineAutoMoqData("./Mocks/nofile.json")]
        [InlineAutoMoqData("./Mocks/invalid_data.json")]
        public async void LoadhAsync_InValidFile_ThrowsDataAccessException(string filename, FileDataAccess dataAccess)
        {
            await Assert.ThrowsAsync<DataAccessException>(() => dataAccess.LoadAsync(new FileDataDetail(filename)));
        }

        [Theory]
        [InlineAutoMoqData("./Mocks/data_small.json", "people", 52)]
        [InlineAutoMoqData("./Mocks/data_small.json", "places", 825)]
        public async void FetchAsync_ValidType_Success(string filename, string type, int size, FileDataAccess dataAccess)
        {
            await dataAccess.LoadAsync(new FileDataDetail(filename));
            var data = await dataAccess.FetchAsync(new DataRequest(type));
            var list = data.GetContent<List<object>>();
            Assert.True(list.Count == size);
        }
    }
}
