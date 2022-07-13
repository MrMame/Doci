using DocumentAccessing.Storing;
using Mame.Doci.CrossCutting.Logging.Loggers;
using Mame.Doci.Data.LuceneAccess.Factories;
using Mame.Doci.Data.LuceneAccess.Indexing;
using Mame.Doci.UI.ConsoleClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mame.Doci.Logic.DocumentAccessing.Storing.Factories
{
    public class StoringControllerFactory
    {
        public static IStoringForUser CreateDefault (ILogger logger = null)
        {
            IDocumentStoring luceneStorer = LuceneIndexingControllerFactory.CreateDefault (logger);
            return new StoringController (luceneStorer) {Logger=logger};
        }
    }
}
