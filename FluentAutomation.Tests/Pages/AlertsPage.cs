namespace FluentAutomation.Tests.Pages
{
    public class AlertsPage : PageObject<AlertsPage>
    {
        public string ResultSelector = "#result";

        public string TriggerAlertSelector = "#trigger-alert";

        public string TriggerConfirmSelector = "#trigger-confirm";

        public string TriggerPromptSelector = "#trigger-prompt";

        public AlertsPage(FluentTest test)
            : base(test)
        {
            Url = "/Alerts";
        }
    }
}