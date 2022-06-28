
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
        public void Constructor_UsingParameterless_ThrowsNoNullException ()
        {
            //Arrange
            //Act
            //Assert
            Assert.DoesNotThrow(
                delegate {
                    var TestTextfileLogger = TextFileLoggerFactory.CreateParameterless();
                },
                "Parameterless constructor shall not throw Exception if used!");
        }


        [Test]
        public void Constructor_TargetLogFileParameterIsNull_ThrowsNullException ()
        {
            //Arrange
            
            //Act
            
            //Assert
            Assert.Throws<NullReferenceException> (
                delegate {
                    var TestTextfileLogger = TextFileLoggerFactory.CreateWithNullReferenceTargetFile();
                },
                "No null reference eception thrown!");
          
        }

        [Test]
        [TestCase ("Invalid::Filename.txt")]
        [TestCase ("Invalid>>Filename.txt")]
        [TestCase ("Invalid<<Filename.txt")]
        [TestCase ("Invalid\"Filename.txt")]
        [TestCase ("Invalid??Filename.txt")]
        [TestCase ("Invalid||Filename.txt")]
        [TestCase ("Invalid**Filename.txt")]
        public void Constructor_IfTargetfileNameHasInvalidChars_ThrowsIOExceptionn (string testFileName)
        {
            //Arrange
            var FileInfoInvalidName = new FileInfo (testFileName);
            //Act
            //Assert
            Assert.Throws<System.IO.IOException> (() => {
                TextFileLogger TestLogger = TextFileLoggerFactory.CreateWithFileInfo (FileInfoInvalidName);
            }, "System.IO.IOException was not thrown.");

        }

    }
}
