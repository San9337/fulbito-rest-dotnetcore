using apidata.DataContracts;
using model.Model;

namespace apidata.Mapping
{
    public static class LocationMapping
    {
        public static LocationData Map(this Location location)
        {
            return location.MapTo<LocationData>();
        }
    }
}
