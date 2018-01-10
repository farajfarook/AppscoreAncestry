using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppscoreAncestry.Common.UnitTest;
using AppscoreAncestry.Infrastructure.DataAccess;
using AppscoreAncestry.Infrastructure.Repositories;
using AppscoreAncestry.Infrastructure.Tests.Mocks;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace AppscoreAncestry.Infrastructure.Tests.Repositories
{
    public class PersonRepositoryTests
    {
        [Theory]
        [AutoMoqData]
        public async void ListAsync_WithDefault_Success(Mock<IDataAccess> dataAccessMock, ILogger<PersonRepository> logger)
        {
            var dataStr = MockData.GetContent("people");
            dataAccessMock.Setup(_ => _.FetchAsync(It.IsAny<DataRequest>())).Returns(Task.FromResult(new DataResult(dataStr)));
            var repo = new PersonRepository(dataAccessMock.Object, logger);
            var persons = await repo.ListAsync();
            Assert.Equal(52, persons.Count());
        }

        [Theory]
        [AutoMoqData]
        public async void ListAsync_NullResults_Success(Mock<IDataAccess> dataAccessMock, ILogger<PersonRepository> logger)
        {            
            dataAccessMock.Setup(_ => _.FetchAsync(It.IsAny<DataRequest>())).Returns(Task.FromResult(new DataResult(null)));
            var repo = new PersonRepository(dataAccessMock.Object, logger);
            var places = await repo.ListAsync();
            Assert.Empty(places);
        }

        [Theory]
        [InlineAutoMoqData(1, true)]
        [InlineAutoMoqData(-1, false)]
        [InlineAutoMoqData(9999999, false)]
        public async void GetById_WithIds_Tests(int id, bool result, Mock<IDataAccess> dataAccessMock, ILogger<PersonRepository> logger)
        {
            var dataStr = MockData.GetContent("people");
            dataAccessMock.Setup(_ => _.FetchAsync(It.IsAny<DataRequest>())).Returns(Task.FromResult(new DataResult(dataStr)));
            var repo = new PersonRepository(dataAccessMock.Object, logger);
            var place = await repo.GetByIdAsync(id);
            Assert.True(place != null == result);
        }

        [Theory]
        [AutoMoqData]
        public async void GetById_WithId_Success(Mock<IDataAccess> dataAccessMock, ILogger<PersonRepository> logger)
        {
            var dataStr = MockData.GetContent("people");
            dataAccessMock.Setup(_ => _.FetchAsync(It.IsAny<DataRequest>())).Returns(Task.FromResult(new DataResult(dataStr)));
            var repo = new PersonRepository(dataAccessMock.Object, logger);
            var person = await repo.GetByIdAsync(1);
            Assert.Equal("Brande Brittne", person.Name);
        }
    }
}
