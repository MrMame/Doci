using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Mame.Doci.Logic.DocumentManager.Contracts.Interfaces;
using Mame.Doci.Logic.DocumentManager.Storing;
using NUnit.Framework;

namespace Mame.Doci.Logic.DocumentAccessing.Tests.UnitTests.Storing
{
    public partial class StoringControllerUnitTests
    {
        [Test]
        public void UserWantsToStore_IfFileInfosOkButIDocumentStorerIsNULL_ThrowsArgumentNullException ()
        {
            //ARRANGE
            List<FileInfo> fileinfos = new List<FileInfo> ();
            IDocumentRepository docStorer = null;
            var theController = new StoringController (docStorer);
                       

            //ACT
            //ASSERT
            Assert.Throws<ArgumentNullException> (() =>
            {
                theController.StoreDocuments (fileinfos);
            }, "Exception was not thrown");
        }
        [Test]
        public void UserWantsToStore_IfFileInfoOkButIDocumentStorerIsNULL_ThrowsArgumentNullException ()
        {
            //ARRANGE
            FileInfo fileinfo = new FileInfo (@"C:\thisFileIsNotExisting.txt");
            IDocumentRepository docStorer = null;
            var theController = new StoringController (docStorer);

            //ACT
            //ASSERT
            Assert.Throws<ArgumentNullException> (() =>
            {
                theController.StoreDocument (fileinfo);
            }, "Exception was not thrown");
        }
        [Test]
        public void UserWantsToStore_FileInfoIsNull_ThrowsArgumentNullException ()
        {
            //ARRANGE
            FileInfo nullFileInfo = null;
            IDocumentRepository docStorer = NSubstitute.Substitute.For<IDocumentRepository> ();
            var theController = new StoringController (docStorer);
            //ACT
            //ASSERT
            Assert.Throws<ArgumentNullException> (() =>
            {
                theController.StoreDocument (nullFileInfo);
            }, "Exception was not thrown");
        }
        [Test]
        public void UserWantsToStore_ListOfFileInfosIsNull_ThrowsArgumentNullException ()
        {
            //ARRANGE
            List<FileInfo> nullFileInfos = null;
            IDocumentRepository docStorer = NSubstitute.Substitute.For<IDocumentRepository> ();
            var theController = new StoringController (docStorer);

            //ACT
            //ASSERT
            Assert.Throws<ArgumentNullException> (() =>
            {
                theController.StoreDocuments (nullFileInfos);
            }, "Exception was not thrown");
        }
    }
}

