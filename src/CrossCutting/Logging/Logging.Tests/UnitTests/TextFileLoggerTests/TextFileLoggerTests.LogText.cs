
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using Mame.Doci.CrossCutting.Logging.Loggers;
using System.IO;
using Mame.Doci.CrossCutting.Logging.Tests.UnitTests.TextFileLoggerTests.TestSupport;
using System.Linq;

namespace Mame.Doci.CrossCutting.Logging.Tests.UnitTests.TextFileLoggerTests
{
    [TestFixture]
    public partial class TextFileLoggerTests
    {

        #region "TESTS"

        [Test]
        public void LogText_IfTargetFileIsExistingAndAccessible_TextIsAppend()
        {
            //Arrange
            var WriteableTextLogger = TextFileLoggerFactory.CreateWithExistingWriteableTargetFile ();
            FileInfo TargetFile = WriteableTextLogger.TargetFile;
            
            //Act
            WriteableTextLogger.LogText (Data.LogLevels.Warning, "This is a warning message.");
            
            //Assert
            using (StreamReader sr = TargetFile.OpenText ()) { 
                Assert.IsTrue (sr.ReadToEnd ().EndsWith("This is a warning message.\r\n"));
                sr.Close ();
            } ;
            
            // CleanUp
            TargetFile.Delete ();
        }

        [Test]
        public void LogText_IfTargetFileIsNotAccessible_ThrowsUnauthorizedAccessException ()
        {
            //Arrange
            var TextLogger = TextFileLoggerFactory.CreateWithExistingWriteableTargetFile ();
            FileInfo TargetFile = TextLogger.TargetFile;
            TargetFile.Attributes = FileAttributes.ReadOnly;
            //Act
            //Assert
            Assert.Throws<System.UnauthorizedAccessException> (() =>
            {
                TextLogger.LogText(Data.LogLevels.Warning,"Logggingmessage bu file is not accessible");
            }, "System.UnauthorizedAccessException was not thrown");
            // CleanUp
            TargetFile.Attributes = FileAttributes.Normal;
            TargetFile.Delete ();
        }

        [Test]
        public void LogText_IfTargetFileIsNotExistingButAccessible_TextIsWrittenIntoNewFile ()
        {
            //Arrange
            var WriteableTextLogger = TextFileLoggerFactory.CreateWithNotExistingWriteableTargetFile ();
            FileInfo TargetFile = WriteableTextLogger.TargetFile;

            //Act
            WriteableTextLogger.LogText (Data.LogLevels.Warning, "This is a warning message.");

            //Assert
            using (StreamReader sr = TargetFile.OpenText ())
            {
                Assert.IsTrue (sr.ReadToEnd ().Contains ("This is a warning message."));
                sr.Close ();
            };

            // CleanUp
            TargetFile.Delete ();
        }

        [Test]
        public void LogText_IfBackupIsFalseAndTargetFileMaxSizeReached_OldTargetFileIsDeletedAndMessageAppendsToNewTargetFile ()
        {
            //Arrange
            TextFileLogger WriteableTextLogger = TextFileLoggerFactory.CreateWithNotExistingWriteableTargetFile ();
            WriteableTextLogger.BackupOversizedLogfiles = false;

            Int32 NumberOfBytesForExtrapayload = WriteableTextLogger.MaxNumberOfBytes + 10000;
            ExtraPayloadTargetFile (WriteableTextLogger.TargetFile, NumberOfBytesForExtrapayload);

            //Act
            WriteableTextLogger.LogText (Data.LogLevels.Warning, "This is the stroke that brokes the camels back.");

            //Assert
            Assert.Less (WriteableTextLogger.TargetFile.Length, WriteableTextLogger.MaxNumberOfBytes,"TargetFile was not deleted because of MaximumBytes! Message was added to old oversized file instead.");

            // CleanUp
            WriteableTextLogger.TargetFile.Delete ();

        }


        [Test]
        public void LogText_IfBackupIsTrueAndTargetFileMaxSizedReached_CreatesBackupFileAndAppendsTextToNewTargetfile ()
        {
            //Arrange
            /* Define the name 0of the Logfile and delete if any files (backupsincluded) are still exsiting from previous tests*/
            string TargetFileFolder = Environment.GetFolderPath (Environment.SpecialFolder.MyDocuments) + "\\";
            string TargetFileNameNoExt = "TextFileLoggerTesting"; 
            string TargetFileExt = ".Log";
            string targetFileFullname = TargetFileFolder + TargetFileNameNoExt + TargetFileExt;
            TextFileLogger WriteableTextLogger = TextFileLoggerFactory.CreateWithFileInfo (new FileInfo(targetFileFullname));
            string TargetFileNameBackupDatePlaceholders = string.Concat (Enumerable.Repeat ("?", WriteableTextLogger.BackupDateFormat.Length));
            RemoveExistingFiles (new DirectoryInfo(TargetFileFolder), 
                                        TargetFileNameNoExt + TargetFileNameBackupDatePlaceholders + TargetFileExt);
            /* Create Logfile and fill with placeholder bytes to exceed maxFileSize */
            WriteableTextLogger.BackupOversizedLogfiles = true;
            Int32 NumberOfBytesForExtrapayload = WriteableTextLogger.MaxNumberOfBytes + 10000;
            ExtraPayloadTargetFile (WriteableTextLogger.TargetFile, NumberOfBytesForExtrapayload);

            //Act
            WriteableTextLogger.LogText (Data.LogLevels.Warning, "This is the stroke that brokes the camels back.");

            //Assert
            bool isBackupFileExisting = (WriteableTextLogger.TargetFile.Directory.GetFiles (TargetFileNameNoExt + TargetFileNameBackupDatePlaceholders + TargetFileExt).Count() == 2);
            Assert.True (isBackupFileExisting, "It seems that no Backupfile was created.");
            Assert.Less (WriteableTextLogger.TargetFile.Length, WriteableTextLogger.MaxNumberOfBytes, "TargetFile seems not to be deleted because of ist size ! Message was added to old oversized file instead of a small sized new file.");
            

            // CleanUp
            RemoveExistingFiles (new DirectoryInfo (TargetFileFolder),
                                TargetFileNameNoExt + TargetFileNameBackupDatePlaceholders + TargetFileExt);

        }

        #endregion



        #region "PRIVATES"

        private void RemoveExistingFiles (DirectoryInfo TargetDirectory ,string FilenamePattern)
        {  
            foreach (FileInfo fi in TargetDirectory.GetFiles (FilenamePattern))
            {
                fi.Attributes = FileAttributes.Normal;
                fi.Delete();
            }
        }

        private void ExtraPayloadTargetFile (FileInfo OversizedTargetFile, Int32 NumberOfBytes)
        {
            Int32 MoreBytesThanMaximum = NumberOfBytes;
            char[] payload = CreatePayload (MoreBytesThanMaximum, FillChar: 'X');
            using (StreamWriter sr = OversizedTargetFile.AppendText ())
            {
                sr.Write (payload);
                sr.Close ();
            }
            OversizedTargetFile.Refresh ();
        }

        private char[] CreatePayload (Int32 NumberOfBytes,char FillChar)
        {
            char[] payload = new char[NumberOfBytes];
            for (int i = 0; i < payload.Length; i++)
            {
                payload[i] = FillChar;
            }
            return payload;
        }

        #endregion

    }
}
