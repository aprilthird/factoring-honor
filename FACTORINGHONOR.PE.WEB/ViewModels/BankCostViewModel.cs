using FACTORINGHONOR.PE.CORE.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FACTORINGHONOR.PE.WEB.ViewModels
{
    public class BankCostViewModel
    {
        [Display(Name = "Id")]
        public Guid? Id { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = ConstantHelpers.MESSAGES.VALIDATION.REQUIRED)]
        [Display(Name = "Nombre", Prompt = "Nombre")]
        public string Name { get; set; }

        [Display(Name = "Valor", Prompt = "Valor")]
        public double Value { get; set; }

        [Display(Name = "Tipo", Prompt = "Tipo")]
        public bool IsFinal { get; set; }

        public double ConvertedValue { get; set; }
    }
}
