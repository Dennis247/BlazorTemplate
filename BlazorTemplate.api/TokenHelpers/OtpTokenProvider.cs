using BlazorTemplate.api.Context;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorTemplate.api.TokenHelpers
{
    public class OtpTokenProvider : TotpSecurityStampBasedTokenProvider<User>
    {
        public const string ProviderKey = "ResetPassword";

        public override Task<bool> CanGenerateTwoFactorTokenAsync(UserManager<User> manager, User user)
        {
            return Task.FromResult(false);
        }
    }
}
