using model.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace model.Model
{
    public class State : IEntity
    {
        public static State UNDEFINED
        {
            get
            {
                return new State()
                {
                    Country = Country.UNDEFINED,
                    Id = 1,
                    Name = "STATE_UNDEFINED"
                };
            }
        }

        [Key]
        public int Id { get; set; }

        public string Name { get; set; }
        public virtual Country Country { get; set; }

    }
}
