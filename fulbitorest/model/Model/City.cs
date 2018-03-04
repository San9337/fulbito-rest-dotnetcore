﻿using model.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace model.Model
{
    public class City : IEntity
    {
        public static City UNDEFINED
        {
            get
            {
                return new City()
                {
                    Id = 1,
                    Name = "CITY_UNDEFINED",
                    State = State.UNDEFINED
                };
            }
        }

        [Key]
        public int Id { get; set; }

        public string Name { get; set; }
        public virtual State State { get; set; }
    }
}
