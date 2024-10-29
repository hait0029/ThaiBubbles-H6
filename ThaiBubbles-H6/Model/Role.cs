using System;
using System.ComponentModel.DataAnnotations;

namespace ThaiBubbles_H6.Model
{
    public class Role
    {
        [Key]
        public int RoleID { get; set; }

        public string RoleType { get; set; } = string.Empty;
    }
}