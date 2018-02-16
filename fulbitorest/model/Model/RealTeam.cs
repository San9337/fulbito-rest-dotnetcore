using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace model.Model
{
    public class RealTeam
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string LogoUrl { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}
