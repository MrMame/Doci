using Lucene.Net.Analysis;
using Lucene.Net.Analysis.Standard;
using Lucene.Net.Util;
using Lucene.Net.Index;
using Lucene.Net.Store;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using DocumentAccessing.Storing;

namespace Mame.Doci.Data.LuceneAccess.Indexing
{
    public class IndexingController : IIndexingController, IDocumentStoring
    {
        public string ParentTargetPathForImportedDocuments { get => throw new NotImplementedException (); set => throw new NotImplementedException (); }


        DirectoryInfo _defaultIndexFolder;
        bool _defaultOverwriteExistingIndex;


        #region "PUBLICS"

        public IndexingController (DirectoryInfo indexFolder,bool overwriteExistingIndex)
        {
            _defaultIndexFolder = indexFolder;
            _defaultOverwriteExistingIndex = overwriteExistingIndex;
        }


        #region "INTERFACE IDocumetStoring"
        public void Store(FileInfo storefile)
        {
            AddToIndex (_defaultIndexFolder, _defaultOverwriteExistingIndex, storefile);
        }

        #endregion

        public void AddToIndex (DirectoryInfo indexFolder,bool createOrOverwriteExistingIndex, FileInfo importFile)
        {
            FileInfo[] files = { importFile };
            AddToIndex (indexFolder, createOrOverwriteExistingIndex, files);
        }

        public void AddToIndex (DirectoryInfo indexFolder, bool createOrOverwriteExistingIndex, FileInfo[] importFiles)
        {
            // Index is existing but we are not allowed to overwrite it.
            ThrowExceptionIfIndexIsProtected (indexFolder, createOrOverwriteExistingIndex);
            // open the Index and get the writer Object for adding documents
            IndexWriter theWriter = OpenIndexWriter (indexFolder.FullName, createOrOverwriteExistingIndex);
            // Add Each File to Index
            foreach(FileInfo file in importFiles)
            {
                // Create a LuceneImportfile for the Lucene index from the source importfile
                IndexImportFile theImportFile = new IndexImportFile (file);
                // Add the document into the index
                theWriter.AddDocument (theImportFile.LuceneDocument);
            }
            // Celan up. Write the index to file and close connection
            theWriter.Optimize ();
            theWriter.Dispose ();
        }

        public void AddToIndex (DirectoryInfo IndexFolder, bool createOrOverwriteExistingIndex,  DirectoryInfo ImportFolder, bool ImportWithSubfolders)
        {
            throw new NotImplementedException ();
        }

        #endregion


        #region "PRIVATES"


        private void ThrowExceptionIfIndexIsProtected (DirectoryInfo IndexFolder, bool createOrOverwriteExistingIndex)
        {
            bool indexExistingAtTarget = IsLuceneIndexExisting (IndexFolder);
            if (!createOrOverwriteExistingIndex && indexExistingAtTarget)
            {
                throw new AccessViolationException ("There is a lucene index already existing, but it's not allowed to overwrite this one!");
            }
        }


        private IndexWriter OpenIndexWriter (string indexFolder,bool createOrOverwriteExistingIndex)
        {
            // open index
            FSDirectory dir = FSDirectory.Open (indexFolder);
            Analyzer theAnalyzer = new StandardAnalyzer (Lucene.Net.Util.Version.LUCENE_30);
            // getwriter
            return new IndexWriter (dir, theAnalyzer, create: createOrOverwriteExistingIndex, IndexWriter.MaxFieldLength.UNLIMITED);
        }

        public static bool IsLuceneIndexExisting (DirectoryInfo indexPath)
        {
            SimpleFSDirectory targetFolder = new SimpleFSDirectory (indexPath);
            return IndexReader.IndexExists (targetFolder);
        }


        #endregion

    }
}
