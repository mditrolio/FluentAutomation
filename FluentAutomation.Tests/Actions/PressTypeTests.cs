namespace FluentAutomation.Tests.Actions
{
    using Xunit;

    public class PressTypeTests : BaseTest
    {
        public PressTypeTests()
        {
            InputsPage.Go();
        }

        [Fact]
        public void PressType()
        {
            I.Focus(InputsPage.TextControlSelector)
             .Press("{TAB}")
             .Type("wat")
             .Assert.Text("wat").In(InputsPage.TextareaControlSelector);
        }
    }
}