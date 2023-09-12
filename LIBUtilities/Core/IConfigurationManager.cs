namespace LIBUtilities.Utilities
{
    public interface IConfigurationManager
    {
        string AppSetting(string key, string type = "");
    }
}