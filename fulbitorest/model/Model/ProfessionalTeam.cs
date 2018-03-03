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
        public string FormattedName => Name + " - " + CountryName;

        public static string UNDEFINED_NAME => "TEAM_FAN_UNDEFINED";
        public static string UNDEFINED_LOGO => "LOGO_UNDEFINED";
        public static ProfessionalTeam UNDEFINED { get {
                var undef = new ProfessionalTeam(UNDEFINED_NAME, Country.UNDEFINED_NAME);
                undef.LogoUrl = UNDEFINED_LOGO;
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
