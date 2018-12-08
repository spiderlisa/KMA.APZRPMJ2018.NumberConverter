using System;
using System.IO;

namespace KMA.APZRPMJ2018.NumberConverter.Tools
{
    public static class FileFolderHelper
    {
        private static readonly string AppDataPath =
            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);

        internal static readonly string ClientFolderPath =
            Path.Combine(AppDataPath, "NumberConverter");

        internal static readonly string LogFolderPath =
            Path.Combine(ClientFolderPath, "Log");

        internal static readonly string LogFilepath = Path.Combine(LogFolderPath,
            "App_" + DateTime.Today.ToString("yyyy_MM_dd") + ".txt");

        public static readonly string StorageFilePath =
            Path.Combine(ClientFolderPath, "Storage.numcon");

        public static readonly string LastUserFilePath =
            Path.Combine(ClientFolderPath, "LastUser.numcon");

        public static void CheckAndCreateFile(string filePath)
        {
            try
            {
                FileInfo file = new FileInfo(filePath);
                if (!file.Directory.Exists)
                {
                    file.Directory.Create();
                }
                if (!file.Exists)
                {
                    file.Create().Close();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}