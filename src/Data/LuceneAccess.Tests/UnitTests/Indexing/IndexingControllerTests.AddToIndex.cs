using Mame.Doci.Data.LuceneAccess.Indexing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

using LuceneAccess.Tests.TestSupport;

namespace LuceneAccess.Tests.UnitTests.Indexing
{
    /// <summary>
    /// Zusammenfassungsbeschreibung für UnitTest1
    /// </summary>
    public partial class IndexingControllerTests
    {

        #region "TESTS"

        [TestMethod]
        public void AddToIndex_AccessibleIndexFolderAndImportFile_AddsDocumentToIndexWithAllFields ()
        {
            //Arrange
            List<string> checkFieldnames = new List<string> () { "Title", "Filename","Path",
                                                                "ContentCompressed","Type",
                                                                "FileSize","Last Modified"};
            DirectoryInfo CleanTargetIndexFolder = CreateCleanAndWriteableFolder ();
            FileInfo ImportFile = GetDefaultImportFile ();

            IndexingController TestController = IndexingControllerFactory.CreateController ();

            //Act
            TestController.AddToIndex (CleanTargetIndexFolder, createOrOverwriteExistingIndex:true, ImportFile);

            //Assert
            bool IsLuceneindexExisting = LuceneIndexQuery.IsLuceneIndexExisting (CleanTargetIndexFolder);
            bool IsImporttFileInIndex = LuceneIndexQuery.IsDocumentFilenameInIndexExisting (CleanTargetIndexFolder,ImportFile);
            bool AreAllFieldsInIndexExsiting = LuceneIndexQuery.AreAllImportFileFieldsExistingInIndex (CleanTargetIndexFolder, ImportFile, checkFieldnames);
            Assert.IsTrue (IsLuceneindexExisting,"There is no Lucene Index existing inside targetindexFolder! " + CleanTargetIndexFolder);
            Assert.IsTrue (IsImporttFileInIndex,"No Document match found inside lucene index for test importfile! " + ImportFile.FullName);
            Assert.IsTrue (AreAllFieldsInIndexExsiting, "The index fields of the document are not matching expected fieldnames.");
            

            // CleanUp
            CleanTargetIndexFolder.Delete (true);

        }

        [TestMethod]
        public void AddToIndex_IfIndexFolderIsNotAccessible_ThrowsException ()
        {
            Assert.Fail ("Test not implemented");
        }
        [TestMethod]
        public void AddToIndex_IfImportFileIsNotAccessible_LogsMessage ()
        {
            Assert.Fail ("Test not implemented");
        }
        [TestMethod]
        public void AddToIndex_IfSingleFileInImportFilesIsNotAccessible_LogsMessageAndProcessesRestOfFiles ()
        {
            Assert.Fail ("Test not implemented");
        }

        [TestMethod]
        public void AddToIndex_IfImportFileIsNULL_ThrowsNullReferenceException ()
        {
            Assert.Fail ("Test not implemented");
        }
        [TestMethod]
        public void AddToIndex_IfImportFilesIsNULL_ThrowsNullReferenceException ()
        {
            Assert.Fail ("Test not implemented");
        }
        [TestMethod]
        public void AddToIndex_IfIndexFolderIsNULL_ThrowsNullReferenceException ()
        {
            Assert.Fail ("Test not implemented");
        }
        [TestMethod]
        public void AddToIndex_IfImportFolderIsNULL_ThrowsNullReferenceException ()
        {
            Assert.Fail ("Test not implemented");
        }
        [TestMethod]
        public void AddToIndex_IfIndexIsExistingAndOverwriteIsTRUE_ ()
        {
            Assert.Fail ("Test not implemented");
        }
        public void AddToIndex_IfIndexIsExistingAndOverwriteIsFALSE_ ()
        {
            Assert.Fail ("Test not implemented");
        }

        [TestMethod]
        public void AddToIndex_IfIndexIsNotExistingAndOverwriteIsFALSE_AddsDocument ()
        {
            Assert.Fail ("Test not implemented");
        }
        [TestMethod]
        public void AddToIndex_IfIndexIsNotExistingAndOverwriteIsTRUE_AddsDocument ()
        {
            Assert.Fail ("Test not implemented");
        }

        [TestMethod]
        public void AddToIndex_IfParentPathIsEmpty_AddsDocumentWithRelativePath ()
        {
            Assert.Fail ("Test not implemented");
        }
        [TestMethod]
        public void AddToIndex_IfParentPathIsGiven_AddsDocumentWithParentPath ()
        {
            Assert.Fail ("Test not implemented");
        }




        #endregion



        #region "PRIVATE"

        private DirectoryInfo CreateCleanAndWriteableFolder () {
            string TargetFoldername = Environment.GetFolderPath (Environment.SpecialFolder.MyDocuments) + "\\IndexingControllerTestsLuceneIndex\\";
            DirectoryInfo TargetDir =  new DirectoryInfo (TargetFoldername);
            if (TargetDir.Exists)
            {
                TargetDir.Delete (recursive:true);
                TargetDir.Refresh ();
            }
            TargetDir.Create ();
            TargetDir.Refresh ();
            return TargetDir;
        }

        private FileInfo GetDefaultImportFile ()
        {
            return new FileInfo ("Assets\\jenkins-user-handbook.pdf");
        }

        #endregion

    }
}
