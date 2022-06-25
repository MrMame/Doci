using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarkusMeinhard.Doci.CrossCutting.Logger {
    
    
    /// <summary>
    /// Bietet die möglichkeit, beliebig viele Logger zu registrieren. Wird die Funktion LogText ausgeführt, 
    /// wird für alle in dieser Klassen registrierten Logger geloggt.
    /// </summary>
    class LogfilesController :ILogger{

        List<ILogger> _Loggers = new List<ILogger>();
        LogLevels _PrintingLogLevel = LogLevels.All;


        public LogLevels PrintingLogLevel {
            get {return _PrintingLogLevel;}
            set {_PrintingLogLevel = value;}
        }


        #region "PUBLICS vvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvv"

        /// <summary>
        /// Logt in jedem registrierten Loggingobjekt den Übergebenen Text
        /// </summary>
        /// <param name="MessageType">Typ des Meldungstextes</param>
        /// <param name="Text">Meldungstext, der geloggt werden soll.</param>
        public void LogText(LogLevels MessageType, string Text) {
            foreach (ILogger logger in _Loggers) {
                logger.LogText(MessageType, Text);
            }
        }

        /// <summary>
        /// Fügt der Logger Liste ein neues Logger Objekt hinzu
        /// </summary>
        /// <param name="Logger"></param>
        public void AddLogger(ILogger Logger) {
            _Loggers.Add(Logger);
        }

        #endregion "PUBLICS ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^"
        
    }
}
