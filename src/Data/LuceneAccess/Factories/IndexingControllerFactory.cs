using DocumentAccessing.Storing;
using Mame.Doci.CrossCutting.Logging.Loggers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Mame.Doci.Data.LuceneAccess.Factories
{
    public class LuceneIndexingControllerFactory
    {
        public static IDocumentStoring CreateDefault (ILogger logger = null)
        {
            DirectoryInfo targetIndexFolder = CreateCleanAndWriteableFolder ();
            return new Indexing.IndexingController (indexFolder: targetIndexFolder, overwriteExistingIndex: true) { Logger=logger};
        }



        #region "PRIVATES"
        private static DirectoryInfo CreateCleanAndWriteableFolder ()
        {
            string TargetFoldername = Environment.GetFolderPath (Environment.SpecialFolder.MyDocuments) + "\\IndexingControllerTestsLuceneIndex\\";
            DirectoryInfo TargetDir = new DirectoryInfo (TargetFoldername);
            if (TargetDir.Exists)
            {
                TargetDir.Delete (recursive: true);
                TargetDir.Refresh ();
            }
            TargetDir.Create ();
            TargetDir.Refresh ();
            return TargetDir;
        }


        #endregion

    }
}
