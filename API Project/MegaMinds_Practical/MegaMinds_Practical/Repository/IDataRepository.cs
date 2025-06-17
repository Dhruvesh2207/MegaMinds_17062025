using MegaMinds_Practical.Model;

namespace MegaMinds_Practical.Repository
{
    public interface IDataRepository
    {
        Task<List<ObservationsModel>> GetAllObservationsAsync();
        Task<bool> SaveObservationAsync(ObservationsModel observation);
    }
}
