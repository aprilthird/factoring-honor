using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FACTORINGHONOR.PE.WEB.ViewModels
{
    public class UserConfigurationViewModel
    {
        [Display(Name = "Tipo de Tasa", Prompt = "Tipo de Tasa")]
        public int RateType { get; set; }

        [Display(Name = "Tipo de Cambio", Prompt = "Tipo de Cambio")]
        public Guid CurrencyTypeId { get; set; }

        public CurrencyTypeViewModel CurrencyType { get; set; }
    }
}
