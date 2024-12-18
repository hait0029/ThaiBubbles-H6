﻿using System.ComponentModel.DataAnnotations;

namespace ThaiBubbles_H6.Model
{
    public class User
    {
        [Key]
        public int UserID { get; set; } = 0;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string FName { get; set; } = string.Empty;
        public string LName { get; set; } = string.Empty;
        public string PhoneNr { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public int? FavoriteId { get; set; }
        public int? CityId { get; set; }
        
        public City? Cities { get; set; }
        [JsonIgnore]
        public List<Favorite?> FavoriteFk { get; set; } = new List<Favorite?>(); // en til mange relation mellem User til Favorite

        // Foreign key for Role
        
        public int? RoleID { get; set; }

        // Navigation property for the Role
        
        public Role? Role { get; set; }

        [JsonIgnore]
        public List<Order?> Order { get; set; } = new List<Order?>(); // en til mange relation mellem User til Order
    }
}
