
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using Mame.Doci.CrossCutting.Logging.Loggers;
using System.IO;

namespace Mame.Doci.CrossCutting.Logging.Tests.UnitTests.TextFileLoggerTests
{
    [TestFixture]
    public partial class TextFileLoggerTests
    {

        [Test]
        public void LogText_IfTargetFileIsAccessible_TextIsWritten()
        {
            //Arrange
            //Act
            //Assert
            Assert.Fail ("TestNotImplemented");
        }
        [Test]
        public void LogText_IfTargetFileIsNotAccessible_ThrowsException ()
        {
            //Arrange
            //Act
            //Assert
            Assert.Fail ("TestNotImplemented");
        }
        [Test]
        public void LogText_IfTargetFileIsNotExistingButAccessible_TextIsWrittenIntoNewFile ()
        {
            //Arrange
            //Act
            //Assert
            Assert.Fail ("TestNotImplemented");
        }

        [Test]
        public void LogText_IfTargetFileIsNULL_ThrowsException ()
        {
            //Arrange
            //Act
            //Assert
            Assert.Fail ("TestNotImplemented");
        }
        [Test]
        public void LogText_IfBackupIsFalseAndTargetFileMaxSizedReached_DeletesOldTargetFileAndAppendsToNewTargetFile ()
        {
            //Arrange
            //Act
            //Assert
            Assert.Fail ("TestNotImplemented");
        }
        [Test]
        public void LogText_IfBackupIsTrueAndTargetFileMaxSizedReached_CreatesBackupFileAndAppendsTextToNewTargetfile ()
        {
            //Arrange
            //Act
            //Assert
            Assert.Fail ("TestNotImplemented");
        }
        




    }
}
