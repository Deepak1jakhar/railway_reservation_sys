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

        public async Task<Train> UpdateTrainAsync(Train train)
        {
            _db.trains.Update(train);
            await _db.SaveChangesAsync();
            return train;

        }
    }
}
