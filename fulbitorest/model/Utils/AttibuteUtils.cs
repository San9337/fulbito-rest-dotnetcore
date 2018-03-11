using model.Attributes;
using model.Enums;
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
            return enumValue.GetCustomAttribute<DescriptionAttribute>().Description;
        }

        public static string GetCode(this Enum enumValue)
        {
            return enumValue.GetCustomAttribute<CodeAttribute>().Code;
        }

        public static SlotEnum GetEnumValueFromCode(string code)
        {
            var slotValues = Enum.GetValues(typeof(SlotEnum));
            var enumerator = slotValues.GetEnumerator();
            while (enumerator.MoveNext())
            {
                var slotValue = (SlotEnum) enumerator.Current;
                if (slotValue.GetCode() == code)
                    return slotValue;
            }
            return SlotEnum.UNDEFINED;
        }

        private static TCustomAttribute GetCustomAttribute<TCustomAttribute>(this Enum enumValue) where TCustomAttribute : Attribute
        {
            var type = enumValue.GetType();

            var memberInfo = type.GetMember(enumValue.ToString()).First();
            var attribute = Attribute.GetCustomAttribute(memberInfo, typeof(TCustomAttribute)) as TCustomAttribute;

            return attribute;
        }
    }
}
