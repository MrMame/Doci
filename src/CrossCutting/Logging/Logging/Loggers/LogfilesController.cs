using MarkusMeinhard.Doci.CrossCutting.Logging.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarkusMeinhard.Doci.CrossCutting.Logging.Loggers
{
    
    

    class MultiLogger:ILogger{

        List<ILogger> _Loggers = new List<ILogger>();
        LogLevels _PrintingLogLevel = LogLevels.All;


        public LogLevels PrintingLogLevel {
            get {return _PrintingLogLevel;}
            set {_PrintingLogLevel = value;}
        }


        #region "PUBLICS"

        public void LogText(LogLevels MessageType, string Text) {
            foreach (ILogger logger in _Loggers) {
                logger.LogText(MessageType, Text);
            }
        }

        public void AddLogger(ILogger Logger) {
            _Loggers.Add(Logger);
        }

        #endregion "PUBLICS"
        
    }
}
