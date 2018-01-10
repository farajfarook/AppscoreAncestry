using AutoFixture.Xunit2;
using Xunit;

namespace Enbiso.Common.UnitTest.AutoMoq
{
    public class InlineAutoMoqDataAttribute : CompositeDataAttribute
    {
        public InlineAutoMoqDataAttribute(params object[] values) : base(new InlineDataAttribute(values), new AutoMoqDataAttribute())
        {
        }
    }
}