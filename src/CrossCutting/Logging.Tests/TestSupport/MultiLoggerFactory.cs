using Mame.Doci.CrossCutting.Logging.Contracts;
using Mame.Doci.CrossCutting.Logging.Loggers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Logging.Tests.TestSupports
{
    static class MultiLoggerFactory
    {
        static public MultiLogger CreateParameterless ()
        {
            return new MultiLogger ();
        }

        static public MultiLogger CreateWithLoggers (ILogger loggerA, ILogger loggerB)
        {
            var theMlog = new MultiLogger ();
            theMlog.AddLogger (loggerA);
            theMlog.AddLogger (loggerB);
            return theMlog;
        }
    }
}
