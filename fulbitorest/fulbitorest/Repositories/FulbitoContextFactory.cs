using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FulbitoRest.Repositories
{
    //https://docs.microsoft.com/en-us/ef/core/miscellaneous/cli/dbcontext-creation
    public class FulbitoContextFactory : IDesignTimeDbContextFactory<FulbitoDbContext>
    {
        FulbitoDbContext IDesignTimeDbContextFactory<FulbitoDbContext>.CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<FulbitoDbContext>();

            return new FulbitoDbContext(optionsBuilder.Options);
        }
    }
}
