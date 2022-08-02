using System;
using System.Collections.Generic;
using System.IO;
using NSubstitute;
using Mame.Doci.CrossCutting.Logging.Contracts;
using Mame.Doci.Logic.DocumentManager.Storing;
using NUnit.Framework;
using Mame.Doci.Logic.DocumentManager.Contracts.Interfaces;

namespace Mame.Doci.Logic.DocumentAccessing.Tests.UnitTests.Storing
{
    public partial class StoringControllerUnitTests
    {
        [Test]
        public void LogMessage_IfNoLoggerIsSet_ThrowsNoException ()
        {
            //ARRANGE
            List<FileInfo> fileinfos = new List<FileInfo> ();
            IDocumentRepository docStorer = null;
            var theController = new StoringController (docStorer);

            //ACT
            theController.LogMessage (logLevel: LogLevels.Info,"This message is going nowhere.");

            //ASSERT
            /*  No Eception means Test success */
        }

        [Test]
        public void LogMessage_IfLoggerIsSet_LoggerLogText ()
        {
            //ARRANGE
            List<FileInfo> fileinfos = new List<FileInfo> ();
            IDocumentRepository docStorer = null;
            var theController = new StoringController (docStorer);

            var theLogger = Substitute.For<ILogger> ();
            theController.Logger = theLogger;

            string logMessage = "This message is going nowhere.";
            LogLevels logLvl = LogLevels.Info;

            //ACT
            theController.LogMessage (logLvl, logMessage);

            //ASSERT
            theLogger.Received().LogText(logLvl, logMessage);

        }

    }
}
