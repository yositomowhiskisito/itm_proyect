using System;
using System.Runtime.CompilerServices;

namespace LIBUtilities.Core
{
    public class LogHelper
    {
        public static void Log(Exception exception, bool subError = false, [CallerMemberName] string caller = "", [CallerFilePath] string file = "")
        {
            try
            {
                if (exception == null)
                    return;

            }
            catch (Exception ex)
            {
                if (!subError)
                    Log(ex, true);
            }
        }
    }
}