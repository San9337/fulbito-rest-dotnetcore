using model.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace model.Utils
{
    public static class EnumUtils
    {
        public static IEnumerable<T> Values<T>()
        {
            if (!typeof(T).IsEnum)
                throw new DevException("Only enums are valid");

            return Enum.GetValues(typeof(T)).Cast<T>();
        }
    }
}
