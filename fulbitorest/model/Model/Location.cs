using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace model.Model
{
    public class Location
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }
        
        public virtual Country Country { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}
