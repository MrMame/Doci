using Mame.Doci.CrossCutting.Logging.Contracts;
using Mame.Doci.Logic.DocumentManager.Contracts.Interfaces;
using Mame.Doci.Data.LuceneRepository.Logic;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Mame.Doci.Data.LuceneRepository.Factories
{
    public class LuceneIndexingControllerFactory
    {
        public static IDocumentRepository CreateDefault (ILogger logger = null)
        {
            DirectoryInfo targetIndexFolder = CreateCleanAndWriteableFolder ();
            return new IndexingController (indexFolder: targetIndexFolder, overwriteExistingIndex: true) { Logger=logger};
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
