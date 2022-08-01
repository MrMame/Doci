using Mame.Doci.Data.LuceneRepository.Indexing;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuceneAccess.Tests.TestSupport
{
    public static class IndexingControllerFactory
    {

        public static IndexingController CreateController()
        {
            return new IndexingController ();
        }


    }
}
