using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Text;

namespace FACTORINGHONOR.PE.DATA.Context
{
    public class FactoringHonorDbContextFactory : IDesignTimeDbContextFactory<FactoringHonorDbContext>
    {
        public FactoringHonorDbContextFactory() { }

        public FactoringHonorDbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<FactoringHonorDbContext>();

            builder.UseSqlServer(
                "Server=localhost;Database=FHDB;Trusted_Connection=True;MultipleActiveResultSets=true"
                //DataConnectionString
                , opts => opts.CommandTimeout((int)TimeSpan.FromMinutes(10).TotalSeconds));

            return new FactoringHonorDbContext(builder.Options);
        }
    }
}
