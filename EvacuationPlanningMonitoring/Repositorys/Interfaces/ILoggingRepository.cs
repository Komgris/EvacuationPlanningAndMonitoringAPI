using EvacuationPlanningMonitoring.Models.DbModels;

namespace EvacuationPlanningMonitoring.Repositorys.Interfaces
{
    public interface ILoggingRepository : IBaseRepository<LoggingModel>
    {
        Task CreateLog(string action, string data, string vehicleID, string ZoneID);
    }
}
