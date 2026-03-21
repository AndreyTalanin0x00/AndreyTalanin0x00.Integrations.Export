using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;

using AndreyTalanin0x00.Extensions.DependencyInjection;
using AndreyTalanin0x00.Integrations.Export.DependencyInjection;
using AndreyTalanin0x00.Integrations.Export.Options;
using AndreyTalanin0x00.Integrations.Export.Options.Builders;
using AndreyTalanin0x00.Integrations.Export.Options.Builders.Abstractions;
using AndreyTalanin0x00.Integrations.Export.Requests;
using AndreyTalanin0x00.Integrations.Export.Responses;
using AndreyTalanin0x00.Integrations.Export.Services;
using AndreyTalanin0x00.Integrations.Export.Services.Abstractions;

using Microsoft.Extensions.DependencyInjection;

using ObjectExportIntermediateObjectPackage = System.Object;

// Disable the IDE0001 (Simplify name) notification to preserve explicit types.
#pragma warning disable IDE0001

namespace AndreyTalanin0x00.Integrations.Export.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddExportPipeline<TExportRequest, TExportResponse, TExportIntermediateObjectPackage, TExportObjectPackage>(this IServiceCollection services, Action<IExportPipelineOptionsBuilder<TExportRequest, TExportResponse, TExportIntermediateObjectPackage, TExportObjectPackage>> configureAction)
        where TExportRequest : ExportRequest
        where TExportResponse : ExportResponse
        where TExportIntermediateObjectPackage : class
        where TExportObjectPackage : class
    {
        services.AddExportPipelineCore<TExportRequest, TExportResponse, TExportIntermediateObjectPackage, TExportObjectPackage>(configureAction);

        return services;
    }

    public static IServiceCollection AddExportPipeline<TExportRequest, TExportResponse, TExportObjectPackage>(this IServiceCollection services, Action<IExportPipelineOptionsBuilder<TExportRequest, TExportResponse, ObjectExportIntermediateObjectPackage, TExportObjectPackage>> configureAction)
        where TExportRequest : ExportRequest
        where TExportResponse : ExportResponse
        where TExportObjectPackage : class
    {
        services.AddExportPipelineCore<TExportRequest, TExportResponse, ObjectExportIntermediateObjectPackage, TExportObjectPackage>(configureAction);

        return services;
    }

    public static IServiceCollection AddCustomExportPipeline<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] TExportPipeline, TExportRequest, TExportResponse, TExportIntermediateObjectPackage, TExportObjectPackage>(this IServiceCollection services, Action<IExportPipelineOptionsBuilder<TExportPipeline, TExportRequest, TExportResponse, TExportIntermediateObjectPackage, TExportObjectPackage>> configureAction)
        where TExportPipeline : class, IExportPipeline<TExportRequest, TExportResponse>
        where TExportRequest : ExportRequest
        where TExportResponse : ExportResponse
        where TExportIntermediateObjectPackage : class
        where TExportObjectPackage : class
    {
        services.AddCustomExportPipelineCore<TExportPipeline, TExportRequest, TExportResponse, TExportIntermediateObjectPackage, TExportObjectPackage>(null, configureAction);

        return services;
    }

    public static IServiceCollection AddCustomExportPipeline<TExportPipeline, TExportRequest, TExportResponse, TExportIntermediateObjectPackage, TExportObjectPackage>(this IServiceCollection services, ServiceImplementationFactory<TExportPipeline> exportPipelineImplementationFactory, Action<IExportPipelineOptionsBuilder<TExportPipeline, TExportRequest, TExportResponse, TExportIntermediateObjectPackage, TExportObjectPackage>> configureAction)
        where TExportPipeline : class, IExportPipeline<TExportRequest, TExportResponse>
        where TExportRequest : ExportRequest
        where TExportResponse : ExportResponse
        where TExportIntermediateObjectPackage : class
        where TExportObjectPackage : class
    {
        services.AddCustomExportPipelineCore<TExportPipeline, TExportRequest, TExportResponse, TExportIntermediateObjectPackage, TExportObjectPackage>(exportPipelineImplementationFactory, configureAction);

        return services;
    }

    public static IServiceCollection AddCustomExportPipeline<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] TExportPipeline, TExportRequest, TExportResponse, TExportObjectPackage>(this IServiceCollection services, Action<IExportPipelineOptionsBuilder<TExportPipeline, TExportRequest, TExportResponse, ObjectExportIntermediateObjectPackage, TExportObjectPackage>> configureAction)
        where TExportPipeline : class, IExportPipeline<TExportRequest, TExportResponse>
        where TExportRequest : ExportRequest
        where TExportResponse : ExportResponse
        where TExportObjectPackage : class
    {
        services.AddCustomExportPipelineCore<TExportPipeline, TExportRequest, TExportResponse, ObjectExportIntermediateObjectPackage, TExportObjectPackage>(null, configureAction);

        return services;
    }

    public static IServiceCollection AddCustomExportPipeline<TExportPipeline, TExportRequest, TExportResponse, TExportObjectPackage>(this IServiceCollection services, ServiceImplementationFactory<TExportPipeline> exportPipelineImplementationFactory, Action<IExportPipelineOptionsBuilder<TExportPipeline, TExportRequest, TExportResponse, ObjectExportIntermediateObjectPackage, TExportObjectPackage>> configureAction)
        where TExportPipeline : class, IExportPipeline<TExportRequest, TExportResponse>
        where TExportRequest : ExportRequest
        where TExportResponse : ExportResponse
        where TExportObjectPackage : class
    {
        services.AddCustomExportPipelineCore<TExportPipeline, TExportRequest, TExportResponse, ObjectExportIntermediateObjectPackage, TExportObjectPackage>(exportPipelineImplementationFactory, configureAction);

        return services;
    }

    private static IServiceCollection AddExportPipelineCore<TExportRequest, TExportResponse, TExportIntermediateObjectPackage, TExportObjectPackage>(this IServiceCollection services, Action<IExportPipelineOptionsBuilder<TExportRequest, TExportResponse, TExportIntermediateObjectPackage, TExportObjectPackage>>? configureAction)
        where TExportRequest : ExportRequest
        where TExportResponse : ExportResponse
        where TExportIntermediateObjectPackage : class
        where TExportObjectPackage : class
    {
        ExportPipelineOptions<TExportRequest, TExportResponse, TExportIntermediateObjectPackage, TExportObjectPackage> exportPipelineOptions = new();
        ExportPipelineOptionsBuilder<TExportRequest, TExportResponse, TExportIntermediateObjectPackage, TExportObjectPackage> exportPipelineOptionsBuilder = new(exportPipelineOptions);

        if (configureAction is not null)
            configureAction(exportPipelineOptionsBuilder);

        exportPipelineOptions = exportPipelineOptionsBuilder.Build();

        ConfigureExportPipelineServices(services, exportPipelineOptions);

        services.AddTransient<IExportPipeline<TExportRequest, TExportResponse>, ExportPipeline<TExportRequest, TExportResponse, TExportIntermediateObjectPackage, TExportObjectPackage>>();

        return services;
    }

    private static IServiceCollection AddCustomExportPipelineCore<TExportPipeline, TExportRequest, TExportResponse, TExportIntermediateObjectPackage, TExportObjectPackage>(this IServiceCollection services, ServiceImplementationFactory<TExportPipeline>? exportPipelineImplementationFactory, Action<IExportPipelineOptionsBuilder<TExportPipeline, TExportRequest, TExportResponse, TExportIntermediateObjectPackage, TExportObjectPackage>>? configureAction)
        where TExportPipeline : class, IExportPipeline<TExportRequest, TExportResponse>
        where TExportRequest : ExportRequest
        where TExportResponse : ExportResponse
        where TExportIntermediateObjectPackage : class
        where TExportObjectPackage : class
    {
        ExportPipelineOptions<TExportPipeline, TExportRequest, TExportResponse, TExportIntermediateObjectPackage, TExportObjectPackage> exportPipelineOptions = new();
        ExportPipelineOptionsBuilder<TExportPipeline, TExportRequest, TExportResponse, TExportIntermediateObjectPackage, TExportObjectPackage> exportPipelineOptionsBuilder = new(exportPipelineOptions);

        if (configureAction is not null)
            configureAction(exportPipelineOptionsBuilder);

        exportPipelineOptions = exportPipelineOptionsBuilder.Build();

        ConfigureExportPipelineServices(services, exportPipelineOptions);
        ConfigureCustomExportPipelineServices(services, exportPipelineOptions);

        if (exportPipelineImplementationFactory is not null)
            services.AddTransient<IExportPipeline<TExportRequest, TExportResponse>, TExportPipeline>(serviceProvider => exportPipelineImplementationFactory(serviceProvider));
        else
            services.AddTransient<IExportPipeline<TExportRequest, TExportResponse>, TExportPipeline>();

        return services;
    }

    private static void ConfigureExportPipelineServices<TExportRequest, TExportResponse, TExportIntermediateObjectPackage, TExportObjectPackage>(IServiceCollection services, ExportPipelineOptions<TExportRequest, TExportResponse, TExportIntermediateObjectPackage, TExportObjectPackage> exportPipelineOptions)
        where TExportRequest : ExportRequest
        where TExportResponse : ExportResponse
        where TExportIntermediateObjectPackage : class
        where TExportObjectPackage : class
    {
        const string exportPipelineOptionsClassName =
            nameof(ExportPipelineOptions<TExportRequest, TExportResponse, TExportIntermediateObjectPackage, TExportObjectPackage>);

        const string addExportWriterServiceCollectionVisitorPropertyName =
            nameof(ExportPipelineOptions<TExportRequest, TExportResponse, TExportIntermediateObjectPackage, TExportObjectPackage>.AddExportWriterServiceCollectionVisitor);
        const string addExportProcessorServiceCollectionVisitorPropertyName =
            nameof(ExportPipelineOptions<TExportRequest, TExportResponse, TExportIntermediateObjectPackage, TExportObjectPackage>.AddExportProcessorServiceCollectionVisitor);
        const string addExportPipelineChannelKeyResolverServiceCollectionVisitorPropertyName =
            nameof(ExportPipelineOptions<TExportRequest, TExportResponse, TExportIntermediateObjectPackage, TExportObjectPackage>.AddExportPipelineChannelKeyResolverServiceCollectionVisitor);
        const string exportPipelineChannelServiceDescriptorsPropertyName =
            nameof(ExportPipelineOptions<TExportRequest, TExportResponse, TExportIntermediateObjectPackage, TExportObjectPackage>.ExportPipelineChannelServiceDescriptors);

        const string buildMethodName =
            nameof(IExportPipelineOptionsBuilderInternal<TExportRequest, TExportResponse, TExportIntermediateObjectPackage, TExportObjectPackage>.Build);

        Action<IServiceCollection> addExportWriterServiceCollectionVisitor = exportPipelineOptions.AddExportWriterServiceCollectionVisitor
            ?? throw new UnreachableException($"An {exportPipelineOptionsClassName} instance has its {addExportWriterServiceCollectionVisitorPropertyName} property set to null after the {buildMethodName} method has been called.");
        Action<IServiceCollection> addExportProcessorServiceCollectionVisitor = exportPipelineOptions.AddExportProcessorServiceCollectionVisitor
            ?? throw new UnreachableException($"An {exportPipelineOptionsClassName} instance has its {addExportProcessorServiceCollectionVisitorPropertyName} property set to null after the {buildMethodName} method has been called.");
        Action<IServiceCollection> addExportPipelineChannelKeyResolverServiceCollectionVisitor = exportPipelineOptions.AddExportPipelineChannelKeyResolverServiceCollectionVisitor
            ?? throw new UnreachableException($"An {exportPipelineOptionsClassName} instance has its {addExportPipelineChannelKeyResolverServiceCollectionVisitorPropertyName} property set to null after the {buildMethodName} method has been called.");

        if (exportPipelineOptions.ExportPipelineChannelServiceDescriptors.Count == 0)
            throw new UnreachableException($"An {exportPipelineOptionsClassName} instance has its {exportPipelineChannelServiceDescriptorsPropertyName} property empty after the {buildMethodName} method has been called.");

        addExportWriterServiceCollectionVisitor(services);
        addExportProcessorServiceCollectionVisitor(services);
        addExportPipelineChannelKeyResolverServiceCollectionVisitor(services);

        foreach (ExportPipelineChannelServiceDescriptor exportPipelineChannelServiceDescriptor in exportPipelineOptions.ExportPipelineChannelServiceDescriptors)
        {
            Action<IServiceCollection> addExportPipelineChannelServiceCollectionVisitor =
                exportPipelineChannelServiceDescriptor.AddExportPipelineChannelServiceCollectionVisitor;

            addExportPipelineChannelServiceCollectionVisitor(services);
        }
    }

    private static void ConfigureCustomExportPipelineServices<TExportPipeline, TExportRequest, TExportResponse, TExportIntermediateObjectPackage, TExportObjectPackage>(IServiceCollection services, ExportPipelineOptions<TExportPipeline, TExportRequest, TExportResponse, TExportIntermediateObjectPackage, TExportObjectPackage> exportPipelineOptions)
        where TExportPipeline : class, IExportPipeline<TExportRequest, TExportResponse>
        where TExportRequest : ExportRequest
        where TExportResponse : ExportResponse
        where TExportIntermediateObjectPackage : class
        where TExportObjectPackage : class
    {
    }
}
