using System;

namespace LIBUtilities.Core
{
    public class FileHelper
    {
        public static IFiles IFiles;

        public static byte[] Load()
        {
            if (IFiles == null)
                return null;

            return IFiles.Load();
        }
    }

    public interface IFiles
    {
        byte[] Load();
    }
}