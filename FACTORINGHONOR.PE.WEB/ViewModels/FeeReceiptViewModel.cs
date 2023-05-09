using FACTORINGHONOR.PE.CORE.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FACTORINGHONOR.PE.WEB.ViewModels
{
    public class FeeReceiptViewModel
    {
        [Display(Name = "Id")]
        public Guid? Id { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = ConstantHelpers.MESSAGES.VALIDATION.REQUIRED)]
        [Display(Name = "RUC Emisor", Prompt = "RUC Emisor")]
        public string IssuingRuc { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = ConstantHelpers.MESSAGES.VALIDATION.REQUIRED)]
        [Display(Name = "RUC Deudor", Prompt = "RUC Deudor")]
        public string DebtorRuc { get; set; }

        [Display(Name = "Fecha de Emisión", Prompt = "Fecha de Emisión")]
        public string IssueDate { get; set; }

        [Display(Name = "Fecha de Descuento", Prompt = "Fecha de Descuento")]
        public string DiscountDate { get; set; }

        [Display(Name = "Fecha de Pago", Prompt = "Fecha de Pago")]
        public string PaymentDate { get; set; }

        [Display(Name = "Monto Total Recibido", Prompt = "Pago Total Recibido")]
        public double TotalAmmount { get; set; }

        [Display(Name = "Retención", Prompt = "Retención")]
        public double Withholding { get; set; }

        [Display(Name = "Banco", Prompt = "Banco")]
        public Guid BankId { get; set; }

        public BankViewModel Bank { get; set; }

        public int DaysDiff { get; set; }

        public double TermEffectiveRate { get; set; }

        public double TermDiscountRate { get; set; }

        public double Discount { get; set; }

        public double NetAmmount { get; set; }

        public double TotalInitialCost { get; set; }

        public double TotalFinalCost { get; set; }

        public double ReceivedAmmount { get; set; }

        public double DeliveredAmmount { get; set; }

        public double TotalAnualEffectiveCost { get; set; }

        public int Number { get; set; }
    }
}
