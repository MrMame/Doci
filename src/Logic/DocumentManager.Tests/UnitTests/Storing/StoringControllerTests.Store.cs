using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Mame.Doci.Logic.DocumentManager.Contracts.Interfaces;
using Mame.Doci.Logic.DocumentManager.Storing;
using Mame.Doci.CrossCutting.DataClasses;
using NUnit.Framework;
using Mame.Doci.Logic.DocumentManager.Contracts.Exceptions;

namespace Mame.Doci.Logic.DocumentAccessing.Tests.UnitTests.Storing
{
    public partial class StoringControllerUnitTests
    {

        [Test]
        public void Store_IfDocumentsOkButIRepositoryIsNULL_ThrowsArgumentNullException ()
        {
            //ARRANGE
            var theController = new StoringController ();
            List<Document> documents = new List<Document>();

            //ACT
            //ASSERT
            Assert.Throws<ArgumentNullException> (() =>
            {
                theController.Store (documents, null);
            }, "Exception was not thrown");
        }

        [Test]
        public void Store_IfDocumentOkButIRepositoryIsNULL_ThrowsArgumentNullException ()
        {
            //ARRANGE
            var theController = new StoringController ();
            Document document = new Document (new FileInfo(@"C:\thisFileIsNotExisting.txt"));



            //ACT
            //ASSERT
            Assert.Throws<ArgumentNullException> (() =>
            {
                theController.Store (document, null);
            }, "Exception was not thrown");
        }

        [Test]
        public void Store_DocumentIsNull_ThrowsArgumentNullException ()
        {
            //ARRANGE
            var theController = new StoringController ();
            Document nullDocument = null;
            IDocumentRepository repository = NSubstitute.Substitute.For<IDocumentRepository> ();
            //ACT
            //ASSERT
            Assert.Throws<ArgumentNullException> (() =>
            {
                theController.Store (nullDocument, repository);
            }, "Exception was not thrown");
        }
        [Test]
        public void Store_ListOfDocumentsIsNull_ThrowsArgumentNullException ()
        {
            //ARRANGE
            var theController = new StoringController ();
            List<Document> nullDocuments = null;
            IDocumentRepository repository = NSubstitute.Substitute.For<IDocumentRepository> ();
                        
            //ACT
            //ASSERT
            Assert.Throws<ArgumentNullException> (() =>
            {
                theController.Store (nullDocuments, repository);
            }, "Exception was not thrown");
        }

        [Test]
        public void Store_IfFileIsInvalid_ThrowsDocumentStoreException ()
        {
            //ARRANGE
            var theController = new StoringController ();
            Document document = new Document(new FileInfo($"acdb:\\doesntWork"));
            IDocumentRepository repository = NSubstitute.Substitute.For<IDocumentRepository> ();

            //ACT
            //ASSERT
            Assert.Throws<DocumentStoreException> (() =>
            {
                theController.Store (document, repository);
            }, "Exception was not thrown");
        }



    }
}
