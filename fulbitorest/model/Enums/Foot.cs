using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace model.Enums
{
    public enum Foot
    {
        [Description("left-foot")]
        Left = 0,
        [Description("right-foot")]
        Right = 1,
        [Description("both-foot")]
        Both = 2
    }
}
