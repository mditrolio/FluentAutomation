namespace FluentAutomation.Tests.Asserts
{
    using Xunit;

    public class CountTests : AssertBaseTest
    {
        public CountTests()
        {
            InputsPage.Go();
        }

        [Fact]
        public void CountElements()
        {
            I.Assert
             .Count(0).Not.Of("div")
             .Count(0).Not.Of(I.Find("div"))
             .Count(0).Of("crazyElementThatDoesntExist")
             .Count(0).Of(I.Find("crazyElementThatDoesntExist"));

            I.Expect
             .Count(0).Not.Of("div")
             .Count(0).Not.Of(I.Find("div"))
             .Count(0).Of("crazyElementThatDoesntExist")
             .Count(0).Of(I.Find("crazyElementThatDoesntExist"));
        }

        [Fact]
        public void CountFailure()
        {
            I.Assert
             .Throws(() => I.Assert.Count(0).Of("div"))
             .Throws(() => I.Assert.Count(1).Of("div"))
             .Throws(() => I.Assert.Count(1).Of("crazyElementThatDoesntExist"))
             .Throws(() => I.Assert.Count(0).Not.Of("crazyElementThatDoesntExist"));
        }
    }
}