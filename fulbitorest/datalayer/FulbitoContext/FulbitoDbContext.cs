using Microsoft.EntityFrameworkCore;
using model;
using model.Model;
using model.Model.Security;

namespace datalayer.FulbitoContext
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

            modelBuilder.ConfigureEntities();

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<User> Users { get; set; }
        public DbSet<UserCredentials> UserCredentials { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<State> States { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<ProfessionalTeam> ProfessionalTeams { get; set; }
        public DbSet<AuthContext> AuthContexts { get; set; }
    }
}
