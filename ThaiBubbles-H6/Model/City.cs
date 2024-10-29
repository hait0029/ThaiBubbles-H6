using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ThaiBubbles_H6.Model
{
    public class City
    {
        [Key]
        public int CityID { get; set; } = 0;
        public string CityName { get; set; } = string.Empty;
        public int ZIPCode { get; set; } = 0;


    }
}
