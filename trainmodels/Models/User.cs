using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace trainmodels.Models
{
    [Table("User")]
    public class User
    {
        public int UserId { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }

        public int Age { get; set; }
        public string UserName { get; set; }
        public string Role { get; set; }
        public string Token { get; set; }

        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        public string Password { get; set; }
        public ICollection<Booking> Bookings { get; set; }
    }
}
