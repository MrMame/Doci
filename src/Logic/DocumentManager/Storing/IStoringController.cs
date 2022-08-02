using Mame.Doci.Logic.DocumentManager.Contracts.Interfaces;
using Mame.Doci.CrossCutting.DataClasses;
using System.Collections.Generic;
using System.IO;

namespace Mame.Doci.Logic.DocumentManager.Storing
{
    interface IStoringController
    {

        void Store (Document document, IDocumentRepository documentRepository);
        void Store (List<Document> documents, IDocumentRepository documentRepository);


    }
}
