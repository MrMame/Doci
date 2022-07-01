using Lucene.Net.Analysis;
using Lucene.Net.Analysis.Standard;
using Lucene.Net.Documents;
using Lucene.Net.Index;
using Lucene.Net.QueryParsers;
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

        private const string INDEX_FIELDNAME_FILENAME = "Filename";


        
        public static bool IsLuceneIndexExisting (DirectoryInfo indexPath)
        {
            SimpleFSDirectory targetFolder = new SimpleFSDirectory (indexPath);
            return IndexReader.IndexExists(targetFolder);
        }

        internal static bool IsDocumentFilenameInIndexExisting (DirectoryInfo targetIndexFolder, FileInfo importFile)
        {

            bool isDocInIndexExisting = false;
            Document searchDoc = GetDocumentFromIndex (targetIndexFolder, importFile);

            if (searchDoc is null)
            {
                isDocInIndexExisting = false;
            } else
            {
                isDocInIndexExisting = true;
            }

            return isDocInIndexExisting;
        }

        internal static bool AreAllImportFileFieldsExistingInIndex(DirectoryInfo targetIndexFolder, FileInfo importFile, List<string> checkFieldnames)
        {
            bool AllFieldsExisting = false;
            

            Document searchDoc = GetDocumentFromIndex (targetIndexFolder, importFile);

            if (searchDoc is null)
            {
                AllFieldsExisting = false;
            } else
            {
                // Check All Fields
                IList<IFieldable> theFields = searchDoc.GetFields ();

                AllFieldsExisting = true;
                if(theFields.Count != checkFieldnames.Count)
                {
                    // If checked fields number are not the same, we can be sure that this will not match at all
                    AllFieldsExisting = false;
                } else
                {
                    /* Check All Fields. As soon as a docuemnt field is not inside the checkedFieldnames,
                        the docuemnts fields are not matching anymore.*/
                    bool FieldnameExisting = false;
                    foreach (string check in checkFieldnames)
                    {
                        FieldnameExisting = false;
                        foreach (IFieldable it in theFields)
                        {
                            if (check.Equals (it.Name)) FieldnameExisting = true;
                        }
                        if (FieldnameExisting == false) { AllFieldsExisting = false; break; }
                    }
                }
            }
            return AllFieldsExisting;
        }



        #region "PRIVATES"

        private static Document GetDocumentFromIndex (DirectoryInfo targetIndexFolder, FileInfo importFile)
        {

            Document ReturnDocument;

            // Prepare
            SimpleFSDirectory targetFolder = new SimpleFSDirectory (targetIndexFolder);
            Analyzer analyzer = new StandardAnalyzer (Lucene.Net.Util.Version.LUCENE_30);
            IndexSearcher indexSearcher = new IndexSearcher (targetFolder, readOnly: true);
            int maxNumberOfDocuments = 5;

            Term t = new Term (INDEX_FIELDNAME_FILENAME, importFile.FullName);
            Query tq = new TermQuery (t);

            // Read
            TopDocs tp = indexSearcher.Search (tq, maxNumberOfDocuments);
            if (tp.TotalHits >= 1)
            {
                ReturnDocument = indexSearcher.Doc (tp.ScoreDocs[0].Doc);
            } else
            {
                ReturnDocument = null;
            }
            
            // Close
            indexSearcher.Dispose ();
            targetFolder.Dispose ();

            return ReturnDocument;

        }


        #endregion



    }
}
