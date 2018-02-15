using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FulbitoRest.Exceptions
{
    public class FulbitoException : Exception
    {
        public FulbitoException()
        {
        }
        public FulbitoException(string msg)
            : base(msg)
        {
        }

    }
}
