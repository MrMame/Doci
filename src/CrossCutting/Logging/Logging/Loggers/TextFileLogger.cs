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

        // string _Filename = "Logfile.log";
        FileInfo _fi;
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
     
        public TextFileLogger(FileInfo TargetLogFile)
        {
            _fi = TargetLogFile;
            _isTextfileAccessible = IsTextfileAccessible(TargetLogFile);
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
        public TextFileLogger (FileInfo TargetLogFile, bool BackupOversizedLogfiles) {
            _fi = TargetLogFile;
            _backupOversizedLogs = BackupOversizedLogfiles;
            _isTextfileAccessible = IsTextfileAccessible(TargetLogFile);
        }
   
        #endregion "CONSTRUCTOR"





        #region "PUBLICS"

        public void LogText(LogLevels MessageType, string Text)
        {
            if (_isTextfileAccessible) {
                string message = DateTime.Now.ToString(_DateTimeFormat) + _seperator + MessageType.ToString("G") + _seperator + Text + "\r\n";
                WriteToTextfile(_fi,message);
                RenameOrDeleteMaxSizedFile(_backupOversizedLogs);
            }
        }

        #endregion "PUBLICS"




        #region "PRIVATES"

        private void RenameOrDeleteMaxSizedFile(bool BackupOversizedLogfiles) {
            if (!_isTextfileAccessible) return;
            
            if (_fi.Length > _maxFileSize) {
                if (BackupOversizedLogfiles) {
                    _fi.MoveTo(_fi.DirectoryName + "\\" +
                                _fi.Name.Replace(_fi.Extension, "") + 
                                "_" + DateTime.Now.ToString("yyyyMMdd_hhmmss") 
                                + _fi.Extension.ToString());
                } else {
                    _fi.Delete();
                }
            }
        }

        private void WriteToTextfile(FileInfo TargetLogfile,string Message)
        {
            if (!_isTextfileAccessible) return;

            using (FileStream fs = TargetLogfile.OpenRead())
            {
                Byte[] info =
                    new UTF8Encoding(true).GetBytes(Message);
                fs.Position = fs.Length;        // Set file inputmarker to the files end
                fs.Write(info, 0, info.Length);
            }

        }


        private bool IsTextfileAccessible(FileInfo TargetLogfile) {

                   
            if (!TargetLogfile.Exists) {
                try {
                    TargetLogfile.CreateText().Close();
                } catch (Exception) {
                    return false;
                }
            } else {
                try {
                    FileStream s = TargetLogfile.OpenWrite();
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
