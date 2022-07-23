using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;
using Mame.Doci.CrossCutting.Logging.Contracts;

namespace Mame.Doci.CrossCutting.Logging.Loggers
{
    public class TextFileLogger:ILogger
    {

        private const bool DEFAULT_BACKUP_OVERSIZED_FILES = true;
        private const Int32 DEFAULT_BACKUP_MAXSIZE = 150000000;
        private const string DEFAULT_FORMATSTRING_BACKUP_DATEFILENAME = "_yyyyMMdd_hhmmss";
        private const string DEFAULT_FORMATSTRING_LOGTEXT_DATEFORMAT = "yyyyMMdd_HH:mm:ss";

        FileInfo _TargetFileInfo;
        char _logTextSeperator = ';';
        string _logTextDateTimeFormat = DEFAULT_FORMATSTRING_LOGTEXT_DATEFORMAT;
        long _maxBackupFileSize = DEFAULT_BACKUP_MAXSIZE;
        bool _backupOversizedTargetFiles = DEFAULT_BACKUP_OVERSIZED_FILES;
        bool _isTextfileAccessible=false;
        LogLevels _printingLogLevel = LogLevels.All;

        public string BackupDateFormat
        {
            get { return DEFAULT_FORMATSTRING_BACKUP_DATEFILENAME; }
        }


        public bool BackupOversizedLogfiles
        {
            get { return _backupOversizedTargetFiles; }
            set { _backupOversizedTargetFiles = value; }
        }

        public Int32 MaxNumberOfBytes
        {
            get { return DEFAULT_BACKUP_MAXSIZE; }
        }

        public LogLevels PrintingLogLevel {
            get { return _printingLogLevel; }
            set { _printingLogLevel = value; }
        }

        public FileInfo TargetFile {
            get { return _TargetFileInfo; }
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
            _TargetFileInfo = TargetLogFile;
            _backupOversizedTargetFiles = BackupOversizedLogfiles;
            _isTextfileAccessible = IsTextfileAccessible (TargetLogFile);
        }
   
        #endregion "CONSTRUCTOR"





        #region "PUBLICS"

        public void LogText(LogLevels MessageType, string Text)
        {
            _TargetFileInfo.Refresh ();
            if (_isTextfileAccessible) {
                string message = DateTime.Now.ToString(_logTextDateTimeFormat) + _logTextSeperator + MessageType.ToString("G") + _logTextSeperator + Text + "\r\n";
                RenameOrDeleteMaxSizedFile(_backupOversizedTargetFiles);
                WriteToTextfile(_TargetFileInfo,message);
            }
            _TargetFileInfo.Refresh ();
        }

        #endregion "PUBLICS"




        #region "PRIVATES"

        private void RenameOrDeleteMaxSizedFile(bool BackupOversizedLogfiles) {
            _TargetFileInfo.Refresh ();
            if (!_isTextfileAccessible) return;
            if (_TargetFileInfo.Length > _maxBackupFileSize) {
                if (BackupOversizedLogfiles) {
                    _TargetFileInfo.CopyTo(_TargetFileInfo.DirectoryName + "\\" +
                                _TargetFileInfo.Name.Replace(_TargetFileInfo.Extension, "")
                                + DateTime.Now.ToString(DEFAULT_FORMATSTRING_BACKUP_DATEFILENAME) 
                                + _TargetFileInfo.Extension.ToString());
                    _TargetFileInfo.Delete ();
                } else {
                    _TargetFileInfo.Delete();
                }
            }
            _TargetFileInfo.Refresh ();
        }

        private void WriteToTextfile(FileInfo TargetLogfile,string Message)
        {
            if (!_isTextfileAccessible) return;

            using (FileStream fs = TargetLogfile.OpenWrite())
            {
                Byte[] info =
                    new UTF8Encoding(true).GetBytes(Message);
                fs.Position = fs.Length;        // Set file inputmarker to the files end
                fs.Write(info, 0, info.Length);
            }

        }


        private bool IsTextfileAccessible(FileInfo TargetLogfile) {
   
            if (!TargetLogfile.Exists) {
                TargetLogfile.CreateText().Close();
            } else {
                TargetLogfile.OpenWrite().Close();
            }
            return true;
        }

        #endregion
    }
}
