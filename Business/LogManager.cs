using System;

namespace Eaton.Homework.Business
{
    public enum LogLevel
    { 
        /// <summary>Only Errors</summary>
        Error = 1,
        /// <summary>Only Warnings and Errors</summary>
        Warning = 2,
        /// <summary>Informations and above</summary>
        Info = 3,
        /// <summary>Verbose mode (all messages)</summary>
        Debug = 4,
        Unknown = 6
    }

    public class LogManager : ILogManager
    {
        public void Log(string message)
        {
            //TODO
        }

        public void Log(string message, LogLevel severity)
        {
            //TODO
        }

        public void Log(string message, Exception ex, LogLevel severity)
        {
            //TODO
        }
    }
}
