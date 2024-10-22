﻿namespace EvacuationPlanningMonitoring.Services.Interfaces
{
    public interface IRedisService
    {
        Task<T?> GetAsync<T>(string key);
        Task SetAsync<T>(string key, T value, TimeSpan? expiration = null);
        Task RemoveAsync(string key);
        Task<bool> UpdateAsync<T>(string key, T value, TimeSpan? expiration = null);
    }
}