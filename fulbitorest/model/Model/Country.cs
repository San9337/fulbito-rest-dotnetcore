using model.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace model.Model
{
    public class Country : IEntity
    {
        public static Country UNDEFINED
        {
            get
            {
                return new Country()
                {
                    Id = 1,
                    Name = "COUNTRY_UNDEFINED"
                };
            }
        }

        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public bool IsUndefined()
        {
            return this.Name == Country.UNDEFINED.Name;
        }
    }
}
