using Microsoft.EntityFrameworkCore;
using model.Model;
using model.Model.Security;
using System;
using System.Collections.Generic;
using System.Text;

namespace datalayer.FulbitoContext
{
    public static class FulbitoEntityConfigurations
    {
        public static void ConfigureEntities(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserCredentials>().HasIndex(uc => uc.Email);
            modelBuilder.Entity<User>().HasIndex(u => u.Id);
            modelBuilder.Entity<User>().Property(u => u.RealTeamId).HasDefaultValue(ProfessionalTeam.UNDEFINED.Id);
            modelBuilder.Entity<User>().Property(u => u.CountryId).HasDefaultValue(Country.UNDEFINED.Id);
            modelBuilder.Entity<User>().Property(u => u.StateId).HasDefaultValue(State.UNDEFINED.Id);
            modelBuilder.Entity<User>().Property(u => u.CityId).HasDefaultValue(City.UNDEFINED.Id);

            modelBuilder.Entity<ProfessionalTeam>().HasIndex(u => new { u.Name, u.CountryName });

            modelBuilder.Entity<AuthContext>().HasIndex(c => c.RefreshToken);

            modelBuilder.Entity<Match>().HasMany(u => u.Players).WithOne(sp => sp.Match);
        }
    }
}
