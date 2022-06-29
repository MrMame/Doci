using Mame.Doci.Logic.LuceneAccess.Logic.Indexing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

using LuceneAccess.Tests.UnitTests.Logic.Indexing.TestSupport;
using LuceneAccess.Tests.TestSupport;

namespace LuceneAccess.Tests.UnitTests.Logic.Indexing
{
    /// <summary>
    /// Zusammenfassungsbeschreibung für UnitTest1
    /// </summary>
    public partial class IndexingControllerTests
    {
     
        [TestMethod]
        public void AddToIndex_AccessibleIndexFolderAndImportFile_AddsDocumentToIndex ()
        {
            //Arrange
            DirectoryInfo CleanTargetIndexFolder = CreateCleanAndWriteableFolder ();
            FileInfo ImportFile = GetDefaultImportFile ();

            IndexingController TestController = IndexingControllerFactory.CreateController ();

            //Act
            TestController.AddToIndex (CleanTargetIndexFolder, createOrOverwriteExistingIndex:true, ImportFile);

            //Assert
            bool IsLuceneindexExisting = LuceneIndexQuery.IsLuceneIndexExisting (CleanTargetIndexFolder);
            bool IsImporttFileInIndex = LuceneIndexQuery.IsDocumentInIndexExisting (CleanTargetIndexFolder,ImportFile);
            Assert.IsTrue (IsLuceneindexExisting);
            Assert.IsTrue (IsImporttFileInIndex);

            // CleanUp
            CleanTargetIndexFolder.Delete ();

        }

      




        private DirectoryInfo CreateCleanAndWriteableFolder () {
            string TargetFoldername = Environment.GetFolderPath (Environment.SpecialFolder.MyDocuments) + "\\IndexingControllerTestsLuceneIndex\\";
            DirectoryInfo TargetDir =  new DirectoryInfo (TargetFoldername);
            if (TargetDir.Exists)
            {
                TargetDir.Delete (recursive:true);
                TargetDir.Refresh ();
            }
            TargetDir.Create ();
            return TargetDir;
        }

        private FileInfo GetDefaultImportFile ()
        {
            return new FileInfo ("Assets\\jenkins-user-handbook.pdf");
        }



    }
}
