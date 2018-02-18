using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Text;

namespace datalayer.FulbitoContext
{
    //https://docs.microsoft.com/en-us/ef/core/miscellaneous/cli/dbcontext-creation
    public class FulbitoContextFactory : IDesignTimeDbContextFactory<FulbitoDbContext>
    {
        FulbitoDbContext IDesignTimeDbContextFactory<FulbitoDbContext>.CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<FulbitoDbContext>();

            //This will only be called from the CLI migration commands
            optionsBuilder.UseMySql("server=localhost;userid=fulbitoDB;password=fulbitoDB;database=fulbito;");

            return new FulbitoDbContext(optionsBuilder.Options);
        }
    }
}
