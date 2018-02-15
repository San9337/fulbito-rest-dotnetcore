using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FulbitoRest.Exceptions
{
    /// <summary>
    /// Throw whenever the parameters of a method breaks the "HAPPY PATH"
    /// </summary>
    public class UnexpectedInputException : FulbitoException
    {
        public UnexpectedInputException()
        {
        }

        public UnexpectedInputException(string message)
            : base(message)
        {
        }

        public UnexpectedInputException(string parameterName, string reason)
            : base("Parameter ( "+ parameterName+" ) is invalid, reason: " + reason)
        {
        }
    }
}
