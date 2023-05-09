using FACTORINGHONOR.PE.CORE.Helpers;
using FACTORINGHONOR.PE.DATA.Context;
using FACTORINGHONOR.PE.ENTITIES.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FACTORINGHONOR.PE.WEB.Data
{
    public static class DbInitializer
    {
        public static void Seed(FactoringHonorDbContext context, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            if (!context.CurrencyTypes.Any())
            {
                context.CurrencyTypes.AddRange(new List<CurrencyType>
                {
                    new CurrencyType { Name = "Nuevos Sol Peruano", ShortName = "PEN", Value = 1, Symbol = "S/" },
                    new CurrencyType { Name = "Dólar Estadounidense", ShortName = "USD", Value = 3.30, Symbol = "$" }
                });
                context.SaveChanges();
            }

            if (!context.Banks.Any())
            {
                context.Banks.AddRange(new List<Bank>
                {
                    new Bank { Name = "Interbank", ShortName = "IBK", RateType = ConstantHelpers.Rate.Type.EFFECTIVE, RateTerm = ConstantHelpers.Bank.COMMERCIAL_YEAR, RateValue = .25 },
                    new Bank { Name = "Banco de Crédito del Perú", ShortName = "BCP", RateType = ConstantHelpers.Rate.Type.EFFECTIVE, RateTerm = ConstantHelpers.Bank.COMMERCIAL_YEAR, RateValue = .60 }
                });
                context.SaveChanges();
            }

            if (!context.BankCosts.Any())
            {
                var ibkBank = context.Banks.FirstOrDefault(x => x.ShortName == "IBK");
                var pen = context.CurrencyTypes.FirstOrDefault(x => x.ShortName == "PEN");
                context.BankCosts.AddRange(new List<BankCost>
                {
                    new BankCost { Name = "Gastos Administrativos", Bank = ibkBank, IsFinal = false, CurrencyType = pen, Value = 10 },
                    new BankCost { Name = "Portes", Bank = ibkBank, IsFinal = false, CurrencyType = pen, Value = 6 },
                    new BankCost { Name = "Fotocopias", Bank = ibkBank, IsFinal = false, CurrencyType = pen, Value = 2 },
                    new BankCost { Name = "Gastos Administrativos", Bank = ibkBank, IsFinal = true, CurrencyType = pen, Value = 15 },
                });

                var bcpBank = context.Banks.FirstOrDefault(x => x.ShortName == "BCP");
                context.BankCosts.AddRange(new List<BankCost>
                {
                    new BankCost { Name = "Gastos Administrativos", Bank = bcpBank, IsFinal = false, CurrencyType = pen, Value = 52 },
                    new BankCost { Name = "Portes", Bank = bcpBank, IsFinal = false, CurrencyType = pen, Value = 7 },
                    new BankCost { Name = "Fotocopias", Bank = bcpBank, IsFinal = false, CurrencyType = pen, Value = 1 },
                    new BankCost { Name = "Gastos Administrativos", Bank = bcpBank, IsFinal = true, CurrencyType = pen, Value = 20 },
                    new BankCost { Name = "Fotocopias", Bank = bcpBank, IsFinal = true, CurrencyType = pen, Value = 1 },
                    new BankCost { Name = "Gastos Notariales", Bank = bcpBank, IsFinal = true, CurrencyType = pen, Value = 10 },
                });

                context.SaveChanges();
            }

            if (roleManager.FindByNameAsync(ConstantHelpers.Role.ADMIN).Result == null)
            {
                roleManager.CreateAsync(new IdentityRole(ConstantHelpers.Role.ADMIN));
            }

            if (roleManager.FindByNameAsync(ConstantHelpers.Role.CUSTOMER).Result == null)
            {
                roleManager.CreateAsync(new IdentityRole(ConstantHelpers.Role.CUSTOMER));
            }

            if (userManager.FindByEmailAsync("admin@factoringhonor.pe").Result == null)
            {
                var currencyType = context.CurrencyTypes.FirstOrDefault(x => x.ShortName == "PEN");

                var user = new ApplicationUser
                {
                    UserName = "admin",
                    Email = "admin@factoringhonor.pe",
                    BirthDate = DateTime.Parse("1990-10-10"),
                    Dni = "99988877",
                    Name = "Mabel Fabiola",
                    PaternalSurname = "Altamirano",
                    MaternalSurname = "Samaniego",
                    PhoneNumber = "999999999",
                    CurrencyTypeId = currencyType.Id,
                    RateType = ConstantHelpers.Rate.Type.EFFECTIVE
                };

                var result = userManager.CreateAsync(user, "Admin.123").Result;

                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, ConstantHelpers.Role.ADMIN).Wait();
                }
            }

            if (userManager.FindByEmailAsync("rcrojas@factoringhonor.pe").Result == null)
            {
                var currencyType = context.CurrencyTypes.FirstOrDefault(x => x.ShortName == "PEN");

                var user = new ApplicationUser
                {
                    UserName = "rcrojas",
                    Email = "rcrojas@factoringhonor.pe",
                    BirthDate = DateTime.Parse("1990-10-10"),
                    Dni = "99966655",
                    Name = "Rosario",
                    PaternalSurname = "Rojas",
                    MaternalSurname = "Curay",
                    PhoneNumber = "964388876",
                    CurrencyTypeId = currencyType.Id,
                    RateType = ConstantHelpers.Rate.Type.EFFECTIVE
                };

                var result = userManager.CreateAsync(user, "Finanzas.123").Result;

                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, ConstantHelpers.Role.CUSTOMER).Wait();
                }
            }
        }
    }
}
