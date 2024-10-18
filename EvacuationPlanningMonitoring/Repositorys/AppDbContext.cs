using EvacuationPlanningMonitoring.Models.DbModels;
using Microsoft.EntityFrameworkCore;

namespace EvacuationPlanningMonitoring.Repositorys
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<EvacuationPlanModel> EvacuationPlans { get; set; }
        public DbSet<EvacuationZoneModel> EvacuationZones { get; set; }
        public DbSet<VehicleModel> Vehicles { get; set; }
    }
}
