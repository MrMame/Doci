using Mame.Doci.CrossCutting.Logging.Contracts;
using Mame.Doci.Logic.DocumentManager.Contracts.Interfaces;

namespace Mame.Doci.Logic.DocumentManager.Storing.Factories
{
    public class StoringControllerFactory
    {
        public static IDocumentService CreateDefault (IDocumentRepository luceneStorer, ILogger logger = null)
        {
            //IDocumentStoring luceneStorer = LuceneIndexingControllerFactory.CreateDefault (logger);
            return new StoringController (luceneStorer) {Logger=logger};
        }
    }
}
