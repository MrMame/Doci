using Mame.Doci.CrossCutting.Logging.Contracts;
using Mame.Doci.Logic.DocumentAccessing.Contracts;

namespace Mame.Doci.Logic.DocumentAccessing.Storing.Factories
{
    public class StoringControllerFactory
    {
        public static IStoringForUser CreateDefault (IDocumentStoring luceneStorer, ILogger logger = null)
        {
            //IDocumentStoring luceneStorer = LuceneIndexingControllerFactory.CreateDefault (logger);
            return new StoringController (luceneStorer) {Logger=logger};
        }
    }
}
