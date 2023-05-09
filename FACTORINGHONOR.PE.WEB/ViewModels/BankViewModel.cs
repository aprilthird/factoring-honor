using FACTORINGHONOR.PE.CORE.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FACTORINGHONOR.PE.WEB.ViewModels
{
    public class BankViewModel
    {
        [Display(Name = "Id")]
        public Guid? Id { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = ConstantHelpers.MESSAGES.VALIDATION.REQUIRED)]
        [Display(Name = "Nombre", Prompt = "Nombre")]
        public string Name { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = ConstantHelpers.MESSAGES.VALIDATION.REQUIRED)]
        [Display(Name = "Nombre Corto", Prompt = "Nombre Corto")]
        public string ShortName { get; set; }

        [Display(Name = "Tipo de Tasa", Prompt = "Tipo de Tasa")]
        public int RateType { get; set; }

        [Display(Name = "Período de Tasa", Prompt = "Período de Tasa")]
        public int RateTerm { get; set; }

        [Display(Name = "Valor de Tasa", Prompt = "Valor de Tasa")]
        public double RateValue { get; set; }

        public double ConvertedValue { get; set; }

        public string RateTypeText => ConstantHelpers.Rate.Type.VALUES[RateType];

        public string RateTermText => ConstantHelpers.Rate.TermDays.VALUES[RateTerm];
    }
}
