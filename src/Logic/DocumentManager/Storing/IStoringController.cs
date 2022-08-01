using Mame.Doci.Logic.DocumentManager.Contracts;
using System.Collections.Generic;
using System.IO;

namespace Mame.Doci.Logic.DocumentManager.Storing
{
    interface IStoringController
    {

        void Store (FileInfo storeFile, IDocumentStoring documentStorer);
        void Store (List<FileInfo> storeFiles, IDocumentStoring documentStorer);


    }
}
