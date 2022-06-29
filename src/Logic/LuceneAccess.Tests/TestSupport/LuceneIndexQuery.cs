using Lucene.Net.Analysis;
using Lucene.Net.Analysis.Standard;
using Lucene.Net.Documents;
using Lucene.Net.Index;
using Lucene.Net.Search;
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

        private const string INDEX_FIELDNAME_FILENAME = "FileName";


        
        public static bool IsLuceneIndexExisting (DirectoryInfo indexPath)
        {
            SimpleFSDirectory targetFolder = new SimpleFSDirectory (indexPath);
            return IndexReader.IndexExists(targetFolder);
        }

        internal static bool IsDocumentInIndexExisting (DirectoryInfo targetIndexFolder, FileInfo importFile)
        {

            bool isDocInIndexExisting = false;

            // Prepare
            SimpleFSDirectory targetFolder = new SimpleFSDirectory (targetIndexFolder);
            Analyzer analyzer = new StandardAnalyzer (Lucene.Net.Util.Version.LUCENE_30);
            IndexSearcher indexSearcher = new IndexSearcher (targetFolder, readOnly:true);
            int maxAnzahlTreffer = 5;

            string searchTerm = "";
            var queryParser = new Lucene.Net.QueryParsers.QueryParser (Lucene.Net.Util.Version.LUCENE_30, searchTerm, analyzer);
            // Read
            TopDocs tp = indexSearcher.Search (queryParser.Query(INDEX_FIELDNAME_FILENAME), maxAnzahlTreffer);
            
            // Check
            if(tp.TotalHits <= 0)
            {
                isDocInIndexExisting = false;// Documenmt is not exitsing
            } else
            {
                Document HitDocument = indexSearcher.Doc (tp.ScoreDocs[0].Doc);
                string hitDocumentFilename = HitDocument.Get (INDEX_FIELDNAME_FILENAME);

                if (hitDocumentFilename == importFile.Name) {
                    isDocInIndexExisting = true;    // Index Document has the same name as previously imported
                } else
                {
                    isDocInIndexExisting = false;   // Index documen name is not the same as previosuyls imported
                }

            }

            // Close
            indexSearcher.Dispose ();
            targetFolder.Dispose ();


            return isDocInIndexExisting;



        }
    }
}
