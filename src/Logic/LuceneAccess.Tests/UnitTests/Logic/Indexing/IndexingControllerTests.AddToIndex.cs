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
            TestController.AddToIndex (CleanTargetIndexFolder, ImportFile);

            //Assert
            bool IsLuceneindexExisting = LuceneIndexQuery.IsLuceneIndexExisting (CleanTargetIndexFolder);
            Assert.IsTrue (IsLuceneindexExisting);

        }




        private DirectoryInfo CreateCleanAndWriteableFolder () {
            string TargetFoldername = Environment.GetFolderPath (Environment.SpecialFolder.MyDocuments) + "\\IndexingControllerTestsLuceneIndex\\";
            return new DirectoryInfo (TargetFoldername);
        }

        private FileInfo GetDefaultImportFile ()
        {
            return new FileInfo ("_TestAssets\\A_Docs\\Continuous-Integration-mit-Jenkins.pdf");
        }

    }
}
