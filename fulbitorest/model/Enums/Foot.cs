using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace model.Enums
{
    public enum Foot
    {
        [Description("USER_FOOT_UNDEFINED")]
        Undefined = 0,
        [Description("USER_FOOT_LEFT")]
        Left = 1,
        [Description("USER_FOOT_RIGHT")]
        Right = 2,
        [Description("USER_FOOT_BOTH")]
        Both = 3
    }
}
