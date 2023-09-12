using System;

namespace LIBUtilities.Core
{
    public class LogsHelper
    {
        public static ILogs ILogs;

        public static void Logs(Exception ex)
        {
            if (ILogs == null)
                return;

            ILogs.Logs(ex);
        }
    }

    public interface ILogs
    {
        void Logs(Exception ex);
    }

}