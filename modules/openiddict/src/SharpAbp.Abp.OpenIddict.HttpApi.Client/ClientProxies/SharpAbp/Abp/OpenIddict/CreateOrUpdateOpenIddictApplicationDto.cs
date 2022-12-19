// This file is automatically generated by ABP framework to use MVC Controllers from CSharp
using SharpAbp.Abp.OpenIddict;
using System;
using System.Collections.Generic;
using System.Text.Json;
using Volo.Abp.Application.Dtos;
using Volo.Abp.ObjectExtending;

// ReSharper disable once CheckNamespace
namespace SharpAbp.Abp.OpenIddict;

public class CreateOrUpdateOpenIddictApplicationDto : ExtensibleObject
{
    public string ClientId { get; set; }

    public string Type { get; set; }

    public string ConsentType { get; set; }

    public string DisplayName { get; set; }

    public string ClientSecret { get; set; }

    public string ClientUri { get; set; }

    public string LogoUri { get; set; }

    public Dictionary<string, JsonElement> Properties { get; set; }

    public Dictionary<string, string> DisplayNames { get; set; }

    public string[] RedirectUris { get; set; }

    public string[] PostLogoutRedirectUris { get; set; }

    public string[] Requirements { get; set; }

    public string[] GrantTypes { get; set; }

    public string[] Scopes { get; set; }

    public string[] Permissions { get; set; }
}
