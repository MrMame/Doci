using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Mame.Doci.CrossCutting.Logging.Contracts;
using Mame.Doci.Logic.DocumentAccessing.Contracts;
//using Mame.Doci.CrossCutting.Logging.Data; 

namespace Mame.Doci.Logic.DocumentAccessing.Storing
{
    public class StoringController :IStoringController, IStoringForUser, ILoggable
    {

        IDocumentStoring _documentStorer = null;
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
        public StoringController(IDocumentStoring documentStorer)
        {
            _documentStorer = documentStorer;
        }


        public void LogMessage (LogLevels logLevel, string message)
        {
            if (this._logger != null) _logger.LogText (logLevel, message);
        }

        public void Store (FileInfo storeFile, IDocumentStoring documentStorer)
        {
            if (storeFile is null) throw new ArgumentNullException ();
            if (documentStorer is null) throw new ArgumentNullException ();

            documentStorer.Store (storeFile);

        }

        public void Store (List<FileInfo> storeFiles, IDocumentStoring documentStorer)
        {
            if (storeFiles is null) throw new ArgumentNullException ();
            if (documentStorer is null) throw new ArgumentNullException ();

            documentStorer.Store (storeFiles);
            
        }
        #endregion

        #region "INTERFACE - IStoringForUser"
        public void UserWantsToStore (FileInfo fileName)
        {
            try
            {
                this.Store (fileName, _documentStorer);
            } catch (Exception ex)
            {
                throw new Exception ($"Error while storing document for user with filename {fileName}",
                                    innerException:ex);               
            }
        }
        public void UserWantsToStore (List<FileInfo> fileNames)
        {
            try
            {
                this.Store (fileNames, _documentStorer);
            } catch (Exception ex)
            {
                throw new Exception ($"Error while storing documents for user with filenames {fileNames}",
                                    innerException: ex);
            }
        }
        #endregion
    }
}
