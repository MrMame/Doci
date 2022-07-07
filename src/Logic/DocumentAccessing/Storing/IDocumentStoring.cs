using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace DocumentAccessing.Storing
{

    /* Public Interface for Data tier Objects that provide Access to the storage system, e.g. Data.LuceneAccess.IndexingController
     will be used by the StoringController to get access to the Lucene string DB*/
    public interface IDocumentStoring
    {
        void Store (FileInfo storeFile);
        void Store (List<FileInfo> storeFiles);
    }
}
