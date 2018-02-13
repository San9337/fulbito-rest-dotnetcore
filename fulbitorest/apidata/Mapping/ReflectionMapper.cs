using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace apidata.Mapping
{
    public static class ReflectionMapper
    {
        /// <summary>
        /// Works as long the property names match exactly
        /// </summary>
        public static TTo MapTo<TTo>(this object from) where TTo : class,new()
        {
            var to = (TTo)Activator.CreateInstance(typeof(TTo));

            var fromProperties = from.GetType().GetProperties();
            var toProperties = typeof(TTo).GetProperties();
            foreach (var fromProp in fromProperties)
            {
                var toProp = toProperties.FirstOrDefault(p => p.Name == fromProp.Name);
                if(toProp != null)
                    toProp.SetValue(to, fromProp.GetValue(from));
            }

            return to;
        }
    }
}
