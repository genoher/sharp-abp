﻿using SharpAbp.Abp.FileStoring.Aws.Localization;
using Volo.Abp.Caching;
using Volo.Abp.Localization;
using Volo.Abp.Localization.ExceptionHandling;
using Volo.Abp.Modularity;
using Volo.Abp.Validation.Localization;
using Volo.Abp.VirtualFileSystem;

namespace SharpAbp.Abp.FileStoring.Aws
{
    [DependsOn(
        typeof(AbpFileStoringModule),
        typeof(AbpCachingModule)
        )]
    public class AbpFileStoringAwsModule : AbpModule
    {
        public override void PreConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpFileStoringOptions>(options =>
            {
                var configuration = GetFileProviderConfiguration();
                options.Providers.TryAdd(configuration);
            });
        }


        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpVirtualFileSystemOptions>(options =>
            {
                options.FileSets.AddEmbedded<AbpFileStoringAwsModule>();
            });

            Configure<AbpLocalizationOptions>(options =>
            {
                options.Resources
                    .Add<FileStoringAwsResource>("en")
                    .AddBaseTypes(typeof(AbpValidationResource))
                    .AddVirtualJson("/SharpAbp/Abp/FileStoring/Aws/Localization/Resources");
            });

            Configure<AbpExceptionLocalizationOptions>(options =>
            {
                options.MapCodeNamespace("FileStoringAws", typeof(FileStoringAwsResource));
            });
        }

        private FileProviderConfiguration GetFileProviderConfiguration()
        {
            var configuration = new FileProviderConfiguration(
                AwsFileProviderConfigurationNames.ProviderName,
                typeof(FileStoringAwsResource));

            configuration.DefaultNamingNormalizers.TryAdd<AwsFileNamingNormalizer>();
            configuration
                .SetValueType(AwsFileProviderConfigurationNames.AccessKeyId, typeof(string))
                .SetValueType(AwsFileProviderConfigurationNames.SecretAccessKey, typeof(string))
                .SetValueType(AwsFileProviderConfigurationNames.UseCredentials, typeof(bool))
                .SetValueType(AwsFileProviderConfigurationNames.UseTemporaryCredentials, typeof(bool))
                .SetValueType(AwsFileProviderConfigurationNames.UseTemporaryFederatedCredentials, typeof(bool))
                .SetValueType(AwsFileProviderConfigurationNames.ProfileName, typeof(string))
                .SetValueType(AwsFileProviderConfigurationNames.ProfilesLocation, typeof(string))
                .SetValueType(AwsFileProviderConfigurationNames.DurationSeconds, typeof(int))
                .SetValueType(AwsFileProviderConfigurationNames.Name, typeof(string))
                .SetValueType(AwsFileProviderConfigurationNames.Policy, typeof(string))
                .SetValueType(AwsFileProviderConfigurationNames.Region, typeof(string))
                .SetValueType(AwsFileProviderConfigurationNames.ContainerName, typeof(string))
                .SetValueType(AwsFileProviderConfigurationNames.CreateContainerIfNotExists, typeof(bool))
                .SetValueType(AwsFileProviderConfigurationNames.TemporaryCredentialsCacheKey, typeof(string));

            return configuration;
        }
    }
}
