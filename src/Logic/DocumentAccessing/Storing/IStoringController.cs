using Mame.Doci.Logic.DocumentAccessing.Contracts;
using System.Collections.Generic;
using System.IO;

namespace Mame.Doci.Logic.DocumentAccessing.Storing
{
    interface IStoringController
    {

        void Store (FileInfo storeFile, IDocumentStoring documentStorer);
        void Store (List<FileInfo> storeFiles, IDocumentStoring documentStorer);


    }
}
