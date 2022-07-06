using DocumentAccessing.IntegrationTests.TestSupport;
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
        public void Store_ExistingFileToAccessibleLuceneindex_StoresDokumentWithAllFields ()
        {
            //Arrange
            List<string> checkFieldnames = new List<string> () { "Title", "Filename","Path",
                                                                "ContentCompressed","Type",
                                                                "FileSize","Last Modified"};
            DirectoryInfo CleanTargetIndexFolder = CreateCleanAndWriteableFolder ();
            FileInfo ImportFile = GetDefaultImportFile ();



            //Act
            Assert.Fail ("Test not implemented");

            //Assert
            bool IsLuceneindexExisting = LuceneIndexQuery.IsLuceneIndexExisting (CleanTargetIndexFolder);
            bool IsImporttFileInIndex = LuceneIndexQuery.IsDocumentFilenameInIndexExisting (CleanTargetIndexFolder, ImportFile);
            bool AreAllFieldsInIndexExsiting = LuceneIndexQuery.AreAllImportFileFieldsExistingInIndex (CleanTargetIndexFolder, ImportFile, checkFieldnames);
            Assert.IsTrue (IsLuceneindexExisting, "There is no Lucene Index existing inside targetindexFolder! " + CleanTargetIndexFolder);
            Assert.IsTrue (IsImporttFileInIndex, "No Document match found inside lucene index for test importfile! " + ImportFile.FullName);
            Assert.IsTrue (AreAllFieldsInIndexExsiting, "The index fields of the document are not matching expected fieldnames.");


            // CleanUp
            CleanTargetIndexFolder.Delete (true);
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

        private FileInfo[] GetDefaultImportFiles ()
        {
            List<FileInfo> files = new List<FileInfo> ();

            files.Add (new FileInfo ("Assets\\Aus Kroatien. Skizzen und Erzählungen15734-0.txt"));
            files.Add (new FileInfo ("Assets\\Financial Sample.xlsx"));
            files.Add (new FileInfo ("Assets\\jenkins-user-handbook.pdf"));
            files.Add (new FileInfo ("Assets\\Real-Statistics-Examples-Basics.xlsx"));

            return files.ToArray ();

        }




        #endregion

    }
}
