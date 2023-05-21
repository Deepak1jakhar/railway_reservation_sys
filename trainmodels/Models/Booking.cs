using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace trainmodels.Models
{
    [Table("Booking")]
    public class Booking
    {
        [Key]
        public int BookingId { get; set; }
        [ForeignKey("User")]
        public int UserId { get; set; }
        [ForeignKey("Train")]
        public int TrainId { get; set; }
        public string Source { get; set; }
        public string Destination { get; set; }
        public DateTime DepartureTime { get; set; }
        public bool Status { get; set; }
        public int NumberOfTickets { get; set; }
        public float TotalFare { get; set; }
        public ICollection<Passenger> Passengers { get; set; }
        public User User { get; set; }
        public Train Train { get; set; }
    }

}
