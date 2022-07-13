using Mame.Doci.CrossCutting.Logging.Data;
using Mame.Doci.CrossCutting.Logging.Loggers;
using Mame.Doci.Logic.DocumentAccessing.Storing.Factories;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Mame.Doci.UI.ConsoleClient
{
    public class Program
    {
      


        static void Main (string[] args)
        {
            // Create Logging
            ILogger logger = new ConsoleLogger ();

            // Parsing commandline
            var clp = new CLParsing.CommandLineArgumentsParser (args);

            if (clp.HasParsingErrors == true){
                logger.LogText(LogLevels.Error, "Invalid commandline parameters detected!");
            }
            else if (!string.IsNullOrEmpty (clp.AddDocument))
            {
                logger.LogText (LogLevels.Info, "...Start adding file (" + clp.AddDocument + ")");
                var documentFilename = new FileInfo(clp.AddDocument);

                IStoringForUser documentStoreController = StoringControllerFactory.CreateDefault (logger);
                documentStoreController.UserWantsToStore (documentFilename);
            }

        }

    }
}
