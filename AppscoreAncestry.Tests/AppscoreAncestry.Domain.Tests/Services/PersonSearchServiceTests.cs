using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppscoreAncestry.Common.UnitTest;
using AppscoreAncestry.Domain.Models.PersonAggregate;
using AppscoreAncestry.Domain.Services;
using AppscoreAncestry.Domain.Tests.Mocks;
using Moq;
using Xunit;

namespace AppscoreAncestry.Domain.Tests.Services
{
    public class PersonSearchServiceTests
    {
        [Theory]
        [AutoMoqData]
        public async void SearchAsync_JustName_Response(Mock<IPersonRepository> personRepoMock)
        {
            personRepoMock.Setup(m => m.ListAsync()).Returns(Task.FromResult(MockData.GetContent<Person>("people")));
            var service = new PersonSearchService(personRepoMock.Object);
            var data = await service.SearchAsync(new PersonSearch()
            {
                Name = "Millisent"
            });
            Assert.Equal(15, data?.Count());
        }
        
        [Theory]
        [InlineAutoMoqData(2, 10, 10)]
        [InlineAutoMoqData(10, 10, 5)]
        [InlineAutoMoqData(0, 10, 10)]
        [InlineAutoMoqData(-3, 10, 10)]
        [InlineAutoMoqData(-3, -1, 15)]
        public async void SearchAsync_SkipTake_Response(int skip, int take, int count, Mock<IPersonRepository> personRepoMock)
        {
            personRepoMock.Setup(m => m.ListAsync()).Returns(Task.FromResult(MockData.GetContent<Person>("people")));
            var service = new PersonSearchService(personRepoMock.Object);
            var data = await service.SearchAsync(new PersonSearch()
            {
                Name = "Millisent",
                Skip = skip,
                Take = take
            });
            Assert.Equal(count, data?.Count());
        }
        
        [Theory]
        [AutoMoqData]
        public async void SearchAsync_NameAndGender_Response(Mock<IPersonRepository> personRepoMock)
        {
            personRepoMock.Setup(m => m.ListAsync()).Returns(Task.FromResult(MockData.GetContent<Person>("people")));
            var service = new PersonSearchService(personRepoMock.Object);
            var data = await service.SearchAsync(new PersonSearch()
            {
                Name = "Millisent",
                Genders = new List<PersonGender>()
                {
                    PersonGender.Female
                }
            });
            Assert.Equal(6, data?.Count());
        }
        
        [Theory]
        [AutoMoqData]
        public async void SearchAsync_NameAndAllGender_Response(Mock<IPersonRepository> personRepoMock)
        {
            personRepoMock.Setup(m => m.ListAsync()).Returns(Task.FromResult(MockData.GetContent<Person>("people")));
            var service = new PersonSearchService(personRepoMock.Object);
            var data = await service.SearchAsync(new PersonSearch()
            {
                Name = "Millisent",
                Genders = new List<PersonGender>()
                {
                    PersonGender.Female,
                    PersonGender.Male,
                    PersonGender.Other
                }
            });
            Assert.Equal(15, data?.Count());
        }
    }
}
