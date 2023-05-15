using System.ComponentModel.DataAnnotations.Schema;

namespace trainmodels.Models.DTO
{
    public class BookingDTO
    {
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
    }
}
