using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Entities.DTO.UserDto
{
    public class UserDto
    {
        public string Id { get; set; }
        public string RefreshToken { get; set; }
        public bool IsActive { get; set; }
        public string RefreshTokenExpiryTime { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string EmailConfirmed { get; set; }
        public string PhoneNumber { get; set; }
        public string PhoneNumberConfirmed { get; set; }
        public string TwoFactorEnabled { get; set; }
        public string LockoutEnd { get; set; }
        public string LockoutEnabled { get; set; }
        public string AccessFailedCount { get; set; }
    }

    public class GetUserByIdDto
    {
        [Required]
        public string Id { get; set; }
    }
}
