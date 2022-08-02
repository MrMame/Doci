using Mame.Doci.CrossCutting.DataClasses;
using Mame.Doci.CrossCutting.Logging.Contracts;
using Mame.Doci.Logic.DocumentManager.Contracts.Interfaces;
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
                var document = new Document(new FileInfo (clp.AddDocument));
                CmdStoreDocument (logger, document);
            }

        }


        internal static void CmdStoreDocument (ILogger logger, Document document)
        {
            // Parameter Check
            if (document is null) throw new ArgumentNullException ();

            // Get File to add
            logger.LogText (LogLevels.Info,
                            "...Start adding file (" + document.FullName + ")");

            // Start storing
            try
            {
                if (!document.Exists())
                {
                    logger.LogText (LogLevels.Info, "Document is not existing (" + document.FullName + ")!");
                    return;
                }

                IDocumentRepository luceneIndexingController = Mame.Doci.Data.LuceneRepository.Factories.LuceneIndexingControllerFactory.CreateDefault (logger);
                IDocumentService documentStoringController = Mame.Doci.Logic.DocumentManager.Storing.Factories.StoringControllerFactory.CreateDefault (luceneIndexingController, logger);

                documentStoringController.StoreDocument (document);
            } catch (Exception ex)
            {
                logger.LogText (LogLevels.Fatal, "An unhandled exception occures while storing the document. " + ex.Message);
            }
        }

    }
}
