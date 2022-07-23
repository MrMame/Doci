using Lucene.Net.Documents;
using Mame.Doci.CrossCutting.Logging.Contracts;
using Mame.Doci.Data.LuceneAccess.Indexing;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using TikaOnDotNet.TextExtraction;

namespace Mame.Doci.Data.LuceneAccess.Indexing
{
    class IndexImportFile:ILoggable
    {

        Document _lucDoc;
        ILogger _logger;

        public Document LuceneDocument  { 
            get { return _lucDoc; }
        }

        public ILogger Logger
        {
            get { return _logger; }
            set { _logger = value; }
        }

        public IndexImportFile (FileInfo FSFileToimport,ILogger logger=null)
        {
            if (FSFileToimport == null) throw new ArgumentNullException ();
            if (!FSFileToimport.Exists) throw new FileNotFoundException ();

            _logger = logger;

            _lucDoc = ParseLuceneDocument (FSFileToimport);

        }




        private Document ParseLuceneDocument (FileInfo fSFileToimport)
        {
            // Get text content from the Source importfile
            TextExtractionResult tikaRes = ParseImportFileText (fSFileToimport);

            // Create Lucene importfile
            Document luceneDocument = new Document ();
            luceneDocument.Add (new Field ("Title",
                                fSFileToimport.Name.ToString (),
                                Field.Store.YES,
                                Field.Index.ANALYZED));
            luceneDocument.Add (new Field ("Filename",
                        fSFileToimport.FullName.ToString (),
                        Field.Store.YES,
                        Field.Index.NOT_ANALYZED));
            luceneDocument.Add (new Field ("Path",
                        fSFileToimport.DirectoryName,
                        Field.Store.YES,
                        Field.Index.NOT_ANALYZED));

            /*
             The "Content" Field will only be used to analyze zthe text of the document for the index. It will not save the whole text inside.
            The whole text will be stored inside the compressed field "ContentCompressed".
             */
            luceneDocument.Add (new Field ("Content",
                        tikaRes.Text,
                        Field.Store.NO,
                        Field.Index.ANALYZED,
                        Field.TermVector.WITH_POSITIONS_OFFSETS));
            /*This field is savinf the compressed content text of the source file to import. It can be used to display
             the text inside a previewbox.*/
            luceneDocument.Add (new Field ("ContentCompressed",
                        CompressionTools.CompressString (tikaRes.Text),
                        Field.Store.YES));
            luceneDocument.Add (new Field ("Type",
                        fSFileToimport.Extension.ToString (),
                        Field.Store.YES,
                        Field.Index.ANALYZED));
            luceneDocument.Add (new Field ("FileSize",
                        fSFileToimport.Length.ToString (),
                        Field.Store.YES,
                        Field.Index.NOT_ANALYZED));
            luceneDocument.Add (new Field ("Last Modified",
                        fSFileToimport.LastWriteTime.ToString (),
                        Field.Store.YES,
                        Field.Index.ANALYZED));

            return luceneDocument;

        }

        private TextExtractionResult ParseImportFileText (FileInfo fSFileToimport)
        {
            TextExtractionResult retResult;

            try
            {
                TextExtractor tikaEx = new TextExtractor ();
                retResult = tikaEx.Extract (fSFileToimport.FullName);
            } catch (Exception)
            {
                /* If an empty file gets parsed, tika throws an exception. We don't want any problems. only inform someone*/
                LogMessage (LogLevels.Warning, "(" + fSFileToimport.FullName + ") has no content.");
                retResult = new TextExtractionResult ();
                retResult.Text = "";
            }
            return retResult;
        }

        public void LogMessage (LogLevels LogLevel, string Message)
        {
            if (_logger != null) _logger.LogText (LogLevel, Message);
        }
    }
}
