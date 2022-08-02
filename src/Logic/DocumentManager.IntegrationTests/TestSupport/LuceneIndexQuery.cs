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

namespace DocumentAccessing.IntegrationTests.TestSupport
{
    static class  LuceneIndexQuery
    {

        private const string INDEX_FIELDNAME_FILENAME = "Filename";


        
        public static bool IsLuceneIndexExisting (DirectoryInfo indexPath)
        {
            SimpleFSDirectory targetFolder = new SimpleFSDirectory (indexPath);
            return IndexReader.IndexExists(targetFolder);
        }

        internal static bool IsDocumentFilenameInIndexExisting (DirectoryInfo targetIndexFolder, Mame.Doci.CrossCutting.DataClasses.Document document)
        {
            bool isDocInIndexExisting = false;
            // Query The Docuemnt
            Document searchDoc = GetDocumentFromIndex (targetIndexFolder, document);
            // Check queryResults
            if (searchDoc is null)
            {
                isDocInIndexExisting = false;
            } else
            {
                isDocInIndexExisting = true;
            }
            // return
            return isDocInIndexExisting;
        }

        internal static bool AreDocumentsFilenameInIndexExisting (DirectoryInfo targetIndexFolder, Mame.Doci.CrossCutting.DataClasses.Document[] documents)
        {
            bool areDocsExisting = false;
            // Query The Docuemnt
            List<Document> searchDocs = GetDocumentsFromIndex (targetIndexFolder, documents);
            // Check queryResults
            if (searchDocs.Count == 0)                          {areDocsExisting = false;
            } else if(searchDocs.Count == documents.Length)   {areDocsExisting = true;}
            // return
            return areDocsExisting;
        }



        internal static bool AreAllImportFileFieldsExistingInIndex(DirectoryInfo targetIndexFolder, Mame.Doci.CrossCutting.DataClasses.Document document, List<string> checkFieldnames)
        {
            bool AllFieldsExisting = false;
            // Query the index for the target file to check
            Document searchDoc = GetDocumentFromIndex (targetIndexFolder, document);
            // check if queried file has all expected indexFields in  it.
            AllFieldsExisting = HasDocAllFields (searchDoc, checkFieldnames);
            // return
            return AllFieldsExisting;
        }

        internal static bool AreAllImportFileFieldsExistingInIndex (DirectoryInfo targetIndexFolder, Mame.Doci.CrossCutting.DataClasses.Document[] documents, List<string> checkFieldnames)
        {
            // Request the index for searched filenames
            List<Document> searchDocs = GetDocumentsFromIndex (targetIndexFolder, documents);
            // if we have less results then filenames requested, somethig did went wrong
            if(documents.Count() != searchDocs.Count) { throw new FileNotFoundException ("The number of documents found are not as expected!"); }
            // Check each docuemnt, if it is containing all expected index field
            bool AllFieldsExisting = true;
            foreach(Document doc in searchDocs)
            {
                if (!HasDocAllFields (doc, checkFieldnames)) { 
                    AllFieldsExisting = false;
                    break;
                }
            }
            // return
            return AllFieldsExisting;
        }

        #region "PRIVATES"


        private static bool HasDocAllFields (Document doc, List<string> checkFieldnames)
        {
            // Check All Fields
            IList<IFieldable> theFields = doc.GetFields ();

            bool AllFieldsExisting = true;
            if (theFields.Count != checkFieldnames.Count)
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
                    if (FieldnameExisting == false)
                    {
                        AllFieldsExisting = false;
                        break;
                    }
                }
            }
            return AllFieldsExisting;
        }


        private static IndexSearcher OpenIndexSearcher (DirectoryInfo targetIndexFolder)
        {
            SimpleFSDirectory targetFolder = new SimpleFSDirectory (targetIndexFolder);
            Analyzer analyzer = new StandardAnalyzer (Lucene.Net.Util.Version.LUCENE_30);
            IndexSearcher returnSearcher =  new IndexSearcher (targetFolder, readOnly: true);
            targetFolder.Dispose ();
            return returnSearcher;
        }



        private static Document GetDocumentFromIndex (DirectoryInfo targetIndexFolder, Mame.Doci.CrossCutting.DataClasses.Document document)
        {
            Document ReturnDocument;
            // Prepare
            IndexSearcher indexSearcher = OpenIndexSearcher (targetIndexFolder);
            int maxNumberOfDocuments = 5;
            // Create the INdex Query
            Term t = new Term (INDEX_FIELDNAME_FILENAME, document.FullName);
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
            // Return
            return ReturnDocument;
        }

        private static List<Document> GetDocumentsFromIndex (DirectoryInfo targetIndexFolder, Mame.Doci.CrossCutting.DataClasses.Document[] documents)
        {
            // Functios Return List
            List<Document> returnDocs = new List<Document> ();
            // Get the indexReader to query the index
            IndexSearcher indexSearcher = OpenIndexSearcher (targetIndexFolder);
            int maxNumberOfDocuments = 5;
            // query for each filename
            foreach (Mame.Doci.CrossCutting.DataClasses.Document document in documents)
            {
                // Create the IndexQuery
                Term t = new Term (INDEX_FIELDNAME_FILENAME, document.FullName);
                Query tq = new TermQuery (t);
                // Read and collect 
                TopDocs tp = indexSearcher.Search (tq, maxNumberOfDocuments);
                if (tp.TotalHits >= 1)
                {
                    returnDocs.Add( indexSearcher.Doc (tp.ScoreDocs[0].Doc));
                }
            }
            // Close
            indexSearcher.Dispose ();
            // Retrun
            return returnDocs;
        }


  



        #endregion



    }
}
