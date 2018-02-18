using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace model.Exceptions
{
    /// <summary>
    /// Some configuration either server side or client side is provoking a failure. Also protocol misuse issues.
    /// </summary>
    public class ConfigurationException : FulbitoException
    {
        public ConfigurationException(string msg)
            :base(msg)
        {
        }
    }
}
