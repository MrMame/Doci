
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using Mame.Doci.CrossCutting.Logging.Loggers;
using System.IO;
using Mame.Doci.CrossCutting.Logging.Tests.UnitTests.TextFileLoggerTests.TestSupport;

namespace Mame.Doci.CrossCutting.Logging.Tests.UnitTests.TextFileLoggerTests
{
    [TestFixture]
    public partial class TextFileLoggerTests
    {

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
        public void LogText_IfBackupIsFalseAndTargetFileMaxSizedReached_DeletesOldTargetFileAndAppendsToNewTargetFile ()
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
            //Act
            //Assert
            Assert.Fail ("TestNotImplemented");
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



    }
}
