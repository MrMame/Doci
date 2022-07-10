using DocumentAccessing.Storing;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Mame.Doci.Logic.DocumentAccessing.Storing
{
    interface IStoringController
    {

        void Store (FileInfo storeFile, IDocumentStoring documentStorer);
        void Store (List<FileInfo> storeFiles, IDocumentStoring documentStorer);


    }
}
