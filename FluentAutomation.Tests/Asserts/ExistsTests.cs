namespace FluentAutomation.Tests.Asserts
{
    using Xunit;

    public class ExistsTests : AssertBaseTest
    {
        public ExistsTests()
        {
            InputsPage.Go();
        }

        [Fact]
        public void ElementExists()
        {
            I.Assert
             .Exists("div")
             .Not.Exists("crazyElementThatDoesntExist")
             .Exists(I.Find("div"))
             .Not.Exists(I.Find("crazyElementThatDoesntExist"));

            I.Expect
             .Exists("div")
             .Not.Exists("crazyElementThatDoesntExist")
             .Exists(I.Find("div"))
             .Not.Exists(I.Find("crazyElementThatDoesntExist"));
        }
    }
}