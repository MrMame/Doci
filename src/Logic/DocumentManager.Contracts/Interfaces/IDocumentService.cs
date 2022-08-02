using Mame.Doci.CrossCutting.DataClasses;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Mame.Doci.Logic.DocumentManager.Contracts.Interfaces
{
    public interface IDocumentService
    {
        void StoreDocument(Document document);
        void StoreDocuments (List<Document> documents);

    }
}
