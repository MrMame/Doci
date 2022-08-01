using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Mame.Doci.Logic.DocumentAccessing.Contracts
{


    public interface IDocumentStoring
    {
        void Store (FileInfo storeFile);        // throws DocumentStoreException
        void Store (List<FileInfo> storeFiles);// throws DocumentStoreException
    }
}
