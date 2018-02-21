using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace model.Enums
{
    public enum Gender
    {
        [Description("undefined")]
        Undefined = 0,
        [Description("male")]
        Male = 1,
        [Description("female")]
        Female = 2,
        
    }
}
