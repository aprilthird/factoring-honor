using FACTORINGHONOR.PE.WEB.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace FACTORINGHONOR.PE.WEB.Services
{
    public interface IUserConfigurationService
    {
        Task<UserConfigurationViewModel> GetUserConfiguration(ClaimsPrincipal userClaim);
    }
}
