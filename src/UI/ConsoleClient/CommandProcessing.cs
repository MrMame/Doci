using Mame.Doci.CrossCutting.Logging.Contracts;
using Mame.Doci.Logic.DocumentManager.Contracts;
using Mame.Doci.UI.ConsoleClient.CLParsing;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mame.Doci.UI.ConsoleClient
{
    internal static class CommandProcessing
    {


        internal static void DoCommands (CommandLineArgumentsParser clp, ILogger logger)
        {
            if (!string.IsNullOrEmpty (clp.AddDocument))
            {
                // Return if file is not existing
                var documentFile = new FileInfo (clp.AddDocument);
                CmdStoreDocument (logger, documentFile);
            }

        }


        internal static void CmdStoreDocument (ILogger logger, FileInfo documentFile)
        {
            // Parameter Check
            if (documentFile is null) throw new ArgumentNullException ();

            // Get File to add
            logger.LogText (LogLevels.Info,
                            "...Start adding file (" + documentFile.FullName + ")");

            // Start storing
            try
            {
                if (!documentFile.Exists)
                {
                    logger.LogText (LogLevels.Info, "Document is not existing (" + documentFile.FullName + ")!");
                    return;
                }

                IDocumentStoring luceneIndexingController = Mame.Doci.Data.LuceneRepository.Factories.LuceneIndexingControllerFactory.CreateDefault (logger);
                IStoringForUser documentStoringController = Mame.Doci.Logic.DocumentManager.Storing.Factories.StoringControllerFactory.CreateDefault (luceneIndexingController, logger);

                documentStoringController.UserWantsToStore (documentFile);
            } catch (Exception ex)
            {
                logger.LogText (LogLevels.Fatal, "An unhandled exception occures while storing the document. " + ex.Message);
            }
        }

    }
}
