using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace trainmodels.Models
{
    [Table("Admin")]
    public class Admin
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; } = null;
        [Required]
        public int Age { get; set; }
        [Required]
        public string Gender { get; set; }
        public string Password { get; set; } = null;

        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; } = null!;
    }
}
