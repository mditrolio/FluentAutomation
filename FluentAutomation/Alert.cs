namespace FluentAutomation
{
    public class Alert
    {
        public static Alert Cancel = new Alert(AlertField.CancelButton);
        public static Alert Input = new Alert(AlertField.Input);
        public static Alert Message = new Alert(AlertField.Message);
        public static Alert OK = new Alert(AlertField.OKButton);

        public readonly AlertField Field;

        public Alert(AlertField field)
        {
            Field = field;
        }
    }

    public enum AlertField
    {
        OKButton,
        CancelButton,
        Message,
        Input
    }
}