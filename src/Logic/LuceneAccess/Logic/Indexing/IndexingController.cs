using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Mame.Doci.Logic.LuceneAccess.Logic.Indexing
{
    public class IndexingController : IIndexingController
    {
        public string ParentTargetPathForImportedDocuments { get => throw new NotImplementedException (); set => throw new NotImplementedException (); }

        #region "PUBLICS"

        public void AddToIndex (DirectoryInfo IndexFolder, FileInfo ImportFile)
        {
            throw new NotImplementedException ();
        }

        public void AddToIndex (DirectoryInfo IndexFolder, FileInfo[] ImportFiles)
        {
            throw new NotImplementedException ();
        }

        public void AddToIndex (DirectoryInfo IndexFolder, DirectoryInfo ImportFolder, bool ImportWithSubfolders)
        {
            throw new NotImplementedException ();
        }

        #endregion


        #region "PRIVATES"



        #endregion

    }
}
