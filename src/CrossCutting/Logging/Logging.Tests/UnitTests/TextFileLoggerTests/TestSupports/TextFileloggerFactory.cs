using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using FactoryProduct = Mame.Doci.CrossCutting.Logging.Loggers.TextFileLogger;

namespace Mame.Doci.CrossCutting.Logging.Tests.UnitTests.TextFileLoggerTests.TestSupport
{
    static public class TextFileLoggerFactory
    {
        public static FactoryProduct CreateParameterless ()
        {
            return new FactoryProduct ();
        }
        public static FactoryProduct CreateWithNullReferenceTargetFile ()
        {
            FileInfo nullReferenceTargetFile = null;
            return new FactoryProduct (nullReferenceTargetFile);
        }
    }
}
