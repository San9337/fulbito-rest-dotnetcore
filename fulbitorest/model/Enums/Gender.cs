using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace model.Enums
{
    public enum Gender
    {
        [Description("USER_GENDER_UNDEFINED")]
        Undefined = 0,
        [Description("USER_GENDER_MALE")]
        Male = 1,
        [Description("USER_GENDER_FEMALE")]
        Female = 2,
    }
}
