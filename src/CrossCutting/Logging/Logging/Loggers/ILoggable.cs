using Mame.Doci.CrossCutting.Logging.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mame.Doci.CrossCutting.Logging.Loggers
{
    public interface ILoggable
    {
        ILogger Logger { get;set;}      // Logger that will be used to Log the Message
        void LogMessage (LogLevels LogLevel,string Message);    // Call if you want to log a Message

    }
}
