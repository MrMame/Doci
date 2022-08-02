using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Mame.Doci.CrossCutting.DataClasses;

namespace Mame.Doci.Logic.DocumentManager.Contracts.Interfaces
{


    public interface IDocumentRepository
    {
        void WriteToRepository (Document document);        // throws DocumentStoreException
        void WriteToRepository (List<Document> documents);// throws DocumentStoreException
    }
}
