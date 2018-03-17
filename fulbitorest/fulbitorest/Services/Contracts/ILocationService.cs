using apidata.DataContracts;
using model.Model;

namespace FulbitoRest.Services.Contracts
{
    public interface ILocationService : IService
    {
        Location CreateFrom(LocationData location);
    }
}
