namespace FluentAutomation.Tests.Pages
{
    public class ScrollingPage : PageObject<ScrollingPage>
    {
        public string BodySelector = "#big";

        public string BottomLeftSelector = "#bottomleft > button";

        public string BottomRightSelector = "#bottomright > button";

        public string FocusColor = "rgb(0, 0, 255)";

        public string HoverColor = "rgb(255, 0, 0)";

        public string TextSelector = "#bigtext";

        public string TopLeftSelector = "#topleft > button";

        public string TopRightSelector = "#topright > button";

        public ScrollingPage(FluentTest test)
            : base(test)
        {
            Url = "/Scrolling";
        }
    }
}