using System.ComponentModel.DataAnnotations;

namespace SerMis.Models
{
    public class UserInfo
    {
        public int Id { get; set; }

        [Required]
        public string? Name { get; set; }

        public string? City { get; set; }
        public int Age { get; set; }
    }
}
