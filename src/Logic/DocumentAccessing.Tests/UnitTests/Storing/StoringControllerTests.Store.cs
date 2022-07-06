using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Mame.Doci.Logic.DocumentAccessing.Storing;
using NUnit.Framework;

namespace Mame.Doci.Logic.DocumentAccessing.Tests.UnitTests.Storing
{
    class StoringControllerUnitTests
    {
        [Test]
        public void Store_FileInfoIsNull_ThrowsArgumentNullException ()
        {
            //ARRANGE
            var theController = new StoringController ();
            FileInfo nullFileInfo = null;
            //ACT
            //ASSERT
            Assert.Throws<ArgumentNullException> (() =>
            {
                theController.Store (nullFileInfo);
            }, "Exception was not thrown");
        }


    }
}
