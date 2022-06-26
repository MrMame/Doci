using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MarkusMeinhard.Doci.CrossCutting.Logging;
using System.IO;
using MarkusMeinhard.Doci.CrossCutting.Logging.Data;

namespace MarkusMeinhard.Doci.CrossCutting.Logging.Loggers
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

        #region " CONSTRUCTOR"

        public TextFileLogger() {
            _isTextfileAccessible = false;
        }
     
        public TextFileLogger(string LogFileName)
        {
            _Filename = LogFileName;
            _isTextfileAccessible = IsTextfileAccessible(_Filename);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="LogFileName">Filename of the target Logfile</param>
        /// <param name="BackupOversizedLogfiles"> If true, a new logfile will be created if the size 
        /// of the target logfile exceeds the maxFileSize threshold.actual datei will be append 
        /// to the backup logfiles filename.
        /// If false, the target logfile will be deleted as soon as the maxFileSize threshhold is exceeded.
        /// All old data will be lost.</param>
        public TextFileLogger (string LogFileName, bool BackupOversizedLogfiles) {
            _Filename = LogFileName;
            _backupOversizedLogs = BackupOversizedLogfiles;
            _isTextfileAccessible = IsTextfileAccessible(_Filename);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="BackupOversizedLogfiles"> If true, a new logfile will be created if the size 
        /// of the target logfile exceeds the maxFileSize threshold.actual datei will be append 
        /// to the backup logfiles filename.
        /// If false, the target logfile will be deleted as soon as the maxFileSize threshhold is exceeded.
        /// All old data will be lost.</param>
        public TextFileLogger (bool BackupOversizedLogfiles) {
            _backupOversizedLogs = BackupOversizedLogfiles;
            _isTextfileAccessible = IsTextfileAccessible(_Filename);
        }

        #endregion "CONSTRUCTOR"


        #region "PUBLICS"

        public void LogText(LogLevels MessageType, string Text)
        {
            if (_isTextfileAccessible) {
                string message = DateTime.Now.ToString(_DateTimeFormat) + _seperator + MessageType.ToString("G") + _seperator + Text + "\r\n";
                WriteToTextfile(_Filename,message);
                RenameOrDeleteMaxSizedFile(_backupOversizedLogs);
            }
        }

        #endregion "PUBLICS"


        #region "PRIVATES"

        private void RenameOrDeleteMaxSizedFile(bool BackupOversizedLogfiles) {
            if (!_isTextfileAccessible) return;
            
            var fi = new FileInfo(_Filename);
            if (fi.Length > _maxFileSize) {
                if (BackupOversizedLogfiles) {
                    fi.MoveTo(fi.DirectoryName + "\\" + 
                                fi.Name.Replace(fi.Extension, "") + 
                                "_" + DateTime.Now.ToString("yyyyMMdd_hhmmss") 
                                + fi.Extension.ToString());
                } else {
                    fi.Delete();
                }
            }
        }

        private void WriteToTextfile(string Filename,string Message)
        {
            if (!_isTextfileAccessible) return;

            using (FileStream fs = File.OpenWrite(Filename))
            {
                Byte[] info =
                    new UTF8Encoding(true).GetBytes(Message);
                fs.Position = fs.Length;        // Set file inputmarker to the files end
                fs.Write(info, 0, info.Length);
            }

        }


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
            return true;
        }

        #endregion
    }
}
