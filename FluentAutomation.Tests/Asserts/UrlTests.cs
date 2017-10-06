namespace FluentAutomation.Tests.Asserts
{
    using Exceptions;

    using Xunit;

    public class UrlTests : AssertBaseTest
    {
        public UrlTests()
        {
            I.Open(SiteUrl);
        }

        [Fact]
        public void TestUrl()
        {
            I.Assert
             .Url(SiteUrl)
             .Url(x => x.Scheme == "http")
             .Not.Url("http://google.com")
             .Not.Url(x => x.Scheme == "https");

            I.Expect
             .Url(SiteUrl)
             .Url(x => x.Scheme == "http")
             .Not.Url("http://google.com")
             .Not.Url(x => x.Scheme == "https");

            Assert.Throws<FluentException>(() => I.Assert.Not.Url(SiteUrl));
            Assert.Throws<FluentException>(() => I.Assert.Not.Url(x => x.Scheme == "http"));

            Assert.Throws<FluentExpectFailedException>(() => I.Expect.Not.Url(SiteUrl));
            Assert.Throws<FluentExpectFailedException>(() => I.Expect.Not.Url(x => x.Scheme == "http"));
        }
    }
}