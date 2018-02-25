﻿using model.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace model.Model
{
    public class Team : IEntity
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }
        public string CountryName { get; set; }

        [NotMapped]
        public string FormattedName => Name + " - " + CountryName;

        public Team()
        {
        }

        public Team(string name, string countryName)
        {
            Name = name;
            CountryName = countryName;
        }

        public override string ToString() => $"{Name} - {CountryName}";
    }
}