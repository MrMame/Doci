using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Mame.Doci.CrossCutting.Logging.Contracts;
using Mame.Doci.CrossCutting.Logging.Contracts.Exceptions;
using Mame.Doci.Logic.DocumentManager.Contracts.Interfaces;
using Mame.Doci.CrossCutting.DataClasses;
using Mame.Doci.Logic.DocumentManager.Contracts.Exceptions;

namespace Mame.Doci.Logic.DocumentManager.Storing
{
    public class StoringController :IStoringController, IDocumentService, ILoggable
    {

        IDocumentRepository _documentStorer = null;
        ILogger _logger = null;



        #region "PUBLICS"
        public ILogger Logger
        {
            get { return _logger; }
            set { _logger = value; }
        }
        
        public StoringController ()
        {
        }
        public StoringController(IDocumentRepository documentStorer)
        {
            _documentStorer = documentStorer;
        }
        #endregion

        #region "INTERFACE - ILoggable"
        public void LogMessage (LogLevels logLevel, string message)
        {
            try
            {
                if (this._logger != null) _logger.LogText (logLevel, message);
            } catch (Exception ex )
            {
                throw new LogMessageException ($"Error trying to log message ({message}) ",
                                            ex);
            }
        }
        #endregion
        
        #region "INTERFACE - IStoringController"
        public void Store (Document document, IDocumentRepository documentRepository)
        {
            if (document is null) throw new ArgumentNullException ();
            if (documentRepository is null) throw new ArgumentNullException ();

            try
            {
                documentRepository.WriteToRepository (document);
            } catch (Exception ex)
            {
                throw new DocumentStoreException ($"Error trying to store file ({document.FullName}) " +
                                     $" using DocumentStorer ({documentRepository.ToString()})",
                                     ex);
            }

        }

        public void Store (List<Document> documents, IDocumentRepository documentRepository)
        {
            if (documents is null) throw new ArgumentNullException ();
            if (documentRepository is null) throw new ArgumentNullException ();

            try
            {
                documentRepository.WriteToRepository (documents);
            } catch (Exception ex)
            {
                throw new Exception ($"Error trying to store files ({documents}) " +
                                     $" using DocumentStorer ({documentRepository.ToString ()})",
                                     ex);
            }


        }
        #endregion

        #region "INTERFACE - IDocumentService"
        public void StoreDocument (Document document)
        {
            if (document is null) throw new ArgumentNullException();
            if (_documentStorer is null) throw new ArgumentNullException ();
            try
            {
                this.Store (document, _documentStorer);
            } catch (Exception ex)
            {
                throw new Exception ($"Error while storing document for user with filename {document}",
                                    innerException:ex);               
            }
        }
        public void StoreDocuments (List<Document> documents)
        {
            if (documents is null) throw new ArgumentNullException ();
            if (_documentStorer is null) throw new ArgumentNullException ();
            try
            {
                this.Store (documents, _documentStorer);
            } catch (Exception ex)
            {
                throw new Exception ($"Error while storing documents for user with filenames {documents}",
                                    innerException: ex);
            }
        }
        #endregion
    }
}
