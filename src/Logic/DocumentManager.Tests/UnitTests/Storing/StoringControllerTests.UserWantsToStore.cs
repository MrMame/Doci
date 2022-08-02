using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Mame.Doci.Logic.DocumentManager.Contracts.Interfaces;
using Mame.Doci.Logic.DocumentManager.Storing;
using Mame.Doci.CrossCutting.DataClasses;
using NUnit.Framework;

namespace Mame.Doci.Logic.DocumentAccessing.Tests.UnitTests.Storing
{
    public partial class StoringControllerUnitTests
    {
        [Test]
        public void UserWantsToStore_IfDocumentsOkButIRepositoryIsNULL_ThrowsArgumentNullException ()
        {
            //ARRANGE
            List<Document> documents = new List<Document> ();
            IDocumentRepository nullRepository = null;
            var theController = new StoringController (nullRepository);
                       

            //ACT
            //ASSERT
            Assert.Throws<ArgumentNullException> (() =>
            {
                theController.StoreDocuments (documents);
            }, "Exception was not thrown");
        }
        [Test]
        public void UserWantsToStore_IfDocumentOkButIRepositoryIsNULL_ThrowsArgumentNullException ()
        {
            //ARRANGE
            Document document = new Document(new FileInfo (@"C:\thisFileIsNotExisting.txt"));
            IDocumentRepository nullRepository = null;
            var theController = new StoringController (nullRepository);

            //ACT
            //ASSERT
            Assert.Throws<ArgumentNullException> (() =>
            {
                theController.StoreDocument (document);
            }, "Exception was not thrown");
        }
        [Test]
        public void UserWantsToStore_DocumentIsNull_ThrowsArgumentNullException ()
        {
            //ARRANGE
            Document nullDocument = null;
            IDocumentRepository repository = NSubstitute.Substitute.For<IDocumentRepository> ();
            var theController = new StoringController (repository);
            //ACT
            //ASSERT
            Assert.Throws<ArgumentNullException> (() =>
            {
                theController.StoreDocument (nullDocument);
            }, "Exception was not thrown");
        }
        [Test]
        public void UserWantsToStore_ListOfDocumentsIsNull_ThrowsArgumentNullException ()
        {
            //ARRANGE
            List<Document> nullDocuments = null;
            IDocumentRepository repository = NSubstitute.Substitute.For<IDocumentRepository> ();
            var theController = new StoringController (repository);

            //ACT
            //ASSERT
            Assert.Throws<ArgumentNullException> (() =>
            {
                theController.StoreDocuments (nullDocuments);
            }, "Exception was not thrown");
        }
    }
}

