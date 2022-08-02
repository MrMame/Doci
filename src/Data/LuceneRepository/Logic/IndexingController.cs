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
using Mame.Doci.Data.LuceneRepository.Data;
using Mame.Doci.CrossCutting.Logging.Contracts;
using Mame.Doci.CrossCutting.Logging.Contracts.Exceptions;
using Mame.Doci.CrossCutting.DataClasses;
using Mame.Doci.Logic.DocumentManager.Contracts.Interfaces;
using Mame.Doci.Logic.DocumentManager.Contracts.Exceptions;


namespace Mame.Doci.Data.LuceneRepository.Logic
{
    public class IndexingController : IIndexingController, IDocumentRepository,ILoggable
    {
        public string ParentTargetPathForImportedDocuments { get => throw new NotImplementedException (); set => throw new NotImplementedException (); }
        public ILogger Logger
        {
            get { return _logger; }
            set { _logger = value; }
        }

        DirectoryInfo _defaultIndexFolder;
        bool _defaultOverwriteExistingIndex;
        ILogger _logger;


        #region "PUBLICS"
        public IndexingController ()
        {
            _defaultIndexFolder = null;
            _defaultOverwriteExistingIndex = true;
        }
        public IndexingController (DirectoryInfo indexFolder,bool overwriteExistingIndex)
        {
            _defaultIndexFolder = indexFolder;
            _defaultOverwriteExistingIndex = overwriteExistingIndex;
        }
        public void AddToIndex (DirectoryInfo indexFolder,bool createOrOverwriteExistingIndex, Document document)
        {
            if (indexFolder is null) throw new ArgumentNullException ();
            if (document is null) throw new ArgumentNullException ();
            try
            {
                Document[] documents = { document };
                AddFilesToIndex (indexFolder, createOrOverwriteExistingIndex, documents);
            } catch (Exception ex)
            {
                throw new Exception ($"Error trying to add ({document.FullName}) " +
                                                  $" to lucene index at path ({_defaultIndexFolder.FullName})",
                                                  ex);
            }

        }
        public void AddToIndex (DirectoryInfo indexFolder, bool createOrOverwriteExistingIndex, Document[] documents)
        {
            if (indexFolder is null) throw new ArgumentNullException ();
            if (documents is null) throw new ArgumentNullException ();
            try
            {
                AddFilesToIndex (indexFolder, createOrOverwriteExistingIndex, documents);
            } catch (Exception ex)
            {
                throw new Exception ($"Error trying to add ({documents.Count()} files ) " +
                                                  $" to lucene index at path ({_defaultIndexFolder.FullName})",
                                                  ex);
            }
        }
        public void AddToIndex (DirectoryInfo IndexFolder, bool createOrOverwriteExistingIndex,  DirectoryInfo ImportFolder, bool ImportWithSubfolders)
        {
            if (IndexFolder is null) throw new ArgumentNullException ();
            if (ImportFolder is null) throw new ArgumentNullException ();
            throw new NotImplementedException ();
            //try
            //{

            //} catch (Exception)
            //{

            //    throw;
            //}
        }
        #endregion


        #region "INTERFACE - IDocumentRepository"
        public void WriteToRepository(Document document)
        {
            if (document is null) throw new ArgumentNullException ();
            try
            {
                AddToIndex (_defaultIndexFolder, _defaultOverwriteExistingIndex, document);
            } catch (Exception ex)
            {
                throw new DocumentStoreException ($"Error trying to store user document file ({document.FullName}) " +
                                                  $" to lucene index at path ({_defaultIndexFolder.FullName})",
                                                  ex)
                { UserDocuments = new Document[1] { document}, RepositoryDirectory = _defaultIndexFolder };
            }

        }
        public void WriteToRepository (List<Document> documents)
        {
            if (documents is null) throw new ArgumentNullException ();
            try
            {
                AddToIndex (_defaultIndexFolder, _defaultOverwriteExistingIndex, documents.ToArray());
            } catch (Exception ex)
            {
                throw new DocumentStoreException ($"Error trying to store user documents to lucene index at path.",
                                                  ex)
                { UserDocuments = documents.ToArray(), RepositoryDirectory = _defaultIndexFolder };
            }

        }
        #endregion


        #region "INTERFACE - ILogger"
        public void LogMessage (LogLevels logLevel, string message)
        {
            try
            {
                if (_logger != null) _logger.LogText (logLevel, message);
            } catch (Exception ex)
            {
                throw new LogMessageException ($"Error trying to log message ({message}) ",
                                            ex);
            }
        }
        #endregion



        #region "PRIVATES"
        private void AddFilesToIndex (DirectoryInfo indexFolder, bool createOrOverwriteExistingIndex, Document[] documents)
        {
            if (indexFolder is null) throw new ArgumentNullException ();
            if (documents is null) throw new ArgumentNullException ();

            // Index is existing but we are not allowed to overwrite it.
            ThrowExceptionIfIndexIsProtected (indexFolder, createOrOverwriteExistingIndex);
            // open the Index and get the writer Object for adding documents
            IndexWriter theWriter = OpenIndexWriter (indexFolder.FullName, createOrOverwriteExistingIndex);
            // Add Each File to Index
            foreach(Document document in documents)
            {
                if (!document.Exists())
                {
                    LogMessage (LogLevels.Warning, "(" + document.FullName + ") is not existing. Couldn't add document to index!");
                } else
                {
                    // Create a LuceneImportfile for the Lucene index from the source importfile
                    IndexImportFile theImportFile = new IndexImportFile (document,_logger);
                    // Add the document into the index
                    theWriter.AddDocument (theImportFile.LuceneDocument);
                }
            }
            // Celan up. Write the index to file and close connection
            theWriter.Optimize ();
            theWriter.Dispose ();
        }
        private void ThrowExceptionIfIndexIsProtected (DirectoryInfo IndexFolder, bool createOrOverwriteExistingIndex)
        {
            if(IndexFolder is null) throw new ArgumentNullException (); 

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
        private static bool IsLuceneIndexExisting (DirectoryInfo indexPath)
        {
            if (indexPath is null) throw new ArgumentNullException ();

            SimpleFSDirectory targetFolder = new SimpleFSDirectory (indexPath);
            return IndexReader.IndexExists (targetFolder);
        }
        #endregion

    }
}
