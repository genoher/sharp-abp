﻿using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Modularity;
using Volo.Abp.Threading;

namespace SharpAbp.Abp.MassTransit.RabbitMQ
{
    [DependsOn(
        typeof(AbpMassTransitModule)
        )]
    public class AbpMassTransitRabbitMqModule : AbpModule
    {
        public override void PreConfigureServices(ServiceConfigurationContext context)
        {
            AsyncHelper.RunSync(() => PreConfigureServicesAsync(context));
        }

        public override Task PreConfigureServicesAsync(ServiceConfigurationContext context)
        {
            var configuration = context.Services.GetConfiguration();
            var abpMassTransitOptions = configuration
                .GetSection("MassTransitOptions")
                .Get<AbpMassTransitOptions>();
            if (abpMassTransitOptions.Provider.Equals(MassTransitRabbitMqConsts.ProviderName, StringComparison.OrdinalIgnoreCase))
            {
                PreConfigure<AbpMassTransitRabbitMqOptions>(options => options.PreConfigure(configuration));

                var rabbitMqOptions = context.Services.ExecutePreConfiguredActions<AbpMassTransitRabbitMqOptions>();

                PreConfigure<AbpMassTransitRabbitMqOptions>(options =>
                {
                    options.DefaultExchangeNameFormatFunc = RabbitMqUtil.ExchangeNameFormat;
                    options.DefaultQueueNameFormatFunc = RabbitMqUtil.QueueNameFormat;

                    options.DefaultPublishTopologyConfigure = new Action<IRabbitMqMessagePublishTopologyConfigurator>(c =>
                    {
                        c.AutoDelete = rabbitMqOptions.DefaultAutoDelete;
                        c.Durable = rabbitMqOptions.DefaultDurable;
                        c.ExchangeType = rabbitMqOptions.DefaultExchangeType;
                    });

                    options.DefaultReceiveEndpointConfigure = new Action<string, string, IRabbitMqReceiveEndpointConfigurator>((exchangeName, queueName, c) =>
                    {
                        //c.Bind(exchangeName);
                        c.ConcurrentMessageLimit = rabbitMqOptions.DefaultConcurrentMessageLimit;
                        c.PrefetchCount = rabbitMqOptions.DefaultPrefetchCount;
                        c.AutoDelete = rabbitMqOptions.DefaultAutoDelete;
                        c.Durable = rabbitMqOptions.DefaultDurable;
                        c.ExchangeType = rabbitMqOptions.DefaultExchangeType;
                    });


                    // options.RabbitMqPostConfigures.Add(new Action<IBusRegistrationContext, IRabbitMqBusFactoryConfigurator>((ctx, cfg) =>
                    // {
                    //     cfg.ConfigureEndpoints(ctx);
                    // }));
                });
            }
            return Task.CompletedTask;
        }

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            AsyncHelper.RunSync(() => ConfigureServicesAsync(context));
        }

        public override Task ConfigureServicesAsync(ServiceConfigurationContext context)
        {
            var configuration = context.Services.GetConfiguration();
            var abpMassTransitOptions = configuration
                .GetSection("MassTransitOptions")
                .Get<AbpMassTransitOptions>();
            if (abpMassTransitOptions.Provider.Equals(MassTransitRabbitMqConsts.ProviderName, StringComparison.OrdinalIgnoreCase))
            {
                var massTransitOptions = context.Services.ExecutePreConfiguredActions<AbpMassTransitOptions>();
                var rabbitMqOptions = context.Services.ExecutePreConfiguredActions<AbpMassTransitRabbitMqOptions>();
                context.Services.AddMassTransit(x =>
                {
                    //Masstransit preConfigure
                    foreach (var preConfigure in massTransitOptions.PreConfigures)
                    {
                        preConfigure(x);
                    }

                    //Consumer
                    foreach (var consumer in rabbitMqOptions.Consumers)
                    {
                        consumer.Configure?.Invoke(x);
                    }

                    //Get message configures
                    var messageConfigues = rabbitMqOptions.GetMessageConfigures();

                    x.UsingRabbitMq((ctx, cfg) =>
                    {
                        //RabbitMq preConfigure
                        foreach (var preConfigure in rabbitMqOptions.RabbitMqPreConfigures)
                        {
                            preConfigure(ctx, cfg);
                        }

                        cfg.Host(rabbitMqOptions.Host, rabbitMqOptions.Port, rabbitMqOptions.VirtualHost, rabbitMqOptions.ConnectionName, h =>
                        {
                            h.Username(rabbitMqOptions.Username);
                            h.Password(rabbitMqOptions.Password);
                            //SSL
                            if (rabbitMqOptions.UseSSL && rabbitMqOptions.ConfigureSsl != null)
                            {
                                h.UseSsl(rabbitMqOptions.ConfigureSsl);
                            }

                            //Cluster
                            if (rabbitMqOptions.UseCluster && rabbitMqOptions.ClusterNodes.Any())
                            {
                                h.UseCluster(c =>
                                {
                                    foreach (var clusterNode in rabbitMqOptions.ClusterNodes)
                                    {
                                        c.Node(clusterNode);
                                    }
                                });
                            }
                        });

                        //RabbitMq configure
                        foreach (var configure in rabbitMqOptions.RabbitMqConfigures)
                        {
                            configure(ctx, cfg);
                        }

                        //Message configure
                        foreach (var messageConfigue in messageConfigues)
                        {
                            var entityName = rabbitMqOptions.DefaultExchangeNameFormatFunc(massTransitOptions.Prefix, messageConfigue.Item1);
                            messageConfigue.Item2?.Invoke(entityName, cfg);
                        }

                        //Producer
                        foreach (var producer in rabbitMqOptions.Producers)
                        {
                            var publishTopologyConfigure = producer.PublishTopologyConfigure ?? rabbitMqOptions.DefaultPublishTopologyConfigure;

                            producer.PublishConfigure?.Invoke(publishTopologyConfigure, ctx, cfg);
                        }

                        //Consumer
                        foreach (var consumer in rabbitMqOptions.Consumers)
                        {
                            var exchangeName = rabbitMqOptions.DefaultExchangeNameFormatFunc(massTransitOptions.Prefix, consumer.ExchangeName);

                            var queueName = rabbitMqOptions.DefaultQueueNameFormatFunc(massTransitOptions.Prefix, rabbitMqOptions.DefaultQueuePrefix, consumer.QueueName);

                            var receiveEndpointConfigure = consumer.ReceiveEndpointConfigure ?? rabbitMqOptions.DefaultReceiveEndpointConfigure;

                            consumer.ReceiveEndpoint?.Invoke(exchangeName, queueName, receiveEndpointConfigure, ctx, cfg);
                        }

                        //RabbitMq postConfigure
                        foreach (var postConfigure in rabbitMqOptions.RabbitMqPostConfigures)
                        {
                            postConfigure(ctx, cfg);
                        }
                    });
                });
            }

            return Task.CompletedTask;
        }

        public override void PostConfigureServices(ServiceConfigurationContext context)
        {
            AsyncHelper.RunSync(() => PostConfigureServicesAsync(context));
        }

        public override Task PostConfigureServicesAsync(ServiceConfigurationContext context)
        {
            var configuration = context.Services.GetConfiguration();
            var abpMassTransitOptions = configuration
                .GetSection("MassTransitOptions")
                .Get<AbpMassTransitOptions>();
            if (abpMassTransitOptions.Provider.Equals(MassTransitRabbitMqConsts.ProviderName, StringComparison.OrdinalIgnoreCase))
            {
                Configure<AbpMassTransitRabbitMqOptions>(options =>
                {
                    var actions = context.Services.GetPreConfigureActions<AbpMassTransitRabbitMqOptions>();
                    foreach (var action in actions)
                    {
                        action(options);
                    }
                });
            }
            return Task.CompletedTask;
        }
    }
}
