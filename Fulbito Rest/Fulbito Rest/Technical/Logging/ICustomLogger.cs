using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FulbitoRest.Technical.Logging
{
    public interface ICustomLogger
    {
        void Log(string message);
    }
}
