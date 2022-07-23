using DocumentAccessing.Storing;
using Mame.Doci.CrossCutting.Logging.Contracts;
using Mame.Doci.UI.ConsoleClient;

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
