using DocumentAccessing.Storing;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Mame.Doci.UI.ConsoleClient;


namespace Mame.Doci.Logic.DocumentAccessing.Storing
{
    public class StoringController :IStoringController, IStoringForUser
    {

        IDocumentStoring _documentStorer = null;

        #region "PUBLICS"
        
        public StoringController ()
        {
        }
        public StoringController(IDocumentStoring documentStorer)
        {
            _documentStorer = documentStorer;
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
            this.Store (fileName, _documentStorer);
        }
        public void UserWantsToStore (List<FileInfo> fileNames)
        {
            this.Store (fileNames, _documentStorer);
        }
        #endregion
    }
}
