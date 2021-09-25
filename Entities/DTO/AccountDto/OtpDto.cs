using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Entities.DTO
{
    public class OtpDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        public string Phone { get; set; }
    }

    public class TwoFADto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        public string Phone { get; set; }
    }

    public class Verify2FADto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        public string Phone { get; set; }
        public string token { get; set; }
    }
}
