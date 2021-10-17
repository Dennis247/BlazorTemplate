using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.DTO
{
    public class AuthResponseDto
    {
        public string Token { get; set; }
        public string RefreshToken { get; set; }
    }
}
