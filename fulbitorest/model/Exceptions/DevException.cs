using System;
using System.Collections.Generic;
using System.Text;

namespace model.Exceptions
{
    /// <summary>
    /// Someone screwed up
    /// </summary>
    public class DevException : Exception
    {
        public DevException(string message) : base(message)
        {
        }
    }
}
