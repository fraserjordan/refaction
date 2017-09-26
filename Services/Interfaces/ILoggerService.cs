using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Service.Models;

namespace Service.Interfaces
{
    public interface ILoggerService
    {
        void LogError(string message, Exception exception);
        void LogInfo(string message, Exception excpetion);
        void LogInfo(string message);
    }
}
