using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mame.Doci.CrossCutting.DataClasses
{
    public class Document
    {

        FileInfo _fileInfo;

        public string Title { get; set; }
        public string Path { get; set; }
        public string FullFilename { get; set; }
        public string FileExtension { get; set; }
        public string FileSize { get; set; }
        public string LastModified { get; set; }

        public Document (FileInfo fileInfo)
        {
            if (fileInfo == null) throw new ArgumentNullException ();
            _fileInfo = fileInfo;

            this.Title = _fileInfo.Name;
            this.FullFilename = _fileInfo.FullName;
            this.FileExtension = _fileInfo.Extension;
            this.Path = _fileInfo.DirectoryName;
            this.FileSize = _fileInfo.Length.ToString ();
            this.LastModified = _fileInfo.LastWriteTime.ToString ();

        }
    }
}
