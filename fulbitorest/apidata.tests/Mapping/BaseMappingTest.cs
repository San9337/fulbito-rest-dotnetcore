using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace apidata.tests.Mapping
{
    public abstract class BaseMappingTest
    {
        public void AssertIdMapped<T>(T result)
        {
            var mappedId = (int)result.GetType().GetProperty("Id").GetValue(result);
            Assert.IsNotNull(mappedId);
            Assert.IsFalse(mappedId == 0);
        }

        public void AssertDescriptionMapped<T>(T result)
        {
            var mappedDesc = (string)result.GetType().GetProperty("Description").GetValue(result);
            Assert.IsFalse(string.IsNullOrEmpty(mappedDesc));
        }

        public void AssertNoNulls<T>(T result)
        {
            foreach (var property in typeof(T).GetProperties())
            {
                Assert.IsNotNull(property.GetValue(result), property.Name + " of type " + property.PropertyType.Name);
            }
        }
    }
}
