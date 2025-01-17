﻿using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using Volo.Abp.EntityFrameworkCore;

namespace SharpAbp.Abp.EntityFrameworkCore
{
    public static class ConfigurationExtensions
    {
        public static EfCoreDatabaseProvider GetDatabaseProvider(
            this IConfiguration configuration,
            EfCoreDatabaseProvider defaultValue = EfCoreDatabaseProvider.PostgreSql)
        {
            if (Enum.TryParse(configuration["EfCoreOptions:DatabaseProvider"], out EfCoreDatabaseProvider databaseProvider))
            {
                return databaseProvider;
            }

            return defaultValue;
        }

        public static Dictionary<string, string> GetProperties(this IConfiguration configuration)
        {
            var options = configuration.GetSection("EfCoreOptions").Get<SharpAbpEfCoreOptions>();
            return options.Properties;
        }

        public static string GetOracleSQLCompatibility(this IConfiguration configuration)
        {
            var properties = configuration.GetProperties();
            if (properties.ContainsKey("OracleSQLCompatibility"))
            {
                return properties["OracleSQLCompatibility"];
            }
            return "11";
        }
    }
}
