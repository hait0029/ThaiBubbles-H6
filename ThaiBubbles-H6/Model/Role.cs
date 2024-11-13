using System;
using System.ComponentModel.DataAnnotations;

namespace ThaiBubbles_H6.Model
{
    public class Role
    {
        [Key]
        public int RoleID { get; set; }

        public string RoleType { get; set; } = string.Empty;
        // Navigation property for the one-to-many relationship with User
        [JsonIgnore]
        public List<User?> UserFk { get; set; } = new List<User?>(); // en til mange relation mellem User til Favorite

    }
}