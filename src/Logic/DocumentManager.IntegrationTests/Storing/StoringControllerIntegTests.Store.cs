using DocumentAccessing.IntegrationTests.TestSupport;
using Mame.Doci.Data.LuceneRepository.Logic;
using Mame.Doci.Logic.DocumentManager.Contracts.Interfaces;
using Mame.Doci.Logic.DocumentManager.Storing;
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
            Mame.Doci.CrossCutting.DataClasses.Document document = GetDefaultImportFile ();

            IDocumentRepository theLuceneDocmentStorer = new IndexingController (cleanTargetIndexFolder,overwriteExistingIndex:true);


            //Act
            var theStoringController = new StoringController();
            theStoringController.Store (document, theLuceneDocmentStorer);
            
            //Assert
            bool IsLuceneindexExisting = LuceneIndexQuery.IsLuceneIndexExisting (cleanTargetIndexFolder);
            bool IsImporttFileInIndex = LuceneIndexQuery.IsDocumentFilenameInIndexExisting (cleanTargetIndexFolder, document);
            bool AreAllFieldsInIndexExsiting = LuceneIndexQuery.AreAllImportFileFieldsExistingInIndex (cleanTargetIndexFolder, document, checkFieldnames);
            Assert.IsTrue (IsLuceneindexExisting, "There is no Lucene Index existing inside targetindexFolder! " + cleanTargetIndexFolder);
            Assert.IsTrue (IsImporttFileInIndex, "No Document match found inside lucene index for test importfile! " + document.FullName);
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
            List<Mame.Doci.CrossCutting.DataClasses.Document> documents = GetDefaultImportFiles ();

            IDocumentRepository theLuceneDocmentStorer = new IndexingController (cleanTargetIndexFolder, overwriteExistingIndex: true);


            //Act
            var theStoringController = new StoringController ();
            theStoringController.Store (documents, theLuceneDocmentStorer);

            //Assert
            bool IsLuceneindexExisting = LuceneIndexQuery.IsLuceneIndexExisting (cleanTargetIndexFolder);
            bool IsImporttFileInIndex = LuceneIndexQuery.AreDocumentsFilenameInIndexExisting (cleanTargetIndexFolder, documents.ToArray());
            bool AreAllFieldsInIndexExsiting = LuceneIndexQuery.AreAllImportFileFieldsExistingInIndex (cleanTargetIndexFolder, documents.ToArray(), checkFieldnames);
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
            List<Mame.Doci.CrossCutting.DataClasses.Document> emptyFilesList = new List<Mame.Doci.CrossCutting.DataClasses.Document> ();

            IDocumentRepository theLuceneDocmentStorer = new IndexingController (cleanTargetIndexFolder, overwriteExistingIndex: true);


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

        private Mame.Doci.CrossCutting.DataClasses.Document GetDefaultImportFile ()
        {
            return new Mame.Doci.CrossCutting.DataClasses.Document (new FileInfo ("Assets\\jenkins-user-handbook.pdf"));
        }

        private List<Mame.Doci.CrossCutting.DataClasses.Document> GetDefaultImportFiles ()
        {
            List< Mame.Doci.CrossCutting.DataClasses.Document> files = new List<Mame.Doci.CrossCutting.DataClasses.Document> ();

            files.Add (new Mame.Doci.CrossCutting.DataClasses.Document(new FileInfo ("Assets\\Aus Kroatien. Skizzen und Erzählungen15734-0.txt")));
            files.Add (new Mame.Doci.CrossCutting.DataClasses.Document(new FileInfo ("Assets\\Financial Sample.xlsx")));
            files.Add (new Mame.Doci.CrossCutting.DataClasses.Document(new FileInfo ("Assets\\jenkins-user-handbook.pdf")));
            files.Add (new Mame.Doci.CrossCutting.DataClasses.Document(new FileInfo ("Assets\\Real-Statistics-Examples-Basics.xlsx")));

            return files;

        }




        #endregion

    }
}
