﻿using EvacuationPlanningMonitoring.Models;
using EvacuationPlanningMonitoring.Models.DbModels;
using EvacuationPlanningMonitoring.Repositorys.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EvacuationPlanningMonitoring.Repositorys
{
    public class VehicleRepositorycs : BaseRepository<VehicleModel>, IVehicleRepository
    {
        public VehicleRepositorycs(AppDbContext dbContext) : base(dbContext) { }

        public async Task ChangeVehicleStatus(string vehicleID, string status)
        {
            var vehicle = await GetQueryable().Where(x => x.VehicleID == vehicleID).FirstOrDefaultAsync();
            if (vehicle != null)
            {
                vehicle.Status = status;
                Update(vehicle);
                await SaveChangesAsync();
            }
        }

        public async Task ClearVehicle()
        {
            var vehicles = await GetQueryable().ToListAsync();
            foreach (var vehicle in vehicles)
            {
                Delete(vehicle);
            }
            await SaveChangesAsync();
        }

        public async Task Create(List<VehicleModel> vehicles)
        {
            foreach (var vehicle in vehicles)
            {
                Add(vehicle);
            }
            await SaveChangesAsync();
        }

        public async Task<List<VehicleModel>> GetAll()
        {
            var vehicles = await GetQueryable().ToListAsync();
            return vehicles;
        }

        public Task<List<VehicleModel>> GetAvaiableVehicle()
        {
            var vehicles = GetQueryable().Where(x => x.Status == VehicleStatus.Available).ToListAsync();
            return vehicles;
        }
    }
}
