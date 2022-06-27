using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Mame.Doci.CrossCutting.Logging.Loggers;

namespace Mame.Doci.CrossCutting.Logging.Tests.UnitTests.TextFileLoggerTests.TestSupport
{
    static public class TextFileLoggerFactory
    {
        public static TextFileLogger CreateParameterless ()
        {
            return new TextFileLogger ();
        }
        public static TextFileLogger CreateWithNullReferenceTargetFile ()
        {
            FileInfo nullReferenceTargetFile = null;
            return new TextFileLogger (nullReferenceTargetFile);
        }

        public static TextFileLogger CreateWithNotExistingWriteableTargetFile ()
        {
            string TargetFileName = Environment.GetFolderPath (Environment.SpecialFolder.MyDocuments) + "\\TextFileLoggerTesting.Log";
            FileInfo WriteableTargetFile = new FileInfo(TargetFileName);
            if (WriteableTargetFile.Exists) WriteableTargetFile.Delete ();
            return new TextFileLogger (WriteableTargetFile);
        }
    }
}
