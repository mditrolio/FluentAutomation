namespace FluentAutomation.Tests.Pages
{
    public class TextPage : PageObject<TextPage>
    {
        public string FocusColor = "rgb(0, 0, 255)";

        public string HoverColor = "rgb(255, 0, 0)";

        public string Link1Selector = "#link1";

        public string Link2Selector = "#link1";

        public string Paragraph1Selector = "#paragraph1";

        public string Paragraph2Selector = "#paragraph2";

        public string TitleSelector = "#title";

        public TextPage(FluentTest test)
            : base(test)
        {
            Url = "/Text";
        }
    }
}