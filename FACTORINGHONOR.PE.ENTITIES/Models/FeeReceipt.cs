using FACTORINGHONOR.PE.CORE.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace FACTORINGHONOR.PE.ENTITIES.Models
{
    public class FeeReceipt
    {
        public Guid Id { get; set; }

        [Required]
        public string UserId { get; set; }

        public ApplicationUser User { get; set; }

        [Required]
        public string IssuingRuc { get; set; }

        [Required]
        public string DebtorRuc { get; set; }

        public DateTime IssueDate { get; set; }

        public DateTime DiscountDate { get; set; }

        public DateTime PaymentDate { get; set; }

        public double TotalAmmount { get; set; }

        public double Withholding { get; set; }

        public Guid BankId { get; set; }

        public Bank Bank { get; set; }

        public Guid CurrencyTypeId { get; set; }

        public CurrencyType CurrencyType { get; set; }

        public DateTime CreateDate { get; set; } = DateTime.UtcNow;

        [NotMapped]
        public int DaysDiff => (PaymentDate - DiscountDate).Days;

        public double TermEffectiveRate { get; set; }

        [NotMapped]
        public double TermDiscountRate => TermEffectiveRate / (TermEffectiveRate + 1);

        [NotMapped]
        public double Discount => TotalAmmount * TermDiscountRate;

        [NotMapped]
        public double NetAmmount => TotalAmmount - Discount;

        public double TotalInitialCost { get; set; }

        public double TotalFinalCost { get; set; }

        [NotMapped]
        public double ReceivedAmmount => NetAmmount - TotalInitialCost - Withholding;

        [NotMapped]
        public double DeliveredAmmount => TotalAmmount + TotalFinalCost - Withholding;

        [NotMapped]
        public double TotalAnualEffectiveCost => Math.Pow(DeliveredAmmount / ReceivedAmmount, ConstantHelpers.Bank.COMMERCIAL_YEAR / DaysDiff) - 1;
    }
}
