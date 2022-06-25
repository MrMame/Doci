using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarkusMeinhard.Doci.CrossCutting.Logger
{
    public interface ILogger
    {
        LogLevels PrintingLogLevel{get;set;}
        void LogText(LogLevels MessageType, string Text);
    }
}
