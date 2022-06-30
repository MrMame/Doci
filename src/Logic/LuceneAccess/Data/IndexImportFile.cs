using Lucene.Net.Documents;
using Mame.Doci.Logic.LuceneAccess.Logic.Indexing;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using TikaOnDotNet.TextExtraction;

namespace Mame.Doci.Logic.LuceneAccess.Data
{
    class IndexImportFile
    {

        Document _lucDoc;


        public Document LuceneDocument  { 
            get { return _lucDoc; }
        }


        public IndexImportFile (FileInfo FSFileToimport)
        {
            if (FSFileToimport == null) throw new ArgumentNullException ();
            if (!FSFileToimport.Exists) throw new FileNotFoundException ();

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
                        Lucene.Net.Documents.CompressionTools.CompressString (tikaRes.Text),
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
            TextExtractor tikaEx = new TextExtractor ();
            return tikaEx.Extract (fSFileToimport.FullName);
        }
    }
}
