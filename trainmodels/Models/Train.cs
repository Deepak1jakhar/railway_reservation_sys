using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace trainmodels.Models
{
    [Table("Train")]
    public class Train
    {
        [Key]
        [Required]
        public int TrainId { get; set; }

        [Required(ErrorMessage = "Train name is required.")]
        [RegularExpression("[aA-zZ]*", ErrorMessage = "Name must be only alphabets")]
        [StringLength(50, ErrorMessage = "Train name cannot exceed 50 characters.")]
        public string TrainName { get; set; } = null!;

        [RegularExpression("[aA-zZ]*", ErrorMessage = "Source must be only alphabets")]
        [Required(ErrorMessage = "Source is required.")]
        [StringLength(100, ErrorMessage = "Source cannot exceed 100 characters.")]
        public string Source { get; set; }

        [RegularExpression("[aA-zZ]*", ErrorMessage = "Destination must be only alphabets")]
        [Required(ErrorMessage = "Destination is required.")]
        [StringLength(100, ErrorMessage = "Destination cannot exceed 100 characters.")]
        public string Destination { get; set; }

        [DataType(DataType.DateTime)]
        [Required(ErrorMessage = "Departure time is required.")]
        public DateTime DepartureTime { get; set; }

        [DataType(DataType.DateTime)]
        [Required(ErrorMessage = "Arrival time is required.")]
        public DateTime ArrivalTime { get; set; }

        [Required]
        public int Fare { get; set; }

        [Required(ErrorMessage = "Total seats is required.")]
        public int TotalSeats { get; set; }

        [Required(ErrorMessage = "Available seats is required.")]
        public int AvailableSeats { get; set; }
    }
}
