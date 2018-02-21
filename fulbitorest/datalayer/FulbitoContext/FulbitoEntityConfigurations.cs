using Microsoft.EntityFrameworkCore;
using model.Model;
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

            modelBuilder.Entity<Team>().HasIndex(u => new { u.Name, u.CountryName });
        }
    }
}
