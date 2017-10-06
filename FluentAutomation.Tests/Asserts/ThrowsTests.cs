namespace FluentAutomation.Tests.Asserts
{
    using Exceptions;

    using Xunit;

    public class ThrowsTests : AssertBaseTest
    {
        [Fact]
        public void TestThrow()
        {
            I.Assert.Throws(() => I.Assert.True(() => false));
            I.Assert.Not.Throws(() => I.Assert.True(() => true));

            Assert.Throws<FluentException>(() => I.Assert.Throws(() => I.Assert.True(() => true)));
            Assert.Throws<FluentException>(() => I.Assert.Not.Throws(() => I.Assert.True(() => false)));

            Assert.Throws<FluentAssertFailedException>(() => With.WaitOnAllAsserts(false).Then.Assert.Throws(() => I.Assert.True(() => true)));
        }

        private void ThrowException()
        {
            throw new FluentException("wat");
        }
    }
}