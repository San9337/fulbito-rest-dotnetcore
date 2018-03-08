using model.Business;
using model.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace model.Model
{
    /// <summary>
    /// Represents a real world professional team
    /// </summary>
    public class ProfessionalTeam : IEntity
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }
        public string CountryName { get; set; }
        public string LogoUrl { get; set; }

        [NotMapped]
        public string DisplayName => Name + ", " + CountryName;

        public static ProfessionalTeam UNDEFINED { get {
                var undef = new ProfessionalTeam("TEAM_FAN_UNDEFINED", Location.UNDEFINED.CountryName);
                undef.Id = 1;
                undef.LogoUrl = "LOGO_UNDEFINED";
                return undef;
            } } 

        public ProfessionalTeam()
        {
        }

        public ProfessionalTeam(string name, string countryName)
        {
            Name = name;
            CountryName = countryName;
        }

        public override string ToString() => $"{Name} - {CountryName}";
    }
}
