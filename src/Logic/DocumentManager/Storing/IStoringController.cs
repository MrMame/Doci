using Mame.Doci.Logic.DocumentManager.Contracts.Interfaces;
using System.Collections.Generic;
using System.IO;

namespace Mame.Doci.Logic.DocumentManager.Storing
{
    interface IStoringController
    {

        void Store (FileInfo storeFile, IDocumentRepository documentStorer);
        void Store (List<FileInfo> storeFiles, IDocumentRepository documentStorer);


    }
}
