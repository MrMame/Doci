using Mame.Doci.CrossCutting.Logging.Loggers;
using NUnit.Framework;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Text;
using Mame.Doci.CrossCutting.Logging.Data;

using Logging.Tests.TestSupports;

namespace Logging.Tests.UnitTests.Loggers
{
    [TestFixture]
    public partial class MultiLoggerTests
    {
        [Test]
        public void LogText_IfLoggersAdded_CallLogTextOnAllLoggersFromList ()
        {
            //ARRANGE
            ILogger LoggerA = Substitute.For<ILogger> ();
            var LoggerB = Substitute.For<ILogger> ();
            var TestMultiLogger = MultiLoggerFactory.CreateWithLoggers (LoggerA, LoggerB);
            
            //ACT
            var TargetLogLevel = LogLevels.Error;
            var TargetLogMessage = "Test logmessage for all loggers";
            TestMultiLogger.LogText (TargetLogLevel, TargetLogMessage);

            //ASSERT
            LoggerA.Received ().LogText (TargetLogLevel, TargetLogMessage);
            LoggerB.Received ().LogText (TargetLogLevel, TargetLogMessage);

        }
        [Test]
        public void LogText_IfNoLoggersAdded_ThrowsNoException ()
        {
            //ARRANGE
            var TestMultiLogger = MultiLoggerFactory.CreateParameterless ();
            //ACT
            //ASSERT
            Assert.DoesNotThrow (() =>
            {
                var TargetLogLevel = LogLevels.Error;
                var TargetLogMessage = "Test logmessage for all loggers";
                TestMultiLogger.LogText (TargetLogLevel, TargetLogMessage);
            }, "Calling LogText to empty Multilogger has thrown exception!");
            
        }

    }
}