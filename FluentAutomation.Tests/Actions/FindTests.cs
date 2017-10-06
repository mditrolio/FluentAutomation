namespace FluentAutomation.Tests.Actions
{
    using Exceptions;

    using Xunit;

    public class FindTests : BaseTest
    {
        public FindTests()
        {
            InputsPage.Go();
        }

        [Fact]
        public void AttemptToFindFakeElement()
        {
            var exception = Assert.Throws<FluentElementNotFoundException>(() => I.Find("#fake-control").Element.ToString()); // accessing Element executes the Find
            Assert.True(exception.Message.Contains("Unable to find"));
            Assert.Throws<FluentElementNotFoundException>(() => I.FindMultiple("doesntexist").Element);
        }

        [Fact]
        public void FindElement()
        {
            var element = I.Find(InputsPage.TextControlSelector).Element;

            // simple assert on element to ensure it was properly loaded
            Assert.True(element.IsText);
            Assert.Throws<FluentElementNotFoundException>(() => I.Find("doesntexist").Element);
        }

        [Fact]
        public void FindMultipleElements()
        {
            var proxy = I.FindMultiple("div");

            Assert.True(proxy.Elements.Count > 1);
            Assert.False(proxy.Element.IsText);
            Assert.True(proxy.Element.Text == proxy.Element.Value);
        }
    }
}