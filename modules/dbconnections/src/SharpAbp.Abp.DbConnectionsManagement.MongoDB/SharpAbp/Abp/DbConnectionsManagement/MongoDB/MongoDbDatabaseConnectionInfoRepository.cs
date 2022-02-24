﻿using JetBrains.Annotations;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Domain.Repositories.MongoDB;
using Volo.Abp.MongoDB;

namespace SharpAbp.Abp.DbConnectionsManagement.MongoDB
{
    public class MongoDbDatabaseConnectionInfoRepository : MongoDbRepository<IDbConnectionsManagementMongoDbContext, DatabaseConnectionInfo, Guid>, IDatabaseConnectionInfoRepository
    {
        public MongoDbDatabaseConnectionInfoRepository(IMongoDbContextProvider<IDbConnectionsManagementMongoDbContext> dbContextProvider)
            : base(dbContextProvider)
        {
        }

        /// <summary>
        ///  Find by name
        /// </summary>
        /// <param name="name"></param>
        /// <param name="includeDetails"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual async Task<DatabaseConnectionInfo> FindByNameAsync(
            [NotNull] string name,
            bool includeDetails = true,
            CancellationToken cancellationToken = default)
        {
            Check.NotNullOrWhiteSpace(name, nameof(name));
            return await FindAsync(x => x.Name == name, includeDetails, GetCancellationToken(cancellationToken));
        }

        /// <summary>
        /// Find by name
        /// </summary>
        /// <param name="name"></param>
        /// <param name="expectedId"></param>
        /// <param name="includeDetails"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual async Task<DatabaseConnectionInfo> FindExpectedByNameAsync(
            string name,
            Guid? expectedId = null,
            bool includeDetails = true,
            CancellationToken cancellationToken = default)
        {
            return await (await GetMongoQueryableAsync())
                .WhereIf(!name.IsNullOrWhiteSpace(), x => x.Name == name)
                .WhereIf(expectedId.HasValue, x => x.Id != expectedId.Value)
                .As<IMongoQueryable<DatabaseConnectionInfo>>()
                .FirstOrDefaultAsync(GetCancellationToken(cancellationToken));
        }

        /// <summary>
        /// Get paged list
        /// </summary>
        /// <param name="skipCount"></param>
        /// <param name="maxResultCount"></param>
        /// <param name="sorting"></param>
        /// <param name="name"></param>
        /// <param name="databaseProvider"></param>
        /// <param name="includeDetails"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual async Task<List<DatabaseConnectionInfo>> GetPagedListAsync(
            int skipCount,
            int maxResultCount,
            string sorting = null,
            string name = "",
            string databaseProvider = "",
            bool includeDetails = true,
            CancellationToken cancellationToken = default)
        {
            return await (await GetMongoQueryableAsync())
                .WhereIf<DatabaseConnectionInfo, IMongoQueryable<DatabaseConnectionInfo>>(!name.IsNullOrWhiteSpace(), item => item.Name == name)
                .WhereIf<DatabaseConnectionInfo, IMongoQueryable<DatabaseConnectionInfo>>(!databaseProvider.IsNullOrWhiteSpace(), item => item.DatabaseProvider == databaseProvider)
                .OrderBy(sorting ?? nameof(DatabaseConnectionInfo.Name))
                .As<IMongoQueryable<DatabaseConnectionInfo>>()
                .PageBy<DatabaseConnectionInfo, IMongoQueryable<DatabaseConnectionInfo>>(skipCount, maxResultCount)
                .ToListAsync(GetCancellationToken(cancellationToken));
        }

        /// <summary>
        /// Get count
        /// </summary>
        /// <param name="name"></param>
        /// <param name="databaseProvider"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual async Task<int> GetCountAsync(
            string name = "",
            string databaseProvider = "",
            CancellationToken cancellationToken = default)
        {
            return await (await GetMongoQueryableAsync())
                .WhereIf<DatabaseConnectionInfo, IMongoQueryable<DatabaseConnectionInfo>>(!name.IsNullOrWhiteSpace(), item => item.Name == name)
                .WhereIf<DatabaseConnectionInfo, IMongoQueryable<DatabaseConnectionInfo>>(!databaseProvider.IsNullOrWhiteSpace(), item => item.DatabaseProvider == databaseProvider)
                .As<IMongoQueryable<DatabaseConnectionInfo>>()
                .CountAsync(GetCancellationToken(cancellationToken));
        }


    }
}
