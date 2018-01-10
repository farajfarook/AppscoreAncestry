using System;
using System.Collections.Generic;
using System.Text;
using AppscoreAncestry.Common.UnitTest;
using AppscoreAncestry.Domain.Models.PlaceAggregate;
using AppscoreAncestry.Infrastructure.DataAccess;
using AppscoreAncestry.Infrastructure.Exceptions;
using Xunit;

namespace AppscoreAncestry.Infrastructure.Tests.DataAccess
{
    public class DataResultTests
    {
        [Theory]
        [AutoMoqData]
        public void GetContent_JunkText_Fail(string text)
        {
            var res = new DataResult(text);
            Assert.Equal(text, res.Content);            
            Assert.Throws<DataAccessException>(() => res.GetContent<Place>());
        }
    }
}
