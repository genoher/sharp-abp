﻿using JetBrains.Annotations;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;

namespace SharpAbp.Abp.DbConnectionsManagement
{
    [Authorize(DbConnectionsManagementPermissions.DatabaseConnectionInfos.Default)]
    public class DatabaseConnectionInfoAppService : DbConnectionsManagementAppServiceBase, IDatabaseConnectionInfoAppService
    {
        protected IDatabaseConnectionInfoManager DatabaseConnectionInfoManager { get; }
        protected IDatabaseConnectionInfoRepository DatabaseConnectionInfoRepository { get; }
        public DatabaseConnectionInfoAppService(
            IDatabaseConnectionInfoManager databaseConnectionInfoManager,
            IDatabaseConnectionInfoRepository databaseConnectionInfoRepository)
        {
            DatabaseConnectionInfoManager = databaseConnectionInfoManager;
            DatabaseConnectionInfoRepository = databaseConnectionInfoRepository;
        }

        /// <summary>
        /// Get DatabaseConnectionInfo by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize(DbConnectionsManagementPermissions.DatabaseConnectionInfos.Default)]
        public virtual async Task<DatabaseConnectionInfoDto> GetAsync(Guid id)
        {
            var databaseConnectionInfo = await DatabaseConnectionInfoRepository.GetAsync(id);
            return ObjectMapper.Map<DatabaseConnectionInfo, DatabaseConnectionInfoDto>(databaseConnectionInfo);
        }

        /// <summary>
        /// Find DatabaseConnectionInfo by name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        [Authorize(DbConnectionsManagementPermissions.DatabaseConnectionInfos.Default)]
        public virtual async Task<DatabaseConnectionInfoDto> FindByNameAsync([NotNull] string name)
        {
            Check.NotNullOrWhiteSpace(name, nameof(name));

            var databaseConnectionInfo = await DatabaseConnectionInfoRepository.FindByNameAsync(name);
            return ObjectMapper.Map<DatabaseConnectionInfo, DatabaseConnectionInfoDto>(databaseConnectionInfo);
        }

        /// <summary>
        /// Get paged list
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [Authorize(DbConnectionsManagementPermissions.DatabaseConnectionInfos.Default)]
        public virtual async Task<PagedResultDto<DatabaseConnectionInfoDto>> GetPagedListAsync(
            DatabaseConnectionInfoPagedRequestDto input)
        {
            var count = await DatabaseConnectionInfoRepository.GetCountAsync(input.Name, input.DatabaseProvider);

            var databaseConnectionInfos = await DatabaseConnectionInfoRepository.GetListAsync(
                input.SkipCount,
                input.MaxResultCount,
                input.Sorting,
                input.Name,
                input.DatabaseProvider);

            return new PagedResultDto<DatabaseConnectionInfoDto>(
              count,
              ObjectMapper.Map<List<DatabaseConnectionInfo>, List<DatabaseConnectionInfoDto>>(databaseConnectionInfos)
              );
        }

        /// <summary>
        /// Create
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [Authorize(DbConnectionsManagementPermissions.DatabaseConnectionInfos.Create)]
        public virtual async Task<Guid> CreateAsync(CreateDatabaseConnectionInfoDto input)
        {
            var databaseConnectionInfo = new DatabaseConnectionInfo(
                GuidGenerator.Create(),
                input.Name,
                input.DatabaseProvider,
                input.ConnectionString);

            await DatabaseConnectionInfoManager.CreateAsync(databaseConnectionInfo);
            return databaseConnectionInfo.Id;
        }

        /// <summary>
        /// Update
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [Authorize(DbConnectionsManagementPermissions.DatabaseConnectionInfos.Update)]
        public virtual async Task UpdateAsync(Guid id, UpdateDatabaseConnectionInfoDto input)
        {
            await DatabaseConnectionInfoManager.UpdateAsync(
                id,
                input.Name,
                input.DatabaseProvider,
                input.ConnectionString);
        }

        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize(DbConnectionsManagementPermissions.DatabaseConnectionInfos.Delete)]
        public virtual async Task DeleteAsync(Guid id)
        {
            await DatabaseConnectionInfoRepository.DeleteAsync(id);
        }
    }
}
