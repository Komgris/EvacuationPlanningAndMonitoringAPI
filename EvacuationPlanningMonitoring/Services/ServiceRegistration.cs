using EvacuationPlanningMonitoring.Services.Interfaces;

namespace EvacuationPlanningMonitoring.Services
{
    public static class ServiceRegistration
    {
        public static void AddMyServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IPlanService, PlanService>();
            services.AddScoped<IVehicleService, VehicleService>();
            services.AddScoped<IEvacuationService, EvacuationService>();
        }
    }
}
