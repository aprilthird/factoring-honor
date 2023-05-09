using System;
using System.Collections.Generic;
using System.Text;

namespace FACTORINGHONOR.PE.ENTITIES.Models
{
    public class BankCost
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public double Value { get; set; }

        public bool IsFinal { get; set; }

        public Guid CurrencyTypeId { get; set; }

        public CurrencyType CurrencyType { get; set; }

        public Guid BankId { get; set; }

        public Bank Bank { get; set; }
    }
}
