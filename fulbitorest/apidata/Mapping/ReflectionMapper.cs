using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using model.Exceptions;

namespace apidata.Mapping
{
    public static class ReflectionMapper
    {
        /// <summary>
        /// Works as long the property names match exactly
        /// </summary>
        internal static TTo MapTo<TTo>(this object from) where TTo : class,new()
        {
            if (from == null)
                throw new DevException("Cannot map a null Entity when attempting to create: " + typeof(TTo).Name);

            var to = (TTo)Activator.CreateInstance(typeof(TTo));

            var fromProperties = from.GetType().GetProperties();
            var toProperties = typeof(TTo).GetProperties();
            foreach (var fromProp in fromProperties)
            {
                var toProp = toProperties.FirstOrDefault(p => p.Name == fromProp.Name);
                if(toProp != null)
                {
                    //Matching types
                    if(fromProp.PropertyType == toProp.PropertyType)
                        toProp.SetValue(to, fromProp.GetValue(from));
                    //Anything to a string
                    else if(toProp.PropertyType == typeof(string) && fromProp.PropertyType != typeof(DateTime))
                    {
                        var fromValue = fromProp.GetValue(from);
                        toProp.SetValue(to, fromValue?.ToString() ?? "");
                    }
                        
                }
            }

            return to;
        }
    }
}
