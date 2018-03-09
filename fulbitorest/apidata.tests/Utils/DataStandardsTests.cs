using apidata.Utils;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace apidata.tests.Utils
{
    [TestClass]
    public class DataStandardsTests
    {
        [TestMethod]
        public void GivenNullDate_FormatDate_ReturnsNull()
        {
            DateTime? date = null;

            var dateFormat = DataStandards.FormatDate(date);

            Assert.AreEqual(null, dateFormat);
        }

        [TestMethod]
        public void DatesAreCorrectlyFormatted_ToData_YYYY_MM_DD()
        {
            var date = DateTime.Now;

            var expectedDateFormat = date.ToString("yyyy-MM-dd");
            var dateFormat = DataStandards.FormatDate(date);

            Assert.AreEqual(expectedDateFormat, dateFormat);
        }

        [TestMethod]
        public void DatesAreCorrectlyFormatted_FromData_YYYY_MM_DD()
        {
            var expectedDate = DateTime.Now;

            var dateFormat = expectedDate.ToString("yyyy-MM-dd");
            var realDate = DataStandards.FormatDate(dateFormat);

            Assert.AreEqual(expectedDate.Date, ((DateTime)realDate).Date);
        }

        [TestMethod]
        public void DatesAreCorrectlyFormatted_FromData_YYYY_MM_DD_HH_MM()
        {
            var expectedDate = DateTime.Now;

            var dateFormat = expectedDate.ToString(DataStandards.DATE_TIME_FORMAT);
            var realDate = DataStandards.FormatDateTime(dateFormat);

            Assert.AreEqual(expectedDate.Date, ((DateTime)realDate).Date);
            Assert.AreEqual(expectedDate.Hour, ((DateTime)realDate).Hour);
            Assert.AreEqual(expectedDate.Minute, ((DateTime)realDate).Minute);
        }
    }
}
