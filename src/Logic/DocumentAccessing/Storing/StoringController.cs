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

     
    }
}
