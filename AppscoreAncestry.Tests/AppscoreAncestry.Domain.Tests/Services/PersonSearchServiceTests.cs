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

        [Theory]
        [AutoMoqData]
        public void ListAncenstors_UserIdData_Reponse(Mock<IPersonRepository> personRepoMock)
        {
            personRepoMock.Setup(m => m.ListAsync()).Returns(Task.FromResult(MockData.GetContent<Person>("people")));
            var service = new PersonSearchService(personRepoMock.Object);
            var person = new Person {Name = "Caitlin Millisent"};
            var data = service.ListAncestors(person);
            Assert.Equal(15, data?.Count());
        }

        [Theory]
        [AutoMoqData]
        public void ListDecendants_ValiData_Success(Mock<IPersonRepository> personRepoMock)
        {
            IEnumerable<Person> people1 = new List<Person>{(new Person() { Id = 10 })};
            IEnumerable<Person> people2 = new List<Person>{(new Person())};            
            personRepoMock.Setup(m => m.ListChildrenAsync(It.Is<int>(n => n == 15))).Returns(Task.FromResult(people1));
            personRepoMock.Setup(m => m.ListChildrenAsync(It.Is<int>(n => n == 10))).Returns(Task.FromResult(people2));
            var service = new PersonSearchService(personRepoMock.Object);
            var person = new Person { Id = 15 };
            var data = service.ListDescendants(person);
            Assert.Equal(2, data?.Count());
        }

        [Theory]
        [AutoMoqData]
        public void ListDecendants_InvalidData_Empty(Mock<IPersonRepository> personRepoMock)
        {
            IEnumerable<Person> people1 = new List<Person> { (new Person() { Id = -1 }) };
            IEnumerable<Person> people2 = new List<Person> { (new Person()) };
            personRepoMock.Setup(m => m.ListChildrenAsync(It.Is<int>(n => n == 1))).Returns(Task.FromResult(people1));
            personRepoMock.Setup(m => m.ListChildrenAsync(It.Is<int>(n => n == 10))).Returns(Task.FromResult(people2));
            var service = new PersonSearchService(personRepoMock.Object);
            var person = new Person { Id = 15 };
            var data = service.ListDescendants(person);
            Assert.Equal(0, data?.Count());
        }



        [Theory]
        [AutoMoqData]
        public async void SearchAsync_DecendantsData_Success(Mock<IPersonSearchService> serviceMock)
        {
            serviceMock.Setup(_ => _.ListDescendants(It.IsAny<Person>()))
                .Returns(MockData.GetContent<Person>("people"));
            var service = serviceMock.Object;
            var data = await service.SearchAsync(new PersonSearch()
            {
                Mode = PersonSearch.SearchMode.Descendants,
                Name = "Caitlin Millisent",
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
