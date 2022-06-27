using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Mame.Doci.CrossCutting.Logging;
using System.IO;
using Mame.Doci.CrossCutting.Logging.Data;

namespace Mame.Doci.CrossCutting.Logging.Loggers
{
    public class TextFileLogger:ILogger
    {

        private const bool DEFAULT_BACKUP_OVERSIZED_FILES = true;
        private const Int32 DEFAULT_BACKUP_MAXSIZE = 150000000;

        FileInfo _fi;
        char _logTextSeperator = ';';
        string _logTextDateTimeFormat = "yyyyMMdd_HH:mm:ss";
        long _maxBackupFileSize = DEFAULT_BACKUP_MAXSIZE;
        bool _backupOversizedTargetFiles = DEFAULT_BACKUP_OVERSIZED_FILES;
        bool _isTextfileAccessible=false;
        LogLevels _printingLogLevel = LogLevels.All;

        public LogLevels PrintingLogLevel {
            get { return _printingLogLevel; }
            set { _printingLogLevel = value; }
        }

        #region " CONSTRUCTOR"

        public TextFileLogger()
        {
        }
     
        public TextFileLogger(FileInfo TargetLogFile)
            :this(TargetLogFile, DEFAULT_BACKUP_OVERSIZED_FILES)
        {
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="LogFileName">Filename of the target Logfile</param>
        /// <param name="BackupOversizedLogfiles"> If true, a new logfile will be created if the size 
        /// of the target logfile exceeds the maxFileSize threshold. actual date will be append 
        /// to the backup logfiles filename.
        /// If false, the target logfile will be deleted as soon as the maxFileSize threshhold is exceeded.
        /// All old data will be lost.</param>
        public TextFileLogger (FileInfo TargetLogFile, bool BackupOversizedLogfiles) {
            if (TargetLogFile is null) throw new NullReferenceException ();
            _fi = TargetLogFile;
            _backupOversizedTargetFiles = BackupOversizedLogfiles;
            _isTextfileAccessible = IsTextfileAccessible (TargetLogFile);
        }
   
        #endregion "CONSTRUCTOR"





        #region "PUBLICS"

        public void LogText(LogLevels MessageType, string Text)
        {
            if (_isTextfileAccessible) {
                string message = DateTime.Now.ToString(_logTextDateTimeFormat) + _logTextSeperator + MessageType.ToString("G") + _logTextSeperator + Text + "\r\n";
                WriteToTextfile(_fi,message);
                RenameOrDeleteMaxSizedFile(_backupOversizedTargetFiles);
            }
        }

        #endregion "PUBLICS"




        #region "PRIVATES"

        private void RenameOrDeleteMaxSizedFile(bool BackupOversizedLogfiles) {
            if (!_isTextfileAccessible) return;
            
            if (_fi.Length > _maxBackupFileSize) {
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
                    TargetLogfile.OpenWrite().Close();
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
