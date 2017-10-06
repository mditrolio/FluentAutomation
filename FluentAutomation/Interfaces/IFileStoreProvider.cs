namespace FluentAutomation
{
    public interface IFileStoreProvider
    {
        bool SaveScreenshot(FluentSettings settings, byte[] contents, string fileName);
    }
}