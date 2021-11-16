using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TempaWeb.api.Context
{
    public class User : IdentityUser
    {
        public string RefreshToken { get; set; }
        public bool IsActive { get; set; }
        public DateTime RefreshTokenExpiryTime { get; set; }
    }
}
