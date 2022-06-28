using Lucene.Net.Index;
using Lucene.Net.Store;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuceneAccess.Tests.TestSupport
{
    static class  LuceneIndexQuery
    {
        
        public static bool IsLuceneIndexExisting (DirectoryInfo IndexPath)
        {
            SimpleFSDirectory targetFolder = new SimpleFSDirectory (IndexPath);
            return IndexReader.IndexExists(targetFolder);
        }




    }
}
