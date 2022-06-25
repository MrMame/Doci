using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarkusMeinhard.Doci.CrossCutting.Logger
{
    public interface ILogger
    {
        // Bis zu welchem Level sollen die Lognachrichten angezeigt werden
        LogLevels PrintingLogLevel{get;set;}
        void LogText(LogLevels MessageType, string Text);   // Logg die NAchricht im Logger
    }
}
