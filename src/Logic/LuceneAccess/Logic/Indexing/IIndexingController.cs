using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Mame.Doci.Logic.LuceneAccess.Logic.Indexing
{
    interface IIndexingController
    {
        /* Parentfoldername for imported Documents Filename in Index.
         * If is nullstring, the imported Docuemnts filename will hav only relative path name 
          as their importfolder as parent.*/
        String ParentTargetPathForImportedDocuments { get; set; }

        void AddToIndex (DirectoryInfo IndexFolder, FileInfo ImportFile);
        void AddToIndex (DirectoryInfo IndexFolder, FileInfo[] ImportFiles);
        void AddToIndex (DirectoryInfo IndexFolder, DirectoryInfo ImportFolder, bool ImportWithSubfolders);
    }
}
