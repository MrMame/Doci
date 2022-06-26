
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using MarkusMeinhard.Doci.CrossCutting.Logging.Loggers;
using System.IO;

namespace MarkusMeinhard.Doci.CrossCutting.Logging.Tests.Unit.UnitTests.TextFileLoggerTests
{
    public partial class TextFileLoggerTests
    {



        [Test]
        public void Constructor_TargetLogFileParameterIsNull_ThrowsNullException ()
        {
            //Arrange
            
            //Act
            
            //Assert
            Assert.Throws<NullReferenceException> (
                delegate {
                    FileInfo NullFileInfo = null;
                    var TestTextfileLogger = new TextFileLogger (NullFileInfo);
                },
                "No null reference eception thrown!");
          
        }



    }
}
