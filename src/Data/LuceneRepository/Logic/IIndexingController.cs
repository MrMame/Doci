using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Mame.Doci.CrossCutting.DataClasses;

namespace Mame.Doci.Data.LuceneRepository.Logic
{
    interface IIndexingController
    {
        /* Parentfoldername for imported Documents Filename in Index.
         * If is nullstring, the imported Docuemnts filename will hav only relative path name 
          as their importfolder as parent.*/
        String ParentTargetPathForImportedDocuments { get; set; }

        void AddToIndex (DirectoryInfo IndexFolder, bool createOrOverwriteExistingIndex,Document document);
        void AddToIndex (DirectoryInfo IndexFolder, bool createOrOverwriteExistingIndex,Document[] documents);
        void AddToIndex (DirectoryInfo IndexFolder, bool createOrOverwriteExistingIndex, DirectoryInfo ImportFolder, bool ImportWithSubfolders);
    }
}
