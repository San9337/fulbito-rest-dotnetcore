using System;
using System.Collections.Generic;
using System.Text;

namespace model.Attributes
{
    public class CodeAttribute : Attribute
    {
        public string Code { get; set; }
        public CodeAttribute(string code)
        {
            Code = code;
        }
    }
}
