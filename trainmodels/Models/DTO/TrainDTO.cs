using System.ComponentModel.DataAnnotations;

namespace trainmodels.Models.DTO
{
    public class TrainDTO
    {
        public int TrainId { get; set; }
        public string TrainName { get; set; } = null!;

        public string Source { get; set; }

        
        public string Destination { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime DepartureTime { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime ArrivalTime { get; set; }
        public int Fare { get; set; }
        public int TotalSeats { get; set; }
        public int AvailableSeats { get; set; }
    }
}
