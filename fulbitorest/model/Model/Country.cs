using model.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace model.Model
{
    public class Country : IEntity
    {
        public static string UNDEFINED_NAME => "COUNTRY_UNDEFINED";

        [Key]
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
