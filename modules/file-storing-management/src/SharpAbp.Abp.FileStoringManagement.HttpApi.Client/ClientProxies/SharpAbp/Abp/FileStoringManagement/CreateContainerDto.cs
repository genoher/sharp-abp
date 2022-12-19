// This file is automatically generated by ABP framework to use MVC Controllers from CSharp
using SharpAbp.Abp.FileStoringManagement;
using System;
using System.Collections.Generic;
using Volo.Abp.Application.Dtos;
using Volo.Abp.ObjectExtending;

// ReSharper disable once CheckNamespace
namespace SharpAbp.Abp.FileStoringManagement;

public class CreateContainerDto
{
    public bool IsMultiTenant { get; set; }

    public string Title { get; set; }

    public string Name { get; set; }

    public string Provider { get; set; }

    public bool EnableAutoMultiPartUpload { get; set; }

    public int MultiPartUploadMinFileSize { get; set; }

    public int MultiPartUploadShardingSize { get; set; }

    public bool HttpAccess { get; set; }

    public CreateContainerItemDto[] Items { get; set; }
}
