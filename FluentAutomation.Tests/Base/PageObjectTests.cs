namespace FluentAutomation.Tests.Base
{
    using Exceptions;

    using Pages;

    using Xunit;

    public class PageObjectTests : BaseTest
    {
        [Fact]
        public void SwitchPageObject()
        {
            SwitchPage.Go();

            var switchPage = InputsPage.Switch<SwitchPage>();
            Assert.True(switchPage.Url.EndsWith("Switch"));

            InputsPage.Go();

            // throw because we aren't on the SwitchPage and nothing is navigating us there
            Assert.Throws<FluentException>(() => InputsPage.Switch<SwitchPage>());
        }
    }
}