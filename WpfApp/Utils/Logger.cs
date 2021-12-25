using System;
using System.IO;

namespace WpfApp.Utils
{
    public static class Logger
    {
        public const string LogFile = @"application.log";

        private static readonly object FileLock = new();

        public static void Info(string text)
        {
            Write(text, LogLevel.INFO);
        }

        public static void Warning(string text)
        {
            Write(text, LogLevel.WARNING);
        }

        public static void Error(string text)
        {
            Write(text, LogLevel.ERROR);
        }

        private static void Write(string text, LogLevel logLevel)
        {
            var contents = $"[{DateTime.Now}] [{logLevel}] {text}{Environment.NewLine}";

            lock (FileLock)
                File.AppendAllText(LogFile, contents);
        }
    }

    public enum LogLevel
    {
        INFO,
        WARNING,
        ERROR
    }
}
