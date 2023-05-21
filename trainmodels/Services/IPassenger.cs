using trainmodels.Models;

namespace trainmodels.Repository
{
    public interface IPassenger
    {
        Passenger GetPassengerById(int id);
        bool AddPassenger(Passenger passenger);
        void UpdatePassenger(Passenger passenger);
        List<Passenger> GetPassengersByBookingId(int bookingId);
        bool DeletePassenger(int id);
    }
}
