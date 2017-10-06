namespace FluentAutomation.Tests.Pages
{
    public class SwitchPage : PageObject<SwitchPage>
    {
        public string IFrameSelector = "iframe";

        public string NewWindowSelector = "#new-window";

        public SwitchPage(FluentTest test)
            : base(test)
        {
            Url = "/Switch";
            At = () => I.Assert.Text("Switch Testbed").In("h2");
        }
    }
}