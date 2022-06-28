using Mame.Doci.Logic.LuceneAccess.Logic.Indexing;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuceneAccess.Tests.UnitTests.Logic.Indexing.TestSupport
{
    public static class IndexingControllerFactory
    {

        public static IndexingController CreateController()
        {
            return new IndexingController ();
        }


    }
}
