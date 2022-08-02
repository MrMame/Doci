using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Mame.Doci.CrossCutting.Logging.Contracts;
using Mame.Doci.CrossCutting.Logging.Contracts.Exceptions;
using Mame.Doci.Logic.DocumentManager.Contracts.Interfaces;
//using Mame.Doci.CrossCutting.Logging.Data; 

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
        public void Store (FileInfo storeFile, IDocumentRepository documentStorer)
        {
            if (storeFile is null) throw new ArgumentNullException ();
            if (documentStorer is null) throw new ArgumentNullException ();

            try
            {
                documentStorer.Store (storeFile);
            } catch (Exception ex)
            {
                throw new Exception ($"Error trying to store file ({storeFile.FullName}) " +
                                     $" using DocumentStorer ({documentStorer.ToString()})",
                                     ex);
            }

        }

        public void Store (List<FileInfo> storeFiles, IDocumentRepository documentStorer)
        {
            if (storeFiles is null) throw new ArgumentNullException ();
            if (documentStorer is null) throw new ArgumentNullException ();

            try
            {
                documentStorer.Store (storeFiles);
            } catch (Exception ex)
            {
                throw new Exception ($"Error trying to store files ({storeFiles}) " +
                                     $" using DocumentStorer ({documentStorer.ToString ()})",
                                     ex);
            }


        }
        #endregion

        #region "INTERFACE - IDocumentService"
        public void StoreDocument (FileInfo documentInfo)
        {
            if (documentInfo is null) throw new ArgumentNullException();
            if (_documentStorer is null) throw new ArgumentNullException ();
            try
            {
                this.Store (documentInfo, _documentStorer);
            } catch (Exception ex)
            {
                throw new Exception ($"Error while storing document for user with filename {documentInfo}",
                                    innerException:ex);               
            }
        }
        public void StoreDocuments (List<FileInfo> documentsInfos)
        {
            if (documentsInfos is null) throw new ArgumentNullException ();
            if (_documentStorer is null) throw new ArgumentNullException ();
            try
            {
                this.Store (documentsInfos, _documentStorer);
            } catch (Exception ex)
            {
                throw new Exception ($"Error while storing documents for user with filenames {documentsInfos}",
                                    innerException: ex);
            }
        }
        #endregion
    }
}
