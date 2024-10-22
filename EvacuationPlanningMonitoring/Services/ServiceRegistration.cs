using EvacuationPlanningMonitoring.Services.Interfaces;
using EvacuationPlanningMonitoring.Validators;
using EvacuationPlanningMonitoring.Validators.Interfaces;

namespace EvacuationPlanningMonitoring.Services
{
    public static class ServiceRegistration
    {
        public static void AddMyServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IPlanService, PlanService>();
            services.AddScoped<IVehicleService, VehicleService>();
            services.AddScoped<IEvacuationService, EvacuationService>();
            //validator
            services.AddScoped<IEvacuationsValidator, EvacuationsValidator>();
            services.AddScoped<IVehiclesValidator, VehiclesValidator>();
        }
    }
}
