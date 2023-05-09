using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FACTORINGHONOR.PE.ENTITIES.Models
{
    public class CurrencyType
    {
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string ShortName { get; set; }

        [Required]
        public string Symbol { get; set; }

        public double Value { get; set; }

        public IEnumerable<FeeReceipt> FeeReceipts { get; set; }

        public IEnumerable<ApplicationUser> ApplicationUsers { get; set; }

        public IEnumerable<BankCost> BankCosts { get; set; }
    }
}
