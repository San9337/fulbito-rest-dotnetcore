using model.Interfaces;
using System;
using System.ComponentModel.DataAnnotations;

namespace model.Model
{
    public class Location : IEntity
    {
        public static Location UNDEFINED => new Location() {Id=1, Description = "UNDEFINED_LOCATION" };

        [Key]
        public int Id { get; set; }
        private double _latitude;
        private double _longitude;

        protected Location()
        {
        }

        public Location(string description, double latitude, double longitude)
        {
            Longitude = longitude;
            Latitude = latitude;
            Description = description;
        }

        public string Description { get; set; }

        public double Latitude
        {
            get { return _latitude; }
            set
            {
                if (value > 90.0 || value < -90.0)
                {
                    throw new ArgumentOutOfRangeException("Latitude", "Argument must be in range of -90 to 90");
                }
                _latitude = value;
            }
        }

        public double Longitude
        {
            get { return _longitude; }
            set {
                if (value > 180.0 || value < -180.0)
                {
                    throw new ArgumentOutOfRangeException("Longitude", "Argument must be in range of -180 to 180");
                }
                _longitude = value;
            }
        }

        public double DistanceTo(Location other)
        {
            //NOTE: couldnt import this package (doesnt register references) 
            //https://github.com/ghuntley/geocoordinate/blob/master/src/GeoCoordinatePortable/GeoCoordinate.cs

            if (double.IsNaN(Latitude) || double.IsNaN(Longitude) || double.IsNaN(other.Latitude) ||
                    double.IsNaN(other.Longitude))
            {
                throw new ArgumentException("Argument Latitude or longitude is not a number");
            }

            var d1 = Latitude * (Math.PI / 180.0);
            var num1 = Longitude * (Math.PI / 180.0);
            var d2 = other.Latitude * (Math.PI / 180.0);
            var num2 = other.Longitude * (Math.PI / 180.0) - num1;
            var d3 = Math.Pow(Math.Sin((d2 - d1) / 2.0), 2.0) +
                     Math.Cos(d1) * Math.Cos(d2) * Math.Pow(Math.Sin(num2 / 2.0), 2.0);

            return 6376500.0 * (2.0 * Math.Atan2(Math.Sqrt(d3), Math.Sqrt(1.0 - d3)));
        }
    }

}
