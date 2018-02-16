using Microsoft.EntityFrameworkCore;
using model;
using model.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FulbitoRest.Repositories
{
    //port: 3306
    //https://github.com/jasonsturges/mysql-dotnet-core
    //<DotNetCliToolReference Include="Microsoft.EntityFrameworkCore.Tools.DotNet" Version="2.0.0" />
    //pomelo
    //dotnet ef migrations add InitialCreate
    //dotnet ef database update
    //https://ef.readthedocs.io/en/staging/miscellaneous/cli/dotnet.html#dotnet-ef-database-drop
    public class FulbitoDbContext : DbContext
    {
        public FulbitoDbContext(DbContextOptions<FulbitoDbContext> contextOptions)
            : base(contextOptions)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ForMySqlUseIdentityColumns();

            modelBuilder.Entity<UserCredentials>().HasIndex(uc => uc.Email);
            modelBuilder.Entity<User>().HasIndex(u => u.Id);

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<User> Users { get; set; }
        public DbSet<UserCredentials> UserCredentials { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<Country> Countries { get; set; }
    }
}
