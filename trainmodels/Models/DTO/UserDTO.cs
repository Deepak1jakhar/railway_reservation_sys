using System.ComponentModel.DataAnnotations;

namespace trainmodels.Models.DTO
{
    public class UserDTO
    {
        public int UserId { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }

        public int Age { get; set; }
        public string UserName { get; set; }

        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        public string Password { get; set; }

    }
}
