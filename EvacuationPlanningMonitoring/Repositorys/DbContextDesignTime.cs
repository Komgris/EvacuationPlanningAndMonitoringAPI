﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql;

namespace EvacuationPlanningMonitoring.Repositorys
{
    public class DbContextDesignTime : IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<AppDbContext>();

            string? dbConStr = default;
            var builderStr = new NpgsqlConnectionStringBuilder();
            builderStr.Host = "";
            builderStr.Database = "";
            builderStr.Port = 5432;
            builderStr.Username = "";
            builderStr.Password = "";
            builderStr.SearchPath = "public";
            builderStr.IncludeErrorDetails = true;
            dbConStr = builderStr.ConnectionString;
            builder.UseNpgsql(
                connectionString: dbConStr,
                npgsqlOptionsAction: options =>
                {
                    options.MigrationsHistoryTable(HistoryRepository.DefaultTableName, "public");
                });
            builder.EnableSensitiveDataLogging();
            return new AppDbContext(builder.Options);
        }
    }
}
