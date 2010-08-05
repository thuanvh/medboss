namespace Nammedia.Medboss.Log
{
    using System;
    using System.Configuration;
    using System.IO;

    internal class LogManager
    {
        public static string LogPathFile;
        public static string SeperatedLine = "****************************";

        public static void InitialLogManager()
        {
            try
            {
                string path = ConfigurationManager.AppSettings["LogPath"];
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                if (Directory.Exists(path))
                {
                    string file = DateTime.Today.ToString("yyMMdd_hhmmss") + DateTime.Today.ToBinary().ToString() + ".log";
                    LogPathFile = path + "/" + file;
                    File.Create(LogPathFile);
                }
            }
            catch (Exception exc)
            {
                LogException(exc);
            }
        }

        public static void LogException(Exception exc)
        {
            try
            {
                File.AppendAllText(LogPathFile, SeperatedLine + "\n" + exc.ToString());
            }
            catch (Exception e)
            {
                LogException(e);
            }
        }
    }
}
