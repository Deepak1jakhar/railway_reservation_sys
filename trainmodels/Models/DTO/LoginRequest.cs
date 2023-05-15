using System.ComponentModel.DataAnnotations;

namespace trainmodels.Models.DTO
{
    public class LoginRequest
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
