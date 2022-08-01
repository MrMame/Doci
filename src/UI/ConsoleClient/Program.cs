using Mame.Doci.CrossCutting.Logging.Contracts;
using Mame.Doci.CrossCutting.Logging.Loggers;
using Mame.Doci.Logic.DocumentManager.Contracts;

using System;
using System.IO;
using Mame.Doci.UI.ConsoleClient.CLParsing;

namespace Mame.Doci.UI.ConsoleClient
{
    public class Program
    {

       


        static void Main (string[] args)
        {
            // Init Logging Objects
            ILogger logger = new ConsoleLogger ();

            try
            {
                // Parsing commandline
                var clp = new CommandLineArgumentsParser (args);
                if (clp.HasParsingErrors == true)
                {
                    logger.LogText (LogLevels.Error, "Invalid commandline parameters detected!");
                    return;
                }

                // process the commandline commands
                CommandProcessing.DoCommands (clp, logger);

                // End
                logger.LogText (LogLevels.Info, "Program finished!");

            } catch (Exception ex)
            {
                logger.LogText (LogLevels.Fatal, "Fatal Error\r\n" +
                                                ex.ToString ());
            }
        }


    }
}
