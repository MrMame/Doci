﻿using System;
using System.Collections.Generic;
using System.IO;
using NSubstitute;
using DocumentAccessing.Storing;
using Mame.Doci.CrossCutting.Logging.Data;
using Mame.Doci.CrossCutting.Logging.Loggers;
using Mame.Doci.Logic.DocumentAccessing.Storing;
using NUnit.Framework;

namespace Mame.Doci.Logic.DocumentAccessing.Tests.UnitTests.Storing
{
    public partial class StoringControllerUnitTests
    {
        [Test]
        public void LogMessage_IfNoLoggerIsSet_ThrowsNoException ()
        {
            //ARRANGE
            List<FileInfo> fileinfos = new List<FileInfo> ();
            IDocumentStoring docStorer = null;
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
            IDocumentStoring docStorer = null;
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