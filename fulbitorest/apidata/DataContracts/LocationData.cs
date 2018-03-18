using System.Runtime.Serialization;

namespace apidata.DataContracts
{
    [DataContract]
    public class LocationData
    {
        [DataMember(Name = "description")]
        public string Description { get; set; }
        [DataMember(Name = "latitude")]
        public double Latitude { get; set; }
        [DataMember(Name = "longitude")]
        public double Longitude { get; set; }
    }
}
