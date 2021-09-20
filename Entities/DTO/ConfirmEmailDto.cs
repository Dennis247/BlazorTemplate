using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.DTO
{
    public class ConfirmEmailDto
    {
        public string Token { get; set; }
        public  string Email { get; set; }
    }
}

