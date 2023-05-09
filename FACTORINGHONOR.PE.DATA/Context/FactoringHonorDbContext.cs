using FACTORINGHONOR.PE.ENTITIES.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using FACTORINGHONOR.PE.CORE.Helpers;
using Microsoft.AspNetCore.Identity;

namespace FACTORINGHONOR.PE.DATA.Context
{
    public class FactoringHonorDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Bank> Banks { get; set; }
        public DbSet<BankCost> BankCosts { get; set; }
        public DbSet<CurrencyType> CurrencyTypes { get; set; }
        public DbSet<EntityRecord> EntityRecords { get; set; }
        public DbSet<FeeReceipt> FeeReceipts { get; set; }

        public FactoringHonorDbContext(DbContextOptions<FactoringHonorDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            var modelEntityTypes = builder.Model.GetEntityTypes();
            foreach (var modelEntityType in modelEntityTypes)
            {
                foreach (var foreignKey in modelEntityType.GetForeignKeys())
                {
                    if (!foreignKey.IsOwnership && foreignKey.DeleteBehavior == DeleteBehavior.Cascade)
                    {
                        foreignKey.DeleteBehavior = DeleteBehavior.Restrict;
                    }

                }
            }

            //builder.Entity<FeeReceipt>()
            //.HasOne(x => x.User)
            //.WithMany(x => x.FeeReceipts)
            //.OnDelete(DeleteBehavior.Restrict);
        }
    }
}
