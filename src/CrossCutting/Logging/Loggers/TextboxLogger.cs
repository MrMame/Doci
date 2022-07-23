using Mame.Doci.CrossCutting.Logging.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows.Forms;

namespace Mame.Doci.CrossCutting.Logging.Loggers
{
    public class TextboxLogger:ILogger
    {

        TextBox _outputTextbox;
        char _seperator = ';';
        string _DateTimeFormat = "yyyyMMdd_HH:mm:ss";
        LogLevels _PrintingLogLevel = LogLevels.All;

        delegate void LogTextCallback(LogLevels MessageType, string Text);


        public LogLevels PrintingLogLevel {
            get { return _PrintingLogLevel; }
            set { _PrintingLogLevel = value; }
        }

        public void LogText(LogLevels MessageType, string Text)
        {
            if (_outputTextbox.InvokeRequired) {
                LogTextCallback d = new LogTextCallback(this.LogText);
                _outputTextbox.Invoke(d, new object[] { MessageType,Text});
            } else {
                _outputTextbox.AppendText(DateTime.Now.ToString(_DateTimeFormat) + _seperator + MessageType.ToString("G") + _seperator + Text + "\r\n");
            }

        }


        public TextboxLogger(TextBox outputTextbox)
        {
            _outputTextbox = outputTextbox;
        }
    
    
    }
}
