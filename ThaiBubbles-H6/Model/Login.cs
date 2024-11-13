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

        [JsonIgnore]
        public int? RoleId { get; set; } // Foreign key property
        public Role? RoleType { get; set; }  // Navigation property
    }
}

