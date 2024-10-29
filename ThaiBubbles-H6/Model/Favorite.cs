using System.ComponentModel.DataAnnotations;

namespace ThaiBubbles_H6.Model
{
    public class Favorite
    {
        [Key]
        public int FavoriteID { get; set; } = 0;
        public int? UserId { get; set; }


        public User? UserFK { get; set; }
    }
}
