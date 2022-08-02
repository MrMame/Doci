using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mame.Doci.CrossCutting.DataClasses;

namespace Mame.Doci.Logic.DocumentManager.Contracts.Exceptions
{
    public class DocumentStoreException : Exception
    {
        public DocumentStoreException () : base () { }
        public DocumentStoreException (string message) : base (message) { }
        public DocumentStoreException (string message, Exception innerException) : base (message, innerException) { }


        public Document[] UserDocuments { get; set; }
        public DirectoryInfo RepositoryDirectory{get;set;}
    }
}
