using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using Mame.Doci.CrossCutting.Logging.Tests.UnitTests.MultiLoggerTests.TestSupports;
using Mame.Doci.CrossCutting.Logging.Loggers;

namespace Mame.Doci.CrossCutting.Logging.Tests.UnitTests.MultiLoggerTests
{
    [TestFixture]
    public partial class MultiLoggerTests
    {
        [Test]
        public void AddLogger_IfLoggerIsNULL_ThrowsArgumentNullException ()
        {
            //ARRANGE
            MultiLogger testLogger = new MultiLogger ();
            //ACT
            //ASSERT
            Assert.Throws<ArgumentNullException> (() =>
            {
                ILogger theNullLogger = null;
                testLogger.AddLogger (theNullLogger);
            }, "Adding a NULL Logger has not thrown ArgumenNullException");
        }


    }
}
