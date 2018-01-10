using System;
using System.Collections.Generic;
using System.Text;
using AppscoreAncestry.Common.UnitTest;
using AppscoreAncestry.Infrastructure.DataAccess;
using AppscoreAncestry.Infrastructure.Tests.Mocks;
using Xunit;

namespace AppscoreAncestry.Infrastructure.Tests.DataAccess
{
    public class FileDataAccessTests
    {
        [Theory]
        [AutoMoqData]
        public async void FetchAsync_ValidRequest_Success(FileDataAccess dataAccess)
        {
            var request = new FileDataRequest(MockFiles.DataSmallFile);
            var result = await dataAccess.FetchAsync(request);

        }
    }
}
