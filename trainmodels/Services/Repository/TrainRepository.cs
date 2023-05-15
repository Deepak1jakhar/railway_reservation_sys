using trainmodels.Data;
using trainmodels.Models;
using trainmodels.Repository;

namespace trainmodels.Services.Repository
{
    public class TrainRepository : ITrain
    {
        private readonly RailContext _db;
        public TrainRepository(RailContext db)
        {
            _db = db;
        }

        public async Task<IEnumerable<Train>> GetAllAsync()
        {
            var trains = _db.trains.ToList();
            return trains;
        }
        public async Task<Train> AddTrainAsync(Train train)
        {
            try
            {
                await _db.trains.AddAsync(train);
                await _db.SaveChangesAsync();
                return train;
            }
            catch (Exception ex)
            {
                throw new Exception("All the fields are required in train");
            }
            
        }

        public void DeleteTrainAsync(int id)
        {
            var obj = _db.trains.Where(t => t.TrainId == id).FirstOrDefault();
            if (obj != null)
            {
                _db.trains.Remove(obj);
                _db.SaveChanges();
            }
            else
            {
                throw new Exception("Train not found to delete");
            }
        }

        public async Task<Train> GetTrainByIdAsync(int id)
        {
            var obj = _db.trains.Find(id);
            if(obj==null)
            {
                throw new Exception("No train found");
            }
            return obj;
        }

        public List<Train> GetTrainBySourceDestinationAsync(string source, string destination)
        {
            var obj =  _db.trains.Where(t => t.Source == source && t.Destination == destination).ToList();
            return obj;
        }

        public async Task<Train> UpdateTrainAsync(int id, Train train)
        {
            var tr = _db.trains.FirstOrDefault(t=>t.TrainId == id);
            if (tr == null)
            {
                throw new Exception("No train available to update");
            }
            tr.TrainName = train.TrainName;
            tr.TrainId = train.TrainId;
            tr.Source = train.Source;
            tr.Destination = train.Destination;
            tr.ArrivalTime = train.ArrivalTime;
            tr.AvailableSeats = train.AvailableSeats;
            tr.DepartureTime = train.DepartureTime;
            tr.TotalSeats = train.TotalSeats;
            await _db.SaveChangesAsync();
            return tr;
        }
    }
}
