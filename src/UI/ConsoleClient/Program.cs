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
            // Init Logging Objects
            ILogger logger = new ConsoleLogger ();

            // Parsing commandline
            var clp = new CLParsing.CommandLineArgumentsParser (args);
            if (clp.HasParsingErrors == true)
            {
                logger.LogText(LogLevels.Error, "Invalid commandline parameters detected!");
                return;
            }
            // Start doing commands
            // -> AddDcoument
            if (!string.IsNullOrEmpty (clp.AddDocument))
            {
                StoreDocumentForUser (logger,clp.AddDocument);
            }
            // End
            logger.LogText (LogLevels.Info, "Program finished!");
        }





        private static void StoreDocumentForUser (ILogger logger,string documentFileName)
        {
            // Parameter Check
            if (string.IsNullOrEmpty (documentFileName)) throw new ArgumentNullException ();
            // Get File to add
            logger.LogText (LogLevels.Info, "...Start adding file (" + documentFileName + ")");
            // Return if file is not existing
            var documentFile = new FileInfo (documentFileName);
            if (!documentFile.Exists)
            {
                logger.LogText (LogLevels.Info, "File to add is not existing (" + documentFile.FullName + ")!");
                return;
            }
            // Start storing
            try
            {
                IStoringForUser documentStoreController = StoringControllerFactory.CreateDefault (logger);
                documentStoreController.UserWantsToStore (documentFile);
            } catch (Exception ex)
            {
                logger.LogText (LogLevels.Fatal, "An unhandled exception occures while storing the document. " + ex.Message);
            }
        }

    }
}
