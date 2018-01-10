using System;
using System.Collections.Generic;
using System.Text;
using AutoFixture;
using AutoFixture.AutoMoq;
using AutoFixture.Xunit2;

namespace Enbiso.Common.UnitTest.AutoMoq
{
    public class AutoMoqDataAttribute : AutoDataAttribute
    {
        public AutoMoqDataAttribute() : base(() => new Fixture().Customize(new AutoMoqCustomization()))
        {
        }
    }
}
