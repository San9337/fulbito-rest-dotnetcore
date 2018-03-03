using model.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FulbitoRest.Services.Contracts
{
    public interface ILocationService : IService
    {
        Location GetOrCreate(string country, string state, string city);
    }
}
