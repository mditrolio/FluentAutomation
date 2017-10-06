namespace FluentAutomation.Tests.Pages
{
    public class InputsPage : PageObject<InputsPage>
    {
        public string ButtonClickedTextSelector = "#button-clicked-text";

        public string ButtonControlSelector = "#button-control";

        public string FocusColor = "rgb(0, 0, 255)";

        public string FormGroupDivSelector = "div[class='form-group other-class']";

        public string HiddenDivSelector = "#hidden-div";

        public string HoverColor = "rgb(255, 0, 0)";

        public string InputButtonControlSelector = "#input-button-control";

        public string MultiSelectControlSelector = "#multi-select-control";

        public string SelectControlSelector = "#select-control";

        public string TextareaControlSelector = "#textarea-control";

        public string TextChangedTextSelector = "#text-control-changed";

        public string TextControlSelector = "#text-control";

        public string TextEmailControlSelector = "#text-email-control";
        public string TextNumberControlSelector = "#text-number-control";
        public string TextPasswordControlSelector = "#text-password-control";
        public string TextSearchControlSelector = "#text-search-control";
        public string TextTelControlSelector = "#text-tel-control";
        public string TextUrlControlSelector = "#text-url-control";

        public InputsPage(FluentTest test)
            : base(test)
        {
            Url = "/Inputs";
        }
    }
}