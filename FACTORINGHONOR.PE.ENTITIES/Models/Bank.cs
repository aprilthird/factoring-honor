using FACTORINGHONOR.PE.CORE.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FACTORINGHONOR.PE.ENTITIES.Models
{
    public class Bank
    {
        public Bank()
        {
            Id = Guid.NewGuid();
        }

        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string ShortName { get; set; }

        public int RateType { get; set; } = ConstantHelpers.Rate.Type.EFFECTIVE;

        public int RateTerm { get; set; } = ConstantHelpers.Bank.COMMERCIAL_YEAR;

        public double RateValue { get; set; }
        
        public IEnumerable<BankCost> BankCosts { get; set; }

        public IEnumerable<FeeReceipt> FeeReceipts { get; set; }
    }
}
