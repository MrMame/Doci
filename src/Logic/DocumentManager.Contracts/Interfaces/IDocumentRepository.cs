using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Mame.Doci.Logic.DocumentManager.Contracts.Interfaces
{


    public interface IDocumentRepository
    {
        void WriteToRepository (FileInfo storeFile);        // throws DocumentStoreException
        void WriteToRepository (List<FileInfo> storeFiles);// throws DocumentStoreException
    }
}
