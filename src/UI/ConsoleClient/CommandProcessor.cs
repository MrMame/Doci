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


        public ILogger Logger { get; set; }
        public IDocumentService DocumentService { get; set; }
        public IDocumentRepository DocumentRepository { get; set; }


        public CommandProcessor (ILogger logger, IDocumentService documentService, IDocumentRepository documentRepository)
        {
            this.Logger = logger;   
            this.DocumentService = documentService;
            this.DocumentRepository = documentRepository;
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


        public void CmdStoreDocument ( Document document)
        {
            // Parameter Check
            if (document is null) throw new ArgumentNullException ();

            // Get File to add
            this.Logger.LogText (LogLevels.Info,
                            "...Start adding file (" + document.FullName + ")");

            // Start storing
            try
            {
                if (!document.Exists())
                {
                    this.Logger.LogText (LogLevels.Info, "Document is not existing (" + document.FullName + ")!");
                    return;
                }

                this.DocumentService.StoreDocument (document);
            } catch (Exception ex)
            {
                this.Logger.LogText (LogLevels.Fatal, "An unhandled exception occures while storing the document. " + ex.Message);
            }
        }

    }
}
