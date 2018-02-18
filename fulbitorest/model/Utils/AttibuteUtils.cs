using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace model.Utils
{
    public static class AttibuteUtils
    {
        public static string GetDescription(this Enum enumValue)
        {
            var type = enumValue.GetType();

            var memberInfo = type.GetMember(enumValue.ToString()).First();
            var attribute = Attribute.GetCustomAttribute(memberInfo, typeof(DescriptionAttribute)) as DescriptionAttribute;

            return attribute.Description;
        }
    }
}
