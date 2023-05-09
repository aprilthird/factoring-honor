using FACTORINGHONOR.PE.CORE.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FACTORINGHONOR.PE.WEB.ViewModels
{
    public class CurrencyTypeViewModel
    {
        [Display(Name = "Id")]
        public Guid? Id { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = ConstantHelpers.MESSAGES.VALIDATION.REQUIRED)]
        [Display(Name = "Nombre", Prompt = "Nombre")]
        public string Name { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = ConstantHelpers.MESSAGES.VALIDATION.REQUIRED)]
        [Display(Name = "Nombre Corto", Prompt = "Nombre Corto")]
        public string ShortName { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = ConstantHelpers.MESSAGES.VALIDATION.REQUIRED)]
        [Display(Name = "Símbolo", Prompt = "Símbolo")]
        public string Symbol { get; set; }

        [Display(Name = "Valor", Prompt = "Valor")]
        public double Value { get; set; }
    }
}
