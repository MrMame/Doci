using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mame.Doci.CrossCutting.Logging.Contracts.Exceptions
{
    [Serializable]
    public class LogMessageException:Exception
    {
        public LogMessageException ()
        {
        }

        public LogMessageException (string message)
            : base (message)    
        {
        }

        public LogMessageException (string message, Exception innerException)
            : base (message, innerException)
        {
        }

    }
}
