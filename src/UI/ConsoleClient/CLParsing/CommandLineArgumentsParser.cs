using CommandLine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mame.Doci.UI.ConsoleClient.CLParsing
{
    public class CommandLineArgumentsParser
    {

        public string AddDocument{get;}

        public bool HasParsingErrors { get; }


        public class ExporterOptions
        {
            [Option ('a', "add-document", Required = true, HelpText = "Add a document to the search DB.")]
            public string AddDocument { get; set; }
        }




        public CommandLineArgumentsParser (string[] args)
        {
            ParserResult<ExporterOptions> clpResult = Parser.Default.ParseArguments<ExporterOptions> (args);
            HasParsingErrors = clpResult is NotParsed<ExporterOptions>;
            if (!HasParsingErrors)
            {
                AddDocument = clpResult.Value.AddDocument;
            }

        }


    }
}
