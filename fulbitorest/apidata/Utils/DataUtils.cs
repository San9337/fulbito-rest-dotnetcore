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

        public static void ValidateBodyNotNulls<TData>(this TData body)
        {
            body.ValidateBody();
            foreach(var prop in typeof(TData).GetProperties())
            {
                if (prop.GetValue(body) == null)
                    throw new UnexpectedInputException("Nulls are not allowed in this methods body");
            }
        }
    }
}
