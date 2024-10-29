using System;
using System.ComponentModel.DataAnnotations;

namespace ThaiBubbles_H6.Model
{
    public class Login
    {
        [Key]
        public int LoginID { get; set; }

        public string Email { get; set; } = string.Empty;

        public string Password { get; set; } = string.Empty;

        public int RoleId { get; set; }

        public int UserUserId { get; set; }
    }
}

