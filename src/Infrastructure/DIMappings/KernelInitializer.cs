using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mame.Doci.CrossCutting.Logging.Contracts;
using Mame.Doci.CrossCutting.Logging.Loggers;
using Mame.Doci.Data.LuceneRepository.Logic;
using Mame.Doci.Logic.DocumentManager.Contracts.Interfaces;
using Ninject;

namespace Mame.Doci.Data.DIMappings
{
    public class KernelInitializer
    {

        public IKernel Kernel { get; set; } 

        public KernelInitializer(IKernel kernel)
        {
            this.Kernel = kernel;

            kernel.Bind<IDocumentRepository> ().To<IndexingController>();
            kernel.Bind<ILogger> ().To<ConsoleLogger> ();

        }
    }
}
