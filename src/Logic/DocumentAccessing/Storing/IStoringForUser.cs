using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Mame.Doci.UI.ConsoleClient
{
    public interface IStoringForUser
    {
        void UserWantsToStore (FileInfo fileName);
        void UserWantsToStore (List<FileInfo> fileNames);

    }
}
