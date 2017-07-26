using System;

namespace Eaton.Homework.Business
{
    /// <summary>Defines methods which are used to log issue/erros</summary>
    public interface ILogManager
    {
        void Log(string message);

        void Log(string message, LogLevel severity);

        void Log(string message, Exception ex, LogLevel severity);
    }
}
