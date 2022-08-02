using Mame.Doci.Data.LuceneRepository.Logic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Mame.Doci.CrossCutting.DataClasses;
using LuceneAccess.Tests.TestSupport;
using NSubstitute;
using Mame.Doci.CrossCutting.Logging.Contracts;

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
            Document document = GetDefaultImportFile ();

            IndexingController TestController = IndexingControllerFactory.CreateController ();

            //Act
            TestController.AddToIndex (CleanTargetIndexFolder, createOrOverwriteExistingIndex:true, document);

            //Assert
            bool IsLuceneindexExisting = LuceneIndexQuery.IsLuceneIndexExisting (CleanTargetIndexFolder);
            bool IsImporttFileInIndex = LuceneIndexQuery.IsDocumentFilenameInIndexExisting (CleanTargetIndexFolder,document);
            bool AreAllFieldsInIndexExsiting = LuceneIndexQuery.AreAllImportFileFieldsExistingInIndex (CleanTargetIndexFolder, document, checkFieldnames);
            Assert.IsTrue (IsLuceneindexExisting,"There is no Lucene Index existing inside targetindexFolder! " + CleanTargetIndexFolder);
            Assert.IsTrue (IsImporttFileInIndex,"No Document match found inside lucene index for test importfile! " + document.FullName);
            Assert.IsTrue (AreAllFieldsInIndexExsiting, "The index fields of the document are not matching expected fieldnames.");
            

            // CleanUp
            CleanTargetIndexFolder.Delete (true);

        }
        [TestMethod]
        [Description("Test is unsafe. Not tested with missing documents inside the index!")]
        public void AddToIndex_AccessibleIndexFolderAndImportFiles_AddsDocumentsToIndexWithAllFields ()
        {
            //Arrange
            List<string> checkFieldnames = new List<string> () { "Title", "Filename","Path",
                                                                "ContentCompressed","Type",
                                                                "FileSize","Last Modified"};
            DirectoryInfo CleanTargetIndexFolder = CreateCleanAndWriteableFolder ();
            Document[] documents = GetDefaultImportFiles ();
            IndexingController TestController = IndexingControllerFactory.CreateController ();

            //Act
            TestController.AddToIndex (CleanTargetIndexFolder, createOrOverwriteExistingIndex: true, documents);

            // Assert
            bool IsLuceneindexExisting = LuceneIndexQuery.IsLuceneIndexExisting (CleanTargetIndexFolder);
            bool AreImporttFilesInIndex = LuceneIndexQuery.AreDocumentsFilenameInIndexExisting (CleanTargetIndexFolder, documents);
            bool AreAllFieldsInIndexExsiting = LuceneIndexQuery.AreAllImportFileFieldsExistingInIndex (CleanTargetIndexFolder, documents, checkFieldnames);
            Assert.IsTrue (IsLuceneindexExisting, "There is no Lucene Index existing inside targetindexFolder! " + CleanTargetIndexFolder);
            Assert.IsTrue (AreImporttFilesInIndex, "There are documents missing inside the index! ");
            Assert.IsTrue (AreAllFieldsInIndexExsiting, "The index fields of the documents are not matching expected fieldnames.");

            // CleanUp
            CleanTargetIndexFolder.Delete (true);
        }


        [TestMethod]
        public void AddToIndex_AccessibleIndexFolderAndEMPTYImportFile_LogsMessage ()
        {
            //Arrange
            /* We only want to create an empty textfile*/
            string TargetFileFolder = Environment.GetFolderPath (Environment.SpecialFolder.MyDocuments) + "\\";
            string TargetFileName = "EmptyTestFileToAddToindex.thetestTXT";
            string targetFileFullname = TargetFileFolder + TargetFileName;
            FileInfo importFile = new FileInfo (targetFileFullname);
            if (importFile.Exists) importFile.Delete ();
            importFile.Refresh ();
            importFile.CreateText ().Close();
            importFile.Refresh ();
            /* Now Create the Docuemtn Object from the Empty file */
            Document document = new Document (importFile);

            /* IndexDir*/
            DirectoryInfo CleanTargetIndexFolder = CreateCleanAndWriteableFolder ();
            /* Logger to check if LogText was called*/                        
            var logger = Substitute.For<ILogger> ();

            IndexingController TestController = IndexingControllerFactory.CreateController ();
            TestController.Logger = logger;

            //Act
            TestController.AddToIndex (CleanTargetIndexFolder, createOrOverwriteExistingIndex: true, document);

            //ASSERT
            logger.Received ().LogText (LogLevels.Warning, "(" + importFile.FullName + ") has no content.");

            // Clean
            importFile.Delete ();

        }

        [TestMethod]
        [Ignore]
        public void AddToIndex_IfIndexFolderIsNotAccessible_ThrowsException ()
        {
            Assert.Fail ("Test not implemented");
        }
        [TestMethod]
        [Ignore]
        public void AddToIndex_IfImportFileIsNotAccessible_LogsMessage ()
        {
            Assert.Fail ("Test not implemented");
        }
        [TestMethod]
        public void AddToIndex_IfSingleFileInImportFilesIsNotAccessible_LogsMessageAndProcessesRestOfFiles ()
        {
            //Arrange
            DirectoryInfo CleanTargetIndexFolder = CreateCleanAndWriteableFolder ();
            Document document = new Document(new FileInfo (@"c:\FileisNotExisting.neverever"));
            /* If file is exsiting, delete it. Fileinfo needs to get refreshed after any changes */
            if (document.Exists()) document.Delete ();
            //document.Refresh ();

            var logger = Substitute.For<ILogger> ();

            IndexingController TestController = IndexingControllerFactory.CreateController ();
            TestController.Logger = logger;

            //Act
            TestController.AddToIndex (CleanTargetIndexFolder, createOrOverwriteExistingIndex: true, document);

            //ASSERT
            logger.Received ().LogText (LogLevels.Warning, "(" + document.FullName + ") is not existing. Couldn't add document to index!");



        }

        [TestMethod]
        [Ignore]
        public void AddToIndex_IfImportFileIsNULL_ThrowsNullReferenceException ()
        {
            Assert.Fail ("Test not implemented");
        }
        [TestMethod]
        [Ignore]
        public void AddToIndex_IfImportFilesIsNULL_ThrowsNullReferenceException ()
        {
            Assert.Fail ("Test not implemented");
        }
        [TestMethod]
        [Ignore]
        public void AddToIndex_IfIndexFolderIsNULL_ThrowsNullReferenceException ()
        {
            Assert.Fail ("Test not implemented");
        }
        [TestMethod]
        [Ignore]
        public void AddToIndex_IfImportFolderIsNULL_ThrowsNullReferenceException ()
        {
            Assert.Fail ("Test not implemented");
        }
        [TestMethod]
        [Ignore]
        public void AddToIndex_IfIndexIsExistingAndOverwriteIsTRUE_ ()
        {
            Assert.Fail ("Test not implemented");
        }
        [TestMethod]
        [Ignore]
        public void AddToIndex_IfIndexIsExistingAndOverwriteIsFALSE_ ()
        {
            Assert.Fail ("Test not implemented");
        }

        [TestMethod]
        [Ignore]
        public void AddToIndex_IfIndexIsNotExistingAndOverwriteIsFALSE_AddsDocument ()
        {
            Assert.Fail ("Test not implemented");
        }
        [TestMethod]
        [Ignore]
        public void AddToIndex_IfIndexIsNotExistingAndOverwriteIsTRUE_AddsDocument ()
        {
            Assert.Fail ("Test not implemented");
        }

        [TestMethod]
        [Ignore]
        public void AddToIndex_IfParentPathIsEmpty_AddsDocumentWithRelativePath ()
        {
            Assert.Fail ("Test not implemented");
        }
        [TestMethod]
        [Ignore]
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

        private Document GetDefaultImportFile ()
        {
            return new Document(new FileInfo ("Assets\\jenkins-user-handbook.pdf"));
        }

        private Document[] GetDefaultImportFiles ()
        {
            List<Document> files = new List<Document> ();

            files.Add (new Document(new FileInfo ("Assets\\Aus Kroatien. Skizzen und Erzählungen15734-0.txt")));
            files.Add (new Document(new FileInfo ("Assets\\Financial Sample.xlsx")));
            files.Add (new Document(new FileInfo ("Assets\\jenkins-user-handbook.pdf")));
            files.Add (new Document(new FileInfo ("Assets\\Real-Statistics-Examples-Basics.xlsx")));
            return files.ToArray();

        }



        #endregion

    }
}
