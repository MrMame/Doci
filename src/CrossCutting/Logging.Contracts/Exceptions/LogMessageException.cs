using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mame.Doci.CrossCutting.Logging.Contracts.Exceptions
{
    [Serializable]
    internal class LogMessageException:Exception
    {
        LogMessageException ()
        {
        }

        LogMessageException (string message)
            : base (message)    
        {
        }

        LogMessageException (string message, Exception innerException)
            : base (message, innerException)
        {
        }

    }
}
