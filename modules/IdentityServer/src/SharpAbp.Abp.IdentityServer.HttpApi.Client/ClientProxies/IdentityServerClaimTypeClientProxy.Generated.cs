// This file is automatically generated by ABP framework to use MVC Controllers from CSharp
using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Http.Client;
using Volo.Abp.Http.Modeling;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Http.Client.ClientProxying;
using SharpAbp.Abp.IdentityServer;
using System.Collections.Generic;
using SharpAbp.Abp.Identity;

// ReSharper disable once CheckNamespace
namespace SharpAbp.Abp.IdentityServer.ClientProxies;

[Dependency(ReplaceServices = true)]
[ExposeServices(typeof(IIdentityServerClaimTypeAppService), typeof(IdentityServerClaimTypeClientProxy))]
public partial class IdentityServerClaimTypeClientProxy : ClientProxyBase<IIdentityServerClaimTypeAppService>, IIdentityServerClaimTypeAppService
{
    public virtual async Task<List<IdentityClaimTypeDto>> GetAllAsync()
    {
        return await RequestAsync<List<IdentityClaimTypeDto>>(nameof(GetAllAsync));
    }
}
