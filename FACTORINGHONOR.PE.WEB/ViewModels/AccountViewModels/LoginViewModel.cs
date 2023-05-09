using FACTORINGHONOR.PE.CORE.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FACTORINGHONOR.PE.WEB.ViewModels.AccountViewModels
{
    public class LoginViewModel
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = ConstantHelpers.MESSAGES.VALIDATION.REQUIRED)]
        [Display(Name = "Usuario", Prompt = "Usuario")]
        public string UserName { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = ConstantHelpers.MESSAGES.VALIDATION.REQUIRED)]
        [Display(Name = "Contraseña", Prompt = "Contraseña")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
