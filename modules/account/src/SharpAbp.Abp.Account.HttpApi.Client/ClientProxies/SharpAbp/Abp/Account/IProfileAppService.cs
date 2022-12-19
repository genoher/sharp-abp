// This file is automatically generated by ABP framework to use MVC Controllers from CSharp
using SharpAbp.Abp.Account;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Account;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

// ReSharper disable once CheckNamespace
namespace SharpAbp.Abp.Account;

public interface IProfileAppService : IApplicationService
{
    Task<ProfileDto> GetAsync();

    Task<ProfileDto> UpdateAsync(UpdateProfileDto input);

    Task ChangePasswordAsync(ChangePasswordInput input);
}
