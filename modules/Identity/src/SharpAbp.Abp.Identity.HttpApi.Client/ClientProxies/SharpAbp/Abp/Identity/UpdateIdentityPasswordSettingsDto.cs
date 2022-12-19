// This file is automatically generated by ABP framework to use MVC Controllers from CSharp
using SharpAbp.Abp.Identity;
using System;
using System.Collections.Generic;
using Volo.Abp.Application.Dtos;
using Volo.Abp.ObjectExtending;

// ReSharper disable once CheckNamespace
namespace SharpAbp.Abp.Identity;

public class UpdateIdentityPasswordSettingsDto
{
    public bool RequireDigit { get; set; }

    public bool RequireLowercase { get; set; }

    public bool RequireNonAlphanumeric { get; set; }

    public bool RequireUppercase { get; set; }

    public int RequiredLength { get; set; }

    public int RequiredUniqueChars { get; set; }
}
