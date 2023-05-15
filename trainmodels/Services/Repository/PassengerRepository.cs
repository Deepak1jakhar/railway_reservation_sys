using trainmodels.Data;
using trainmodels.Models;
using trainmodels.Repository;

namespace trainmodels.Services.Repository
{
    public class PassengerRepository : IPassenger
    {
        private readonly RailContext _db;
        public PassengerRepository(RailContext db)
        {
            _db = db;
        }
        public bool AddPassenger(Passenger passenger)
        {
            if (_db.passengers.Any(p => p.PassengerId == passenger.PassengerId))
            {
                return false;
            }
            _db.passengers.Add(passenger);
            _db.SaveChanges();
            return true;

        }

        public bool DeletePassenger(int id)
        {
            if (!_db.passengers.Any(p => p.PassengerId == id))
            {
                return false;
            }
            var obj = _db.passengers.Where(p => p.PassengerId == id).FirstOrDefault();
            _db.passengers.Remove(obj);
            return true;
        }

        public Passenger GetPassengerById(int id)
        {
            return _db.passengers.Where(p => p.PassengerId == id).FirstOrDefault();
        }

        public List<Passenger> GetPassengersByUserId(int userId)
        {
            return _db.passengers.Where(p => p.UserId == userId).ToList();
        }

        public void UpdatePassenger(Passenger passenger)
        {
            _db.passengers.Update(passenger);
            _db.SaveChanges();

        }
    }
}
