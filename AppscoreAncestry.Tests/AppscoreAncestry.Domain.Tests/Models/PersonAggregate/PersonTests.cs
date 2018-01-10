using System;
using System.Collections.Generic;
using System.Text;
using AppscoreAncestry.Common.UnitTest;
using AppscoreAncestry.Domain.Models.PersonAggregate;
using Moq;
using Xunit;

namespace AppscoreAncestry.Domain.Tests.Models.PersonAggregate
{
    public class PersonTests
    {
        [Theory]
        [InlineAutoMoqData(null)]
        [InlineAutoMoqData("m")]
        [InlineAutoMoqData("f")]
        [InlineAutoMoqData("X")]
        public void Person_SetInvalidGender_ThrowsInvalidOperationException(string gender, Mock<Person> personMock)
        {
            var person = personMock.Object;
            Assert.Throws<InvalidOperationException>(() => person.Gender = gender);
        }

        [Theory]
        [InlineAutoMoqData("M")]
        [InlineAutoMoqData("F")]
        [InlineAutoMoqData("O")]
        public void Person_SetGender_Success(string gender, Mock<Person> personMock)
        {
            var person = personMock.Object;
            person.Gender = gender;
            Assert.True(person.PersonGender != null);
        }
    }
}
