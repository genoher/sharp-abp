﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace SharpAbp.Abp.IdentityServer.Clients
{
    public interface IClientAppService : IApplicationService
    {
        /// <summary>
        /// Get by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<ClientDto> GetAsync(Guid id);

        /// <summary>
        /// Find by clientId
        /// </summary>
        /// <param name="clientId"></param>
        /// <returns></returns>
        Task<ClientDto> FindByClientIdAsync(string clientId);

        /// <summary>
        /// Client paged list
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<PagedResultDto<ClientDto>> GetPagedListAsync(ClientPagedRequestDto input);

        /// <summary>
        /// Get all distinct allowed cors origins
        /// </summary>
        /// <returns></returns>
        Task<List<string>> GetAllDistinctAllowedCorsOriginsAsync();
    
    }
}
