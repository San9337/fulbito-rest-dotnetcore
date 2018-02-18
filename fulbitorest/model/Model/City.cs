using model.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace model.Model
{
    public class City : IEntity
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }
        public virtual State State { get; set; }
    }
}
