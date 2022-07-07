using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace DocumentAccessing.Storing
{
    public interface IDocumentStoring
    {
        void Store (FileInfo storeFile);
        void Store (List<FileInfo> storeFiles);
    }
}
