using System;
using System.Collections.Generic;
using System.Text;

namespace testingutils.Mocking
{
    public static class Mocker
    {
        public static T MockAllValues<T>(T data)
        {
            foreach(var prop in typeof(T).GetProperties())
            {
                if(prop.PropertyType == typeof(string))
                    prop.SetValue(data, Guid.NewGuid().ToString());
            }

            return data;
        }
    }
}
