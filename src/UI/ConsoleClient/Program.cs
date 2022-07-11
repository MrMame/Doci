using Mame.Doci.Logic.DocumentAccessing.Storing.Factories;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Mame.Doci.UI.ConsoleClient
{
    public class Program
    {
        static void Main (string[] args)
        {
            // Parsing commandline
            var clp = new CLParsing.CommandLineArgumentsParser (args);

            if (clp.HasParsingErrors == true){
            }
            else if (!string.IsNullOrEmpty (clp.AddDocument))
            {
                var documentFilename = new FileInfo(clp.AddDocument);

                IStoringForUser documentStoreController = StoringControllerFactory.CreateDefault ();
                documentStoreController.UserWantsToStore (documentFilename);
            }


        }
    }
}
