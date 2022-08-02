using Mame.Doci.CrossCutting.Logging.Contracts;
using Mame.Doci.Logic.DocumentManager.Contracts;
using Ninject;
using System;
using System.IO;
using Mame.Doci.UI.ConsoleClient.CLParsing;
using Mame.Doci.Data.DIMappings;
using Mame.Doci.Logic.DocumentManager.Contracts.Interfaces;

namespace Mame.Doci.UI.ConsoleClient
{
    public class Program
    {

     
        static void Main (string[] args)
        {
            // Init DI Container
            var kernel = new StandardKernel();
            new KernelInitializer (kernel);

            // Init Application Dependencies
            var logger = kernel.Get<ILogger> ();
            var documentService = kernel.Get<IDocumentService> ();
            var documentRepository = kernel.Get<IDocumentRepository> ();

            try
            {
                // Parsing commandline
                var clp = new CommandLineArgumentsParser (args);
                if (clp.HasParsingErrors == true)
                {
                    logger.LogText (LogLevels.Error, "Invalid commandline parameters detected!");
                    return;
                }

                // process the commandline commands
                var commandProcessor = new CommandProcessor (logger, documentService, documentRepository);
                commandProcessor.DoCommands (clp);

                // End
                logger.LogText (LogLevels.Info, "Program finished!");

            } catch (Exception ex)
            {
                logger.LogText (LogLevels.Fatal, "Fatal Error\r\n" +
                                                ex.ToString ());
            }
        }


    }
}
