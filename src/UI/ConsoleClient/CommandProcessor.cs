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
    public class CommandProcessor
    {


        ILogger _logger;
        IDocumentService _documentService;
        IDocumentRepository _documentRepository;


        #region "Publics"
        public CommandProcessor (ILogger logger, IDocumentService documentService, IDocumentRepository documentRepository)
        {
            _logger = logger;   
            _documentService = documentService;
            _documentRepository = documentRepository;
        }
        public void DoCommands (CommandLineArgumentsParser clp)
        {
            if (!string.IsNullOrEmpty (clp.AddDocument))
            {
                // Return if file is not existing
                var document = new Document(new FileInfo (clp.AddDocument));
                CmdStoreDocument (document);
            }

        }
        #endregion



        #region "Privates"
        private void CmdStoreDocument ( Document document)
        {
            // Parameter Check
            if (document is null) throw new ArgumentNullException ();

            // Get File to add
            _logger.LogText (LogLevels.Info,
                            "...Start adding file (" + document.FullName + ")");

            // Start storing
            try
            {
                if (!document.Exists())
                {
                    _logger.LogText (LogLevels.Info, "Document is not existing (" + document.FullName + ")!");
                    return;
                }

                _documentService.StoreDocument (document);
            } catch (Exception ex)
            {
                _logger.LogText (LogLevels.Fatal, "An unhandled exception occures while storing the document. " + ex.Message);
            }
        }
        #endregion

    }
}
