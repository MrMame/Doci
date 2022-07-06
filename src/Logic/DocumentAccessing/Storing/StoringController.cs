using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Mame.Doci.Logic.DocumentAccessing.Storing
{
    public class StoringController :IStoringController
    {
        public void Store (FileInfo storeFile)
        {
            if (storeFile is null) throw new ArgumentNullException ();
        }

        public void Store (List<FileInfo> storeFiles)
        {
            if (storeFiles is null) throw new ArgumentNullException ();
        }

     
    }
}
