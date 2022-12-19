// This file is automatically generated by ABP framework to use MVC Controllers from CSharp
using SharpAbp.Abp.OpenIddict;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Http.Client;
using Volo.Abp.Http.Client.ClientProxying;
using Volo.Abp.Http.Modeling;

// ReSharper disable once CheckNamespace
namespace SharpAbp.Abp.OpenIddict;

[Dependency(ReplaceServices = true)]
[ExposeServices(typeof(IOpenIddictApplicationAppService), typeof(OpenIddictApplicationClientProxy))]
public partial class OpenIddictApplicationClientProxy : ClientProxyBase<IOpenIddictApplicationAppService>, IOpenIddictApplicationAppService
{
    public virtual async Task<OpenIddictApplicationDto> GetAsync(Guid id)
    {
        return await RequestAsync<OpenIddictApplicationDto>(nameof(GetAsync), new ClientProxyRequestTypeValue
        {
            { typeof(Guid), id }
        });
    }

    public virtual async Task<List<OpenIddictApplicationDto>> GetListAsync()
    {
        return await RequestAsync<List<OpenIddictApplicationDto>>(nameof(GetListAsync));
    }

    public virtual async Task<PagedResultDto<OpenIddictApplicationDto>> GetPagedListAsync(PagedAndSortedResultRequestDto input)
    {
        return await RequestAsync<PagedResultDto<OpenIddictApplicationDto>>(nameof(GetPagedListAsync), new ClientProxyRequestTypeValue
        {
            { typeof(PagedAndSortedResultRequestDto), input }
        });
    }

    public virtual async Task<OpenIddictApplicationDto> FindByClientIdAsync(string clientId)
    {
        return await RequestAsync<OpenIddictApplicationDto>(nameof(FindByClientIdAsync), new ClientProxyRequestTypeValue
        {
            { typeof(string), clientId }
        });
    }

    public virtual async Task<OpenIddictApplicationDto> CreateAsync(CreateOrUpdateOpenIddictApplicationDto input)
    {
        return await RequestAsync<OpenIddictApplicationDto>(nameof(CreateAsync), new ClientProxyRequestTypeValue
        {
            { typeof(CreateOrUpdateOpenIddictApplicationDto), input }
        });
    }

    public virtual async Task<OpenIddictApplicationDto> UpdateAsync(Guid id, CreateOrUpdateOpenIddictApplicationDto input)
    {
        return await RequestAsync<OpenIddictApplicationDto>(nameof(UpdateAsync), new ClientProxyRequestTypeValue
        {
            { typeof(Guid), id },
            { typeof(CreateOrUpdateOpenIddictApplicationDto), input }
        });
    }

    public virtual async Task DeleteAsync(Guid id)
    {
        await RequestAsync(nameof(DeleteAsync), new ClientProxyRequestTypeValue
        {
            { typeof(Guid), id }
        });
    }
}
