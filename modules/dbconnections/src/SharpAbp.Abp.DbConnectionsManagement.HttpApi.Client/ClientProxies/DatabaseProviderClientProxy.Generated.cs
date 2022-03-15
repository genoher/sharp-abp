// This file is automatically generated by ABP framework to use MVC Controllers from CSharp
using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Http.Client;
using Volo.Abp.Http.Modeling;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Http.Client.ClientProxying;
using SharpAbp.Abp.DbConnectionsManagement;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace SharpAbp.Abp.DbConnectionsManagement.ClientProxies;

[Dependency(ReplaceServices = true)]
[ExposeServices(typeof(IDatabaseProviderAppService), typeof(DatabaseProviderClientProxy))]
public partial class DatabaseProviderClientProxy : ClientProxyBase<IDatabaseProviderAppService>, IDatabaseProviderAppService
{
    public virtual async Task<List<String>> GetAllAsync()
    {
        return await RequestAsync<List<String>>(nameof(GetAllAsync));
    }
}
