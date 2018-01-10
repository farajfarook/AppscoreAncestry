using AutoFixture.Xunit2;
using Xunit;

namespace AppscoreAncestry.Common.UnitTest
{
    public class InlineAutoMoqDataAttribute : CompositeDataAttribute
    {
        public InlineAutoMoqDataAttribute(params object[] values) : base(new InlineDataAttribute(values), new AutoMoqDataAttribute())
        {
        }
    }
}