using System;
using log4net;
using Service.Interfaces;

namespace Service.Services
{
    public class LoggerService : ILoggerService
    {
        private static ILog _logger;

        public LoggerService()
        {
            _logger = LogManager.GetLogger("log");
        }
        public void LogError(string message, Exception exception)
        {
            _logger.Error(message, exception);
        }

        public void LogInfo(string message, Exception exception)
        {
            _logger.Info(message, exception);
        }

        public void LogInfo(string message)
        {
            _logger.Info(message);
        }
    }
}
