using Dapper;
using MegaMinds_Practical.DbContextDBContext;
using MegaMinds_Practical.Model;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace MegaMinds_Practical.Repository
{
    public class DataRepository : IDataRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public DataRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }
        public async Task<List<ObservationsModel>> GetAllObservationsAsync()
        {
            return await _dbContext.Observations
                .Include(o => o.Datas)
                    .ThenInclude(d => d.Properties)
                .ToListAsync();
        }

        public async Task<bool> SaveObservationAsync(ObservationsModel observation)
        {
            try
            {
                var existing = await _dbContext.Observations
                    .FirstOrDefaultAsync(o => o.Id == observation.Id);

                if (existing != null)
                {
                    _dbContext.Entry(existing).CurrentValues.SetValues(observation);
                }
                else
                {   
                    await _dbContext.Observations.AddAsync(observation);
                }

                await _dbContext.SaveChangesAsync();
                return true;
            }   
            catch
            {
                return false;
            }
        }



    }
}
