using trainmodels.Models;

namespace trainmodels.Repository
{
    public interface ITrain
    {
        Task<IEnumerable<Train>> GetAllAsync();
        Task<Train> GetTrainByIdAsync(int id);
        List<Train> GetTrainBySourceDestinationAsync(string source, string destination);
        Task<Train> AddTrainAsync(Train train);
        Task<Train> UpdateTrainAsync(Train train);
        void DeleteTrainAsync(int id);
    }
}
