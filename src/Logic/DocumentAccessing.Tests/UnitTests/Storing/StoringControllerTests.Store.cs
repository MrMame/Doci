using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using DocumentAccessing.Storing;
using Mame.Doci.Logic.DocumentAccessing.Storing;
using NUnit.Framework;

namespace Mame.Doci.Logic.DocumentAccessing.Tests.UnitTests.Storing
{
    class StoringControllerUnitTests
    {

        [Test]
        public void Store_IfFileInfosOkButIDocumentStorerIsNULL_ThrowsArgumentNullException ()
        {
            //ARRANGE
            var theController = new StoringController ();
            List<FileInfo> fileinfos = new List<FileInfo>();
            IDocumentStoring docStorer = null;

            //ACT
            //ASSERT
            Assert.Throws<ArgumentNullException> (() =>
            {
                theController.Store (fileinfos, docStorer);
            }, "Exception was not thrown");
        }

        [Test]
        public void Store_IfFileInfoOkButIDocumentStorerIsNULL_ThrowsArgumentNullException ()
        {
            //ARRANGE
            var theController = new StoringController ();
            FileInfo fileinfo = new FileInfo (@"C:\thisFileIsNotExisting.txt");
            IDocumentStoring docStorer = null;

            //ACT
            //ASSERT
            Assert.Throws<ArgumentNullException> (() =>
            {
                theController.Store (fileinfo, docStorer);
            }, "Exception was not thrown");
        }

        [Test]
        public void Store_FileInfoIsNull_ThrowsArgumentNullException ()
        {
            //ARRANGE
            var theController = new StoringController ();
            FileInfo nullFileInfo = null;
            IDocumentStoring docStorer = NSubstitute.Substitute.For<IDocumentStoring> ();
            //ACT
            //ASSERT
            Assert.Throws<ArgumentNullException> (() =>
            {
                theController.Store (nullFileInfo, docStorer);
            }, "Exception was not thrown");
        }
        [Test]
        public void Store_ListOfFileInfosIsNull_ThrowsArgumentNullException ()
        {
            //ARRANGE
            var theController = new StoringController ();
            List<FileInfo> nullFileInfos = null;
            IDocumentStoring docStorer = NSubstitute.Substitute.For<IDocumentStoring> ();
                        
            //ACT
            //ASSERT
            Assert.Throws<ArgumentNullException> (() =>
            {
                theController.Store (nullFileInfos, docStorer);
            }, "Exception was not thrown");
        }

        [Test]
        public void Store_ExisingFileInfo_CallsIDocument_WHATEVER ()
        {
            Assert.Fail ("TESt not implemented");
        }

    }
}
