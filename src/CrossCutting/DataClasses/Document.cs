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
        public string FullName { get; set; }
        public string FileExtension { get; set; }
        public string FileSize
        {
            get { return _fileInfo.Length.ToString (); }
        }
        public string LastModified
        {
            get { return _fileInfo.LastWriteTime.ToString (); }
        }

        public Document (FileInfo fileInfo)
        {
            if (fileInfo == null) throw new ArgumentNullException ();
            _fileInfo = fileInfo;

            this.Title = _fileInfo.Name;
            this.FullName = _fileInfo.FullName;
            this.FileExtension = _fileInfo.Extension;
            this.Path = _fileInfo.DirectoryName;
            

        }

        public bool Exists () {
            _fileInfo.Refresh ();
            return _fileInfo.Exists; 
        }  
        public void Delete ()
        {
            _fileInfo.Delete ();
            _fileInfo.Refresh ();
        }

    }
}
