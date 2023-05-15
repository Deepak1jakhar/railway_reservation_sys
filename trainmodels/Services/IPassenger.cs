using trainmodels.Models;

namespace trainmodels.Repository
{
    public interface IPassenger
    {
        Passenger GetPassengerById(int id);
        List<Passenger> GetPassengersByUserId(int userId);
        bool AddPassenger(Passenger passenger);
        void UpdatePassenger(Passenger passenger);
        bool DeletePassenger(int id);
    }
}
