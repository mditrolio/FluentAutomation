namespace FluentAutomation.Tests.Asserts
{
    using Exceptions;

    using Xunit;

    public class BooleanTests : AssertBaseTest
    {
        [Fact]
        public void False()
        {
            I.Assert.False(() => false);
            Assert.Throws<FluentException>(() => I.Assert.False(() => true));
        }

        [Fact]
        public void True()
        {
            I.Assert.True(() => true);
            Assert.Throws<FluentException>(() => I.Assert.True(() => false));
        }
    }
}