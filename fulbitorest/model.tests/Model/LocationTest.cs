using Microsoft.VisualStudio.TestTools.UnitTesting;
using model.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace model.tests.Model
{
    [TestClass]
    public class LocationTest
    {
        [TestMethod]
        public void DistanceCalculationIsAccurate_3KM()
        {
            //http://boulter.com/gps/distance/
            var expectedDistanceInMeters = 3230;

            var bsAs = new Location("House", -34.795947, -58.360915);
            var newYork = new Location("Turdera", -34.790660, -58.395548);

            var distance = bsAs.DistanceTo(newYork);
            var difference = Math.Abs(expectedDistanceInMeters - distance);
            Assert.IsTrue(difference < 30);
        }

        [TestMethod]
        public void DistanceCalculationIsAccurate_10KM()
        {
            //http://boulter.com/gps/distance/
            var expectedDistanceInMeters = 9990;

            var bsAs = new Location("Some place in Egypt", 30.043427, 31.227078);
            var newYork = new Location("New York", 30.101910, 31.305911);

            var distance = bsAs.DistanceTo(newYork);
            var difference = Math.Abs(expectedDistanceInMeters - distance);
            Assert.IsTrue(difference < 100);
        }
    }
}
