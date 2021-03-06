﻿namespace FluentAutomation.Tests.Base
{
    using Exceptions;

    using Xunit;

    // Not public because we don't want this test running in the standard suite and we aren't using Traits yet
    // to group them. Maybe later.
    internal class MultiBrowserTests : FluentTest
    {
        private MultiBrowserTests()
        {
            SeleniumWebDriver.Bootstrap(SeleniumWebDriver.Browser.Chrome, SeleniumWebDriver.Browser.Firefox);
        }

        [Fact]
        /// See https://github.com/stirno/FluentAutomation/issues/104
        public void AssertShouldFailTest()
        {
            Assert.Throws<FluentException>(() =>
            {
                I.Open("http://google.com/")
                 .Assert
                 .Class("wowza").On("input[type='text']");
            });
        }
    }
}