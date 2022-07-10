using DocumentAccessing.IntegrationTests.TestSupport;
using DocumentAccessing.Storing;
using Mame.Doci.Data.LuceneAccess.Indexing;
using Mame.Doci.Logic.DocumentAccessing.Storing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;

namespace DocumentAccessing.IntegrationTests.Storing
{
    [TestClass]
    public partial class StoringControllerIntegTests
    {

        


        [TestMethod]
        public void Store_ExistingFileAndAccessibleLuceneindex_StoresDokumentWithAllFields ()
        {
            //Arrange
            List<string> checkFieldnames = new List<string> () { "Title", "Filename","Path",
                                                                "ContentCompressed","Type",
                                                                "FileSize","Last Modified"};
            DirectoryInfo cleanTargetIndexFolder = CreateCleanAndWriteableFolder ();
            FileInfo importFile = GetDefaultImportFile ();

            IDocumentStoring theLuceneDocmentStorer = new IndexingController (cleanTargetIndexFolder,overwriteExistingIndex:true);


            //Act
            var theStoringController = new StoringController();
            theStoringController.Store (importFile, theLuceneDocmentStorer);
            
            //Assert
            bool IsLuceneindexExisting = LuceneIndexQuery.IsLuceneIndexExisting (cleanTargetIndexFolder);
            bool IsImporttFileInIndex = LuceneIndexQuery.IsDocumentFilenameInIndexExisting (cleanTargetIndexFolder, importFile);
            bool AreAllFieldsInIndexExsiting = LuceneIndexQuery.AreAllImportFileFieldsExistingInIndex (cleanTargetIndexFolder, importFile, checkFieldnames);
            Assert.IsTrue (IsLuceneindexExisting, "There is no Lucene Index existing inside targetindexFolder! " + cleanTargetIndexFolder);
            Assert.IsTrue (IsImporttFileInIndex, "No Document match found inside lucene index for test importfile! " + importFile.FullName);
            Assert.IsTrue (AreAllFieldsInIndexExsiting, "The index fields of the document are not matching expected fieldnames.");


            // CleanUp
            cleanTargetIndexFolder.Delete (true);
        }
        [TestMethod]
        public void Store_ExistingFilesAndAccessibleLuceneindex_StoresDocumentsWithAllFields ()
        {
            //Arrange
            List<string> checkFieldnames = new List<string> () { "Title", "Filename","Path",
                                                                "ContentCompressed","Type",
                                                                "FileSize","Last Modified"};
            DirectoryInfo cleanTargetIndexFolder = CreateCleanAndWriteableFolder ();
            List<FileInfo> importFiles = GetDefaultImportFiles ();

            IDocumentStoring theLuceneDocmentStorer = new IndexingController (cleanTargetIndexFolder, overwriteExistingIndex: true);


            //Act
            var theStoringController = new StoringController ();
            theStoringController.Store (importFiles, theLuceneDocmentStorer);

            //Assert
            bool IsLuceneindexExisting = LuceneIndexQuery.IsLuceneIndexExisting (cleanTargetIndexFolder);
            bool IsImporttFileInIndex = LuceneIndexQuery.AreDocumentsFilenameInIndexExisting (cleanTargetIndexFolder, importFiles.ToArray());
            bool AreAllFieldsInIndexExsiting = LuceneIndexQuery.AreAllImportFileFieldsExistingInIndex (cleanTargetIndexFolder, importFiles.ToArray(), checkFieldnames);
            Assert.IsTrue (IsLuceneindexExisting, "There is no Lucene Index existing inside targetindexFolder! " + cleanTargetIndexFolder);
            Assert.IsTrue (IsImporttFileInIndex, "No Documents match found inside lucene index for test importfile! ");
            Assert.IsTrue (AreAllFieldsInIndexExsiting, "The index fields of the document are not matching expected fieldnames.");


            // CleanUp
            cleanTargetIndexFolder.Delete (true);
        }
        [TestMethod]
        public void Store_EmptyFilesAndAccessibleLuceneindex_ThrowsNoException ()
        {
            //Arrange
            List<string> checkFieldnames = new List<string> () { "Title", "Filename","Path",
                                                                "ContentCompressed","Type",
                                                                "FileSize","Last Modified"};
            DirectoryInfo cleanTargetIndexFolder = CreateCleanAndWriteableFolder ();
            List<FileInfo> emptyFilesList = new List<FileInfo> ();

            IDocumentStoring theLuceneDocmentStorer = new IndexingController (cleanTargetIndexFolder, overwriteExistingIndex: true);


            //Act
            var theStoringController = new StoringController ();
            theStoringController.Store (emptyFilesList, theLuceneDocmentStorer);

            //Assert
           
            /* If no Exception was thrown, the test was successfull*/

            // CleanUp
            cleanTargetIndexFolder.Delete (true);
        }




        #region "PRIVATE"
        private DirectoryInfo CreateCleanAndWriteableFolder ()
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

        private FileInfo GetDefaultImportFile ()
        {
            return new FileInfo ("Assets\\jenkins-user-handbook.pdf");
        }

        private List<FileInfo> GetDefaultImportFiles ()
        {
            List<FileInfo> files = new List<FileInfo> ();

            files.Add (new FileInfo ("Assets\\Aus Kroatien. Skizzen und Erzählungen15734-0.txt"));
            files.Add (new FileInfo ("Assets\\Financial Sample.xlsx"));
            files.Add (new FileInfo ("Assets\\jenkins-user-handbook.pdf"));
            files.Add (new FileInfo ("Assets\\Real-Statistics-Examples-Basics.xlsx"));

            return files;

        }




        #endregion

    }
}
