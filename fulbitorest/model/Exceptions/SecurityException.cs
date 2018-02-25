using System;
using System.Collections.Generic;
using System.Text;

namespace model.Exceptions
{
    public class SecurityException : FulbitoException
    {
        public SecurityException(string msg) : base(msg)
        {

        }
    }
}
