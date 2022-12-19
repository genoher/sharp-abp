// This file is automatically generated by ABP framework to use MVC Controllers from CSharp
using SharpAbp.Abp.Identity;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Identity;

// ReSharper disable once CheckNamespace
namespace SharpAbp.Abp.Identity;

public interface IOrganizationUnitAppService : IApplicationService
{
    Task<OrganizationUnitDto> GetAsync(Guid id);

    Task<List<OrganizationUnitDto>> GetAllAsync();

    Task<PagedResultDto<OrganizationUnitDto>> GetPagedListAsync(OrganizationUnitPagedRequestDto input);

    Task<OrganizationUnitDto> FindByDisplayNameAsync(string displayName);

    Task<List<OrganizationUnitDto>> GetChildrenAsync(Guid? parentId);

    Task<List<OrganizationUnitDto>> GetAllChildrenWithParentCodeAsync(string code, Guid? parentId);

    Task<List<OrganizationUnitDto>> GetListAsync(List<Guid> ids);

    Task<PagedResultDto<IdentityRoleDto>> GetRolesPagedListAsync(Guid id, OrganizationUnitRolePagedRequestDto input);

    Task<PagedResultDto<IdentityRoleDto>> GetUnaddedRolesPagedListAsync(Guid id, OrganizationUnitUnaddedRolePagedRequestDto input);

    Task<PagedResultDto<IdentityUserDto>> GetMembersPagedListAsync(Guid id, OrganizationUnitMemberPagedRequestDto input);

    Task<PagedResultDto<IdentityUserDto>> GetUnaddedMembersPagedListAsync(Guid id, OrganizationUnitUnaddedMemberPagedRequestDto input);

    Task<Guid> CreateAsync(CreateOrganizationUnitDto input);

    Task UpdateAsync(Guid id, UpdateOrganizationUnitDto input);

    Task DeleteAsync(Guid id);

    Task MoveAsync(Guid id, MoveOrganizationUnitDto input);

    Task AddRoleToOrganizationUnitAsync(Guid id, AddRoleToOrganizationUnitDto input);

    Task RemoveRoleFromOrganizationUnitAsync(Guid id, Guid roleId);

    Task AddMemberToOrganizationUnitAsync(Guid id, AddMemberToOrganizationUnitDto input);

    Task RemoveMemberFromOrganizationUnitAsync(Guid id, Guid userId);
}
