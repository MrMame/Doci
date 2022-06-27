using Mame.Doci.CrossCutting.Logging.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mame.Doci.CrossCutting.Logging.Loggers
{
    public interface ILogger
    {
        LogLevels PrintingLogLevel{get;set;}
        void LogText(LogLevels MessageType, string Text);
    }
}
