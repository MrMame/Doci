using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MarkusMeinhard.Doci.CrossCutting.Logger;
using System.IO;

namespace MarkusMeinhard.Doci.CrossCutting.Logger
{
    class TextFileLogger:ILogger
    {

        string _Filename = "Logfile.log";
        char _seperator = ';';
        string _DateTimeFormat = "yyyyMMdd_HH:mm:ss";
        long _maxFileSize = 150000000;
        bool _backupOversizedLogs = false;
        bool _isTextfileAccessible;



        LogLevels _PrintingLogLevel = LogLevels.All;

        public LogLevels PrintingLogLevel {
            get { return _PrintingLogLevel; }
            set { _PrintingLogLevel = value; }
        }

        #region " CONSTRUCTOR -----------------------------------------------------------------"

        public TextFileLogger() {
            _isTextfileAccessible = false;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Filename">Filename des Logfiles, das erzeugt werden soll</param>
        public TextFileLogger(string Filename)
        {
            _Filename = Filename;
            _isTextfileAccessible = IsTextfileAccessible(_Filename);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Filename">Dateiname des Logfiles, das erzeugt werden soll</param>
        /// <param name="BackupOversizedLogfiles">Gibt an, ob das Logfile mit einem Datum 
        /// versehen werden soll und als Backup gespeichert werden soll, 
        /// sobald das Logfile die maximale Grösse überschritten hat.
        /// Andernfalls wird das Logfile komplett gelöscht und von vorne befüllt.
        /// Achtung: Vorangegangene Meldungen werden dadurch unwiderbringlich gelöscht.</param>
        public TextFileLogger(string Filename,bool BackupOversizedLogfiles) {
            _Filename = Filename;
            _backupOversizedLogs = BackupOversizedLogfiles;
            _isTextfileAccessible = IsTextfileAccessible(_Filename);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="BackupOversizedLogfiles">Gibt an, ob das Logfile mit einem Datum 
        /// versehen werden soll und als Backup gespeichert werden soll, 
        /// sobald das Logfile die maximale Grösse überschritten hat.
        /// Andernfalls wird das Logfile komplett gelöscht und von vorne befüllt.
        /// Achtung: Vorangegangene Meldungen werden dadurch unwiderbringlich gelöscht.</param>
        public TextFileLogger(bool BackupOversizedLogfiles) {
            _backupOversizedLogs = BackupOversizedLogfiles;
            _isTextfileAccessible = IsTextfileAccessible(_Filename);
        }

        #endregion "CONSTRUCTOR"


        #region "PUBLICS --------------------------------------------------------------"

        public void LogText(LogLevels MessageType, string Text)
        {
            if (_isTextfileAccessible) {
                string message = DateTime.Now.ToString(_DateTimeFormat) + _seperator + MessageType.ToString("G") + _seperator + Text + "\r\n";
                WriteToTextfile(_Filename,message);
                RenameOrDeleteMaxSizedFile(_backupOversizedLogs);
            }
        }

        #endregion "PUBLICS"


        #region "PRIVATES -------------------------------------------------------------"
        /// <summary>
        /// Prüft, ob die Dateigrösse des Logfiles überschritten wurde. In diesem Fall 
        /// wird das alte Logfile mit einem Datum versehen und es wird 
        /// ein neues Logfile angelegt.
        /// </summary>
        private void RenameOrDeleteMaxSizedFile(bool BackupOversizedLogfiles) {
            // Wenn Datei nicht verfügbur, schliessen
            if (!_isTextfileAccessible) return;
            
            var fi = new FileInfo(_Filename);
            // Wenn Die Datei über die maximale Grösse hinaus gewachsen ist...
            if (fi.Length > _maxFileSize) {
                // ... und das Logfile gespeichert werden soll
                if (BackupOversizedLogfiles) {
                    fi.MoveTo(fi.DirectoryName + "\\" + 
                                fi.Name.Replace(fi.Extension, "") + 
                                "_" + DateTime.Now.ToString("yyyyMMdd_hhmmss") 
                                + fi.Extension.ToString());

                } else {
                // ... das Logfile NICHT gespeichert werden soll
                    fi.Delete();
                }
            }
        }

        private void WriteToTextfile(string Filename,string Message)
        {
            // Wenn Datei nicht verfügbur, schliessen
            if (!_isTextfileAccessible) return;

            using (FileStream fs = File.OpenWrite(Filename))
            {
                Byte[] info =
                    new UTF8Encoding(true).GetBytes(Message);
                // Add some information to the file.
                fs.Position = fs.Length;
                fs.Write(info, 0, info.Length);
            }

        }

        /// <summary>
        /// Prüft, ob das Textfile für die VErwendung mit dem Logger zur verfügung steht.
        /// </summary>
        /// <param name="Filename">Name der TEtdatei, die geprüft werden soll.</param>
        /// <returns></returns>
        private bool IsTextfileAccessible(string Filename) {

            FileInfo fi;
            try {
                fi = new FileInfo(Filename);
            } catch (Exception) {
                return false;
            }
            
            if (!fi.Exists) {
                try {
                fi.CreateText().Close();
                } catch (Exception) {
                    return false;
                }
            } else {
                try {
                    FileStream s = fi.OpenWrite();
                    s.Close();
                } catch (UnauthorizedAccessException ex) {
                    return false;
                } catch (DirectoryNotFoundException ex) {
                    return false;
                }
            }
            //if (fi.IsReadOnly) {
            //    return false;
            //}
            // Datei öffnen/Schliessen um Zugriff zu testen
            
            
            return true;
        }

        #endregion
    }
}
