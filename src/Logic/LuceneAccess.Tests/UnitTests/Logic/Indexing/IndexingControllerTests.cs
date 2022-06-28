using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace LuceneAccess.Tests.UnitTests.Logic.Indexing
{
    /// <summary>
    /// Zusammenfassungsbeschreibung für UnitTest1
    /// </summary>
    [TestClass]
    public partial class IndexingControllerTests
    {
        public IndexingControllerTests ()
        {
            //
            // TODO: Konstruktorlogik hier hinzufügen
            //
        }

        private TestContext testContextInstance;

        /// <summary>
        ///Ruft den Textkontext mit Informationen über
        ///den aktuellen Testlauf sowie Funktionalität für diesen auf oder legt diese fest.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Zusätzliche Testattribute
        //
        // Sie können beim Schreiben der Tests folgende zusätzliche Attribute verwenden:
        //
        // Verwenden Sie ClassInitialize, um vor Ausführung des ersten Tests in der Klasse Code auszuführen.
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Verwenden Sie ClassCleanup, um nach Ausführung aller Tests in einer Klasse Code auszuführen.
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Mit TestInitialize können Sie vor jedem einzelnen Test Code ausführen. 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Mit TestCleanup können Sie nach jedem Test Code ausführen.
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

 
    }
}
