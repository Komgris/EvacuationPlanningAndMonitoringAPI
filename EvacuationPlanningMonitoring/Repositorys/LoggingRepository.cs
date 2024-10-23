using EvacuationPlanningMonitoring.Models.DbModels;
using EvacuationPlanningMonitoring.Repositorys.Interfaces;

namespace EvacuationPlanningMonitoring.Repositorys
{
    public class LoggingRepository : BaseRepository<LoggingModel>, ILoggingRepository
    {
        public LoggingRepository(AppDbContext context) : base(context)
        {
        }

        public async Task CreateLog(string action, string data, string vehicleID, string ZoneID)
        {
            var dateTime = DateTime.Now;
            Add(new LoggingModel()
            {
                Action = action,
                Data = data,
                VehicleID = vehicleID,
                ZoneID = ZoneID,
                CreateTime= dateTime,
            });
            await SaveChangesAsync();
        }
    }
}
