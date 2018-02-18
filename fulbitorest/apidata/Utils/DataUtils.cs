using model.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace apidata.Utils
{
    public static class DataUtils
    {
        /// <summary>
        /// The body of a Http method
        /// </summary>
        public static void ValidateBody<TData>(this TData body)
        {
            if (body == null)
                throw new ConfigurationException("Couldnt parse "+typeof(TData).Name+" from body: null");
        }
    }
}
