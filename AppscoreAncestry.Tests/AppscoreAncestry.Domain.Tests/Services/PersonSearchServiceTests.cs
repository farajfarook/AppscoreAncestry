using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppscoreAncestry.Common.UnitTest;
using AppscoreAncestry.Domain.Models.PersonAggregate;
using AppscoreAncestry.Domain.Models.PlaceAggregate;
using AppscoreAncestry.Domain.Services;
using AppscoreAncestry.Domain.Tests.Mocks;
using Moq;
using Xunit;
using Xunit.Sdk;

namespace AppscoreAncestry.Domain.Tests.Services
{
    public class PersonSearchServiceTests
    {
        [Theory]
        [AutoMoqData]
        public async void SearchAsync_JustName_Response(Mock<IPersonRepository> personRepoMock,
            IPlaceRepository placeRepository)
        {
            personRepoMock.Setup(m => m.ListAsync()).Returns(Task.FromResult(MockData.GetContent<Person>("people")));
            var service = new PersonSearchService(personRepoMock.Object, placeRepository);
            var data = await service.SearchAsync(new PersonSearch()
            {
                Name = "Millisent"
            });
            Assert.Equal(15, data.People.Count());
        }
        
        [Theory]
        [InlineAutoMoqData(2, 10, 10)]
        [InlineAutoMoqData(10, 10, 5)]
        [InlineAutoMoqData(0, 10, 10)]
        [InlineAutoMoqData(-3, 10, 10)]
        [InlineAutoMoqData(-3, -1, 15)]
        public async void SearchAsync_SkipTake_Response(int skip, int take, int count,
            Mock<IPersonRepository> personRepoMock, IPlaceRepository placeRepository)
        {
            personRepoMock.Setup(m => m.ListAsync()).Returns(Task.FromResult(MockData.GetContent<Person>("people")));
            var service = new PersonSearchService(personRepoMock.Object, placeRepository);
            var data = await service.SearchAsync(new PersonSearch()
            {
                Name = "Millisent",
                Skip = skip,
                Take = take
            });
            Assert.Equal(count, data.People.Count());
        }
        
        [Theory]
        [AutoMoqData]
        public async void SearchAsync_NameAndGender_Response(Mock<IPersonRepository> personRepoMock,
            IPlaceRepository placeRepository)
        {
            personRepoMock.Setup(m => m.ListAsync()).Returns(Task.FromResult(MockData.GetContent<Person>("people")));
            var service = new PersonSearchService(personRepoMock.Object, placeRepository);
            var data = await service.SearchAsync(new PersonSearch()
            {
                Name = "Millisent",
                Genders = new List<PersonGender>()
                {
                    PersonGender.Female
                }
            });
            Assert.Equal(6, data.People.Count());
        }
        
        [Theory]
        [AutoMoqData]
        public async void SearchAsync_NameAndAllGender_Response(Mock<IPersonRepository> personRepoMock, IPlaceRepository placeRepository)
        {
            personRepoMock.Setup(m => m.ListAsync()).Returns(Task.FromResult(MockData.GetContent<Person>("people")));
            var service = new PersonSearchService(personRepoMock.Object, placeRepository);
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
            Assert.Equal(15, data.People.Count());
            Assert.Equal(15, data.Total);
            Assert.Equal(15, data.Take);
            Assert.Equal(0, data.Skip);
        }

        [Theory]
        [AutoMoqData]
        public async void SearchAsync_NameAndAllGenderWithTakeSkip_Response(Mock<IPersonRepository> personRepoMock, IPlaceRepository placeRepository)
        {
            personRepoMock.Setup(m => m.ListAsync()).Returns(Task.FromResult(MockData.GetContent<Person>("people")));
            var service = new PersonSearchService(personRepoMock.Object, placeRepository);
            var data = await service.SearchAsync(new PersonSearch()
            {
                Name = "Millisent",
                Genders = new List<PersonGender>()
                {
                    PersonGender.Female,
                    PersonGender.Male,
                    PersonGender.Other
                },
                Take = 10,
                Skip = 10
            });
            Assert.Equal(5, data.People.Count());
            Assert.Equal(15, data.Total);
            Assert.Equal(10, data.Take);
            Assert.Equal(10, data.Skip);
        }

        [Theory]
        [AutoMoqData]
        public void ListAncenstors_UserIdData_Reponse(Mock<IPersonRepository> personRepoMock, IPlaceRepository placeRepository)
        {
            personRepoMock.Setup(m => m.ListAsync()).Returns(Task.FromResult(MockData.GetContent<Person>("people")));
            personRepoMock.Setup(m => m.GetByIdAsync(It.Is<int>(n => n == 1 || n == 2))).Returns(Task.FromResult(new Person()
            {
                Gender = PersonGender.Male.Id
            }));
            var service = new PersonSearchService(personRepoMock.Object, placeRepository);
            var person = new Person {MotherId = 1, FatherId = 2};
            var data = service.ListAncestors(person);
            Assert.Equal(2, data?.Count());
        }

        [Theory]
        [AutoMoqData]
        public void ListDecendants_ValiData_Success(Mock<IPersonRepository> personRepoMock,
            IPlaceRepository placeRepository)
        {
            IEnumerable<Person> people1 = new List<Person>{(new Person() { Id = 10 })};
            IEnumerable<Person> people2 = new List<Person>{(new Person())};            
            personRepoMock.Setup(m => m.ListChildrenAsync(It.Is<int>(n => n == 15))).Returns(Task.FromResult(people1));
            personRepoMock.Setup(m => m.ListChildrenAsync(It.Is<int>(n => n == 10))).Returns(Task.FromResult(people2));
            var service = new PersonSearchService(personRepoMock.Object, placeRepository);
            var person = new Person { Id = 15 };
            var data = service.ListDescendants(person);
            Assert.Equal(2, data?.Count());
        }

        [Theory]
        [AutoMoqData]
        public void ListDecendants_InvalidData_Empty(Mock<IPersonRepository> personRepoMock,
            IPlaceRepository placeRepository)
        {
            IEnumerable<Person> people1 = new List<Person> { (new Person() { Id = -1 }) };
            IEnumerable<Person> people2 = new List<Person> { (new Person()) };
            personRepoMock.Setup(m => m.ListChildrenAsync(It.Is<int>(n => n == 1))).Returns(Task.FromResult(people1));
            personRepoMock.Setup(m => m.ListChildrenAsync(It.Is<int>(n => n == 10))).Returns(Task.FromResult(people2));
            var service = new PersonSearchService(personRepoMock.Object, placeRepository);
            var person = new Person { Id = 15 };
            var data = service.ListDescendants(person);
            Assert.Equal(0, data?.Count());
        }


        [Theory]
        [AutoMoqData]
        public async void SearchAsync_DecendantsData_Success(Mock<IPersonRepository> personRepoMock,
            IPlaceRepository placeRepository)
        {
            var people = MockData.GetContent<Person>("people");
            var children = people.Where(p => p.Id <= 10);
            personRepoMock.Setup(m => m.ListAsync()).Returns(Task.FromResult(people));
            personRepoMock.Setup(m => m.ListChildrenAsync(It.Is<int>(n => n == 49))).Returns(Task.FromResult(children));
            
            var service = new PersonSearchService(personRepoMock.Object, placeRepository);
            var search = new PersonSearch
            {
                Mode = PersonSearch.SearchMode.Descendants,
                Name = "Audrey Millisent",
                Genders = new List<PersonGender>()
                {
                    PersonGender.Female,
                    PersonGender.Male,
                    PersonGender.Other
                }
            };
            var data = await service.SearchAsync(search);
            Assert.Equal(10, data?.People.Count());
            Assert.Equal(10, data?.Total);
        }
        
        [Theory]
        [AutoMoqData]
        public async void SearchAsync_AncestorsData_Success(string name, int count,
            Mock<IPersonRepository> personRepoMock, IPlaceRepository placeRepository)
        {
            var people = MockData.GetContent<Person>("people");
            personRepoMock.Setup(m => m.ListAsync()).Returns(Task.FromResult(people));
            personRepoMock.Setup(m => m.GetByIdAsync(It.Is<int>(n => n == 42 || n == 41))).Returns(Task.FromResult(new Person()
            {
                Gender = PersonGender.Male.Id
            }));
           
            var service = new PersonSearchService(personRepoMock.Object, placeRepository);
            var search = new PersonSearch
            {
                Mode = PersonSearch.SearchMode.Ancestors,
                Name = "Audrey Millisent",
                Genders = new List<PersonGender>()
                {
                    PersonGender.Female,
                    PersonGender.Male,
                    PersonGender.Other
                }
            };
            var data = await service.SearchAsync(search);
            Assert.Equal(2, data?.People.Count());            
        }
    }
}
