using Lucene.Net.Documents;
using Mame.Doci.CrossCutting.Logging.Contracts;
using Mame.Doci.CrossCutting.Logging.Contracts.Exceptions;
using Mame.Doci.Data.LuceneRepository.Logic;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using TikaOnDotNet.TextExtraction;

namespace Mame.Doci.Data.LuceneRepository.Data
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

        public IndexImportFile (Mame.Doci.CrossCutting.DataClasses.Document document,ILogger logger=null)
        {
            if (document == null) throw new ArgumentNullException ();
            if (!document.Exists ()) throw new FileNotFoundException ();

            _logger = logger;

            _lucDoc = ParseLuceneDocument (document);

        }




        private Document ParseLuceneDocument (Mame.Doci.CrossCutting.DataClasses.Document document)
        {
            // Get text content from the Source importfile
            TextExtractionResult tikaRes = ParseImportFileText (document);

            // Create Lucene importfile
            Document luceneDocument = new Document ();
            luceneDocument.Add (new Field ("Title",
                                document.Title.ToString (),
                                Field.Store.YES,
                                Field.Index.ANALYZED));
            luceneDocument.Add (new Field ("Filename",
                        document.FullName.ToString (),
                        Field.Store.YES,
                        Field.Index.NOT_ANALYZED));
            luceneDocument.Add (new Field ("Path",
                        document.Path,
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
                        document.FileExtension.ToString (),
                        Field.Store.YES,
                        Field.Index.ANALYZED));
            luceneDocument.Add (new Field ("FileSize",
                        document.FileSize.ToString (),
                        Field.Store.YES,
                        Field.Index.NOT_ANALYZED));
            luceneDocument.Add (new Field ("Last Modified",
                        document.LastModified.ToString (),
                        Field.Store.YES,
                        Field.Index.ANALYZED));

            return luceneDocument;

        }

        private TextExtractionResult ParseImportFileText (Mame.Doci.CrossCutting.DataClasses.Document document)
        {
            TextExtractionResult retResult;

            try
            {
                TextExtractor tikaEx = new TextExtractor ();
                retResult = tikaEx.Extract (document.FullName);
            } catch (Exception)
            {
                /* If an empty file gets parsed, tika throws an exception. We don't want any problems. only inform someone*/
                LogMessage (LogLevels.Warning, "(" + document.FullName + ") has no content.");
                retResult = new TextExtractionResult ();
                retResult.Text = "";
            }
            return retResult;
        }

        public void LogMessage (LogLevels LogLevel, string Message)
        {
            try
            {
                if (_logger != null) _logger.LogText (LogLevel, Message);
            } catch (Exception ex)
            {
                throw new LogMessageException ($"Error trying to log message ({Message}) ",
                                            ex);
            }
        }
    }
}
