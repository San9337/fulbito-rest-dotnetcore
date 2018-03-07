using System;
using System.Collections.Generic;
using System.Text;

namespace model.Business.Structures
{
    public class FacebookUser
    {
        /// <summary>
        /// The token issued by facebook to obtain this data
        /// </summary>
        public string IssuedToken { get; set; }

        public string Email { get; set; }
        public string UserName { get; set; }
    }
}
