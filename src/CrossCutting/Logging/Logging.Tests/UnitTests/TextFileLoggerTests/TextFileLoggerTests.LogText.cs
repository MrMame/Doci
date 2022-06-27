
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
        public void LogText_ToAccessibleTargetFile_WritesIntoTargetfile ()
        {
            //Arrange
            //Act
            //Assert
            Assert.Fail ("TestNotImplemented");
        }
        [Test]
        public void LogText_ToNotAccessibleTargetFile_ThrowsException ()
        {
            //Arrange
            //Act
            //Assert
            Assert.Fail ("TestNotImplemented");
        }
        [Test]
        public void LogText_ToNotExistingButAccessibleTargetFile_WritesIntoTargetfile ()
        {
            //Arrange
            //Act
            //Assert
            Assert.Fail ("TestNotImplemented");
        }

        [Test]
        public void LogText_ToNULLTargetFile_ThrowsException ()
        {
            //Arrange
            //Act
            //Assert
            Assert.Fail ("TestNotImplemented");
        }
        [Test]
        public void LogText_ToMaxSizedTargetIfBackupIsFalse_DeletesOldFileAndAppendsToNewFile ()
        {
            //Arrange
            //Act
            //Assert
            Assert.Fail ("TestNotImplemented");
        }
        [Test]
        public void LogText_ToMaxSizedTargetIfBackupIsTrue_CreatesBackupFileAndAppendsTextToNewTargetfile ()
        {
            //Arrange
            //Act
            //Assert
            Assert.Fail ("TestNotImplemented");
        }
        




    }
}
