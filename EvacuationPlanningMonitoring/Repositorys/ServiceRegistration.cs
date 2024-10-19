using EvacuationPlanningMonitoring.Services.Interfaces;
using EvacuationPlanningMonitoring.Services;
using EvacuationPlanningMonitoring.Repositorys.Interfaces;

namespace EvacuationPlanningMonitoring.Repositorys
{
    public static class ServiceRegistration
    {
        public static void AddRepository(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IEvacuationZoneRepository, EvacuationZoneRepository>();
            services.AddScoped<IEvacuationPlanRepository, EvacuationPlanRepository>();
            services.AddScoped<IVehicleRepository, VehicleRepositorycs>();
            services.AddScoped<IEvacuationZoneRepository, EvacuationZoneRepository>();
        }
    }
}
