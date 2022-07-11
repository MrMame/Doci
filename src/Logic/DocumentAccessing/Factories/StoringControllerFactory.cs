using DocumentAccessing.Storing;
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
        public static IStoringForUser CreateDefault ()
        {
            IDocumentStoring luceneStorer = LuceneIndexingControllerFactory.CreateDefault ();
            return new StoringController (luceneStorer);
        }
    }
}
