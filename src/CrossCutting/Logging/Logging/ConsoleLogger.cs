using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarkusMeinhard.Doci.CrossCutting.Logger {
    class ConsoleLogger :ILogger{

        string _seperator = " - ";
        string _DateTimeFormat = "yyyyMMdd_HH:mm:ss";
        LogLevels _PrintingLogLevel = LogLevels.All;

        public LogLevels PrintingLogLevel {
            get { return _PrintingLogLevel;}
            set { _PrintingLogLevel = value; }
        }

        public void LogText(LogLevels MessageType, string Text) {
            Console.WriteLine(DateTime.Now.ToString(_DateTimeFormat) + _seperator + MessageType.ToString("G") + _seperator + Text);
        }


    }
}
