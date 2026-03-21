using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

using AndreyTalanin0x00.Extensions.DependencyInjection;
using AndreyTalanin0x00.Integrations.Export.DependencyInjection;
using AndreyTalanin0x00.Integrations.Export.Options.Builders.Abstractions;
using AndreyTalanin0x00.Integrations.Export.Requests;
using AndreyTalanin0x00.Integrations.Export.Responses;
using AndreyTalanin0x00.Integrations.Export.Services;
using AndreyTalanin0x00.Integrations.Export.Services.Abstractions;

using Microsoft.Extensions.DependencyInjection;

// Disable the IDE0001 (Simplify name) notification to preserve explicit types.
#pragma warning disable IDE0001

namespace AndreyTalanin0x00.Integrations.Export.Options.Builders;

internal class ExportPipelineOptionsBuilder<TExportRequest, TExportResponse, TExportIntermediateObjectPackage, TExportObjectPackage> :
    IExportPipelineOptionsBuilderInternal<TExportRequest, TExportResponse, TExportIntermediateObjectPackage, TExportObjectPackage>
    where TExportRequest : ExportRequest
    where TExportResponse : ExportResponse
    where TExportIntermediateObjectPackage : class
    where TExportObjectPackage : class
{
    private readonly ExportPipelineOptions<TExportRequest, TExportResponse, TExportIntermediateObjectPackage, TExportObjectPackage> m_exportPipelineOptions;

    public ExportPipelineOptionsBuilder(ExportPipelineOptions<TExportRequest, TExportResponse, TExportIntermediateObjectPackage, TExportObjectPackage> exportPipelineOptions)
    {
        m_exportPipelineOptions = exportPipelineOptions;
    }

    /// <inheritdoc />
    public IExportPipelineOptionsBuilder<TExportRequest, TExportResponse, TExportIntermediateObjectPackage, TExportObjectPackage> UseExportWriter<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] TExportWriter>()
        where TExportWriter : class, IExportWriter<TExportRequest, TExportResponse>
    {
        m_exportPipelineOptions.AddExportWriterServiceCollectionVisitor = AddExportWriter;

        static void AddExportWriter(IServiceCollection services) =>
            services.AddTransient<IExportWriter<TExportRequest, TExportResponse>, TExportWriter>();

        return this;
    }

    /// <inheritdoc />
    public IExportPipelineOptionsBuilder<TExportRequest, TExportResponse, TExportIntermediateObjectPackage, TExportObjectPackage> UseExportWriter<TExportWriter>(ServiceImplementationFactory<TExportWriter> exportWriterImplementationFactory)
        where TExportWriter : class, IExportWriter<TExportRequest, TExportResponse>
    {
        m_exportPipelineOptions.AddExportWriterServiceCollectionVisitor = AddExportWriter;

        void AddExportWriter(IServiceCollection services) =>
            services.AddTransient<IExportWriter<TExportRequest, TExportResponse>, TExportWriter>(serviceProvider => exportWriterImplementationFactory(serviceProvider));

        return this;
    }

    /// <inheritdoc />
    public IExportPipelineOptionsBuilder<TExportRequest, TExportResponse, TExportIntermediateObjectPackage, TExportObjectPackage> UseExportProcessor<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] TExportProcessor>()
        where TExportProcessor : class, IExportProcessor<TExportRequest, TExportResponse, TExportIntermediateObjectPackage, TExportObjectPackage>
    {
        m_exportPipelineOptions.AddExportProcessorServiceCollectionVisitor = AddExportProcessor;

        static void AddExportProcessor(IServiceCollection services) =>
            services.AddTransient<IExportProcessor<TExportRequest, TExportResponse, TExportIntermediateObjectPackage, TExportObjectPackage>, TExportProcessor>();

        return this;
    }

    /// <inheritdoc />
    public IExportPipelineOptionsBuilder<TExportRequest, TExportResponse, TExportIntermediateObjectPackage, TExportObjectPackage> UseExportProcessor<TExportProcessor>(ServiceImplementationFactory<TExportProcessor> exportProcessorImplementationFactory)
        where TExportProcessor : class, IExportProcessor<TExportRequest, TExportResponse, TExportIntermediateObjectPackage, TExportObjectPackage>
    {
        m_exportPipelineOptions.AddExportProcessorServiceCollectionVisitor = AddExportProcessor;

        void AddExportProcessor(IServiceCollection services) =>
            services.AddTransient<IExportProcessor<TExportRequest, TExportResponse, TExportIntermediateObjectPackage, TExportObjectPackage>, TExportProcessor>(serviceProvider => exportProcessorImplementationFactory(serviceProvider));

        return this;
    }

    /// <inheritdoc />
    public IExportPipelineOptionsBuilder<TExportRequest, TExportResponse, TExportIntermediateObjectPackage, TExportObjectPackage> UseExportPipelineChannelKeyResolver<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] TExportPipelineChannelKeyResolver>()
        where TExportPipelineChannelKeyResolver : class, IExportPipelineChannelKeyResolver<TExportRequest, TExportResponse>
    {
        m_exportPipelineOptions.AddExportPipelineChannelKeyResolverServiceCollectionVisitor = AddExportPipelineChannelKeyResolver;

        static void AddExportPipelineChannelKeyResolver(IServiceCollection services) =>
            services.AddTransient<IExportPipelineChannelKeyResolver<TExportRequest, TExportResponse>, TExportPipelineChannelKeyResolver>();

        return this;
    }

    /// <inheritdoc />
    public IExportPipelineOptionsBuilder<TExportRequest, TExportResponse, TExportIntermediateObjectPackage, TExportObjectPackage> UseExportPipelineChannelKeyResolver<TExportPipelineChannelKeyResolver>(ServiceImplementationFactory<TExportPipelineChannelKeyResolver> exportPipelineChannelKeyResolverImplementationFactory)
        where TExportPipelineChannelKeyResolver : class, IExportPipelineChannelKeyResolver<TExportRequest, TExportResponse>
    {
        m_exportPipelineOptions.AddExportPipelineChannelKeyResolverServiceCollectionVisitor = AddExportPipelineChannelKeyResolver;

        void AddExportPipelineChannelKeyResolver(IServiceCollection services) =>
            services.AddTransient<IExportPipelineChannelKeyResolver<TExportRequest, TExportResponse>, TExportPipelineChannelKeyResolver>(serviceProvider => exportPipelineChannelKeyResolverImplementationFactory(serviceProvider));

        return this;
    }

    /// <inheritdoc />
    public IExportPipelineOptionsBuilder<TExportRequest, TExportResponse, TExportIntermediateObjectPackage, TExportObjectPackage> AddExportPipelineChannel<TExportIntermediateObjectPackageCurrent>(Action<IExportPipelineChannelOptionsBuilder<TExportRequest, TExportResponse, TExportIntermediateObjectPackage, TExportIntermediateObjectPackageCurrent, TExportObjectPackage>> configureAction)
        where TExportIntermediateObjectPackageCurrent : class, TExportIntermediateObjectPackage
    {
        AddCustomExportPipelineChannelCore<ExportPipelineChannel<TExportRequest, TExportResponse, TExportIntermediateObjectPackage, TExportIntermediateObjectPackageCurrent, TExportObjectPackage>, TExportIntermediateObjectPackageCurrent>(configureAction: configureAction);

        return this;
    }

    /// <inheritdoc />
    public IExportPipelineOptionsBuilder<TExportRequest, TExportResponse, TExportIntermediateObjectPackage, TExportObjectPackage> AddCustomExportPipelineChannel<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] TExportPipelineChannel, TExportIntermediateObjectPackageCurrent>(Action<IExportPipelineChannelOptionsBuilder<TExportPipelineChannel, TExportRequest, TExportResponse, TExportIntermediateObjectPackage, TExportIntermediateObjectPackageCurrent, TExportObjectPackage>> configureAction)
        where TExportPipelineChannel : class, IExportPipelineChannel<TExportRequest, TExportResponse, TExportIntermediateObjectPackage, TExportObjectPackage>
        where TExportIntermediateObjectPackageCurrent : class, TExportIntermediateObjectPackage
    {
        AddCustomExportPipelineChannelCore<TExportPipelineChannel, TExportIntermediateObjectPackageCurrent>(null, configureAction);

        return this;
    }

    /// <inheritdoc />
    public IExportPipelineOptionsBuilder<TExportRequest, TExportResponse, TExportIntermediateObjectPackage, TExportObjectPackage> AddCustomExportPipelineChannel<TExportPipelineChannel, TExportIntermediateObjectPackageCurrent>(KeyedServiceImplementationFactory<TExportPipelineChannel, ExportPipelineChannelKey> exportPipelineChannelImplementationFactory, Action<IExportPipelineChannelOptionsBuilder<TExportPipelineChannel, TExportRequest, TExportResponse, TExportIntermediateObjectPackage, TExportIntermediateObjectPackageCurrent, TExportObjectPackage>> configureAction)
        where TExportPipelineChannel : class, IExportPipelineChannel<TExportRequest, TExportResponse, TExportIntermediateObjectPackage, TExportObjectPackage>
        where TExportIntermediateObjectPackageCurrent : class, TExportIntermediateObjectPackage
    {
        AddCustomExportPipelineChannelCore<TExportPipelineChannel, TExportIntermediateObjectPackageCurrent>(exportPipelineChannelImplementationFactory, configureAction);

        return this;
    }

    /// <inheritdoc />
    public ExportPipelineOptions<TExportRequest, TExportResponse, TExportIntermediateObjectPackage, TExportObjectPackage> Build()
    {
        Assert();

        return m_exportPipelineOptions;
    }

    protected virtual void Assert()
    {
        if (m_exportPipelineOptions.AddExportWriterServiceCollectionVisitor is null)
            throw new InvalidOperationException("The export pipeline's configuration is invalid: no export writer is specified.");
        if (m_exportPipelineOptions.AddExportProcessorServiceCollectionVisitor is null)
            throw new InvalidOperationException("The export pipeline's configuration is invalid: no export processor is specified.");
        if (m_exportPipelineOptions.AddExportPipelineChannelKeyResolverServiceCollectionVisitor is null)
            throw new InvalidOperationException("The export pipeline's configuration is invalid: no export pipeline channel key resolver is specified.");

        int exportPipelineChannelServiceDescriptorsCount = m_exportPipelineOptions.ExportPipelineChannelServiceDescriptors.Count;
        if (exportPipelineChannelServiceDescriptorsCount == 0)
            throw new UnreachableException("The export pipeline's configuration is invalid: no export pipeline channel is specified.");

        int exportPipelineChannelServiceDescriptorsKeylessCount = m_exportPipelineOptions.ExportPipelineChannelServiceDescriptors
            .Where(exportPipelineChannelServiceDescriptor => exportPipelineChannelServiceDescriptor.ExportPipelineChannelKey is null)
            .Count();
        if (exportPipelineChannelServiceDescriptorsKeylessCount > 1)
            throw new UnreachableException("The export pipeline's configuration is invalid: the export pipeline has multiple keyless export pipeline channels. Only a single one is allowed.");

        int exportPipelineChannelServiceDescriptorsUniqueCount = m_exportPipelineOptions.ExportPipelineChannelServiceDescriptors
            .Select(exportPipelineChannelServiceDescriptor => exportPipelineChannelServiceDescriptor.ExportPipelineChannelKey?.Value ?? string.Empty)
            .Distinct()
            .Count();
        if (exportPipelineChannelServiceDescriptorsUniqueCount != exportPipelineChannelServiceDescriptorsCount)
            throw new UnreachableException("The export pipeline's configuration is invalid: the export pipeline has non-unique export pipeline channels keys.");

        return;
    }

    private ExportPipelineOptionsBuilder<TExportRequest, TExportResponse, TExportIntermediateObjectPackage, TExportObjectPackage> AddCustomExportPipelineChannelCore<TExportPipelineChannel, TExportIntermediateObjectPackageCurrent>(KeyedServiceImplementationFactory<TExportPipelineChannel, ExportPipelineChannelKey>? exportPipelineChannelImplementationFactory = null, Action<IExportPipelineChannelOptionsBuilder<TExportPipelineChannel, TExportRequest, TExportResponse, TExportIntermediateObjectPackage, TExportIntermediateObjectPackageCurrent, TExportObjectPackage>>? configureAction = null)
        where TExportPipelineChannel : class, IExportPipelineChannel<TExportRequest, TExportResponse, TExportIntermediateObjectPackage, TExportObjectPackage>
        where TExportIntermediateObjectPackageCurrent : class, TExportIntermediateObjectPackage
    {
        ExportPipelineChannelOptions<TExportPipelineChannel, TExportRequest, TExportResponse, TExportIntermediateObjectPackage, TExportIntermediateObjectPackageCurrent, TExportObjectPackage> exportPipelineChannelOptions = new();
        ExportPipelineChannelOptionsBuilder<TExportPipelineChannel, TExportRequest, TExportResponse, TExportIntermediateObjectPackage, TExportIntermediateObjectPackageCurrent, TExportObjectPackage> exportPipelineChannelOptionsBuilder = new(exportPipelineChannelOptions);

        if (configureAction is not null)
            configureAction(exportPipelineChannelOptionsBuilder);

        exportPipelineChannelOptions = exportPipelineChannelOptionsBuilder.Build();

        ExportPipelineChannelKey exportPipelineChannelKey = exportPipelineChannelOptions.ExportPipelineChannelKey
            ?? ExportPipelineChannelKey.Empty;

        ExportPipelineChannelServiceDescriptor exportPipelineChannelServiceDescriptor = new()
        {
            ExportPipelineChannelKey = exportPipelineChannelKey,
            AddExportPipelineChannelServiceCollectionVisitor = AddExportPipelineChannelCore,
        };

        m_exportPipelineOptions.ExportPipelineChannelServiceDescriptors.Add(exportPipelineChannelServiceDescriptor);

        void AddExportPipelineChannelCore(IServiceCollection services)
        {
            ConfigureExportPipelineChannelServices<TExportIntermediateObjectPackageCurrent>(services, exportPipelineChannelOptions);
            if (typeof(TExportPipelineChannel) != typeof(ExportPipelineChannel<TExportRequest, TExportResponse, TExportIntermediateObjectPackage, TExportIntermediateObjectPackageCurrent, TExportObjectPackage>))
                ConfigureCustomExportPipelineChannelServices<TExportPipelineChannel, TExportIntermediateObjectPackageCurrent>(services, exportPipelineChannelOptions);

            if (exportPipelineChannelImplementationFactory is not null)
            {
                TExportPipelineChannel GetExportPipelineChannelByKey(IServiceProvider serviceProvider, object? serviceKey) =>
                    exportPipelineChannelImplementationFactory(serviceProvider, (ExportPipelineChannelKey)serviceKey!);

                services.AddKeyedTransient<IExportPipelineChannel<TExportRequest, TExportResponse, TExportIntermediateObjectPackage, TExportObjectPackage>, TExportPipelineChannel>(exportPipelineChannelKey, (serviceProvider, serviceKey) => GetExportPipelineChannelByKey(serviceProvider, serviceKey));
            }
            else
            {
                services.AddKeyedTransient<IExportPipelineChannel<TExportRequest, TExportResponse, TExportIntermediateObjectPackage, TExportObjectPackage>, TExportPipelineChannel>(exportPipelineChannelKey);
            }

            IExportPipelineChannel<TExportRequest, TExportResponse, TExportIntermediateObjectPackage, TExportObjectPackage> GetExportPipelineChannel(IServiceProvider serviceProvider) =>
                serviceProvider.GetRequiredKeyedService<IExportPipelineChannel<TExportRequest, TExportResponse, TExportIntermediateObjectPackage, TExportObjectPackage>>(exportPipelineChannelKey);

            services.AddTransient<IExportPipelineChannel<TExportRequest, TExportResponse, TExportIntermediateObjectPackage, TExportObjectPackage>>(GetExportPipelineChannel);
        }

        return this;
    }

    private static void ConfigureExportPipelineChannelServices<TExportIntermediateObjectPackageCurrent>(IServiceCollection services, ExportPipelineChannelOptions<TExportRequest, TExportResponse, TExportIntermediateObjectPackage, TExportIntermediateObjectPackageCurrent, TExportObjectPackage> exportPipelineChannelOptions)
        where TExportIntermediateObjectPackageCurrent : class, TExportIntermediateObjectPackage
    {
        const string exportPipelineChannelOptionsClassName =
            nameof(ExportPipelineChannelOptions<TExportRequest, TExportResponse, TExportIntermediateObjectPackage, TExportIntermediateObjectPackageCurrent, TExportObjectPackage>);

        const string addExportFormatterServiceCollectionVisitorPropertyName =
            nameof(ExportPipelineChannelOptions<TExportRequest, TExportResponse, TExportIntermediateObjectPackage, TExportIntermediateObjectPackageCurrent, TExportObjectPackage>.AddExportFormatterServiceCollectionVisitor);
        const string addExportSerializerServiceCollectionVisitorPropertyName =
            nameof(ExportPipelineChannelOptions<TExportRequest, TExportResponse, TExportIntermediateObjectPackage, TExportIntermediateObjectPackageCurrent, TExportObjectPackage>.AddExportSerializerServiceCollectionVisitor);
        const string addExportMapperServiceCollectionVisitorPropertyName =
            nameof(ExportPipelineChannelOptions<TExportRequest, TExportResponse, TExportIntermediateObjectPackage, TExportIntermediateObjectPackageCurrent, TExportObjectPackage>.AddExportMapperServiceCollectionVisitor);

        const string buildMethodName =
            nameof(IExportPipelineChannelOptionsBuilderInternal<TExportRequest, TExportResponse, TExportIntermediateObjectPackage, TExportIntermediateObjectPackageCurrent, TExportObjectPackage>.Build);

        Action<IServiceCollection> addExportFormatterServiceCollectionVisitor = exportPipelineChannelOptions.AddExportFormatterServiceCollectionVisitor
            ?? throw new UnreachableException($"An {exportPipelineChannelOptionsClassName} instance has its {addExportFormatterServiceCollectionVisitorPropertyName} property set to null after the {buildMethodName} method has been called.");
        Action<IServiceCollection> addExportSerializerServiceCollectionVisitor = exportPipelineChannelOptions.AddExportSerializerServiceCollectionVisitor
            ?? throw new UnreachableException($"An {exportPipelineChannelOptionsClassName} instance has its {addExportSerializerServiceCollectionVisitorPropertyName} property set to null after the {buildMethodName} method has been called.");
        Action<IServiceCollection> addExportMapperServiceCollectionVisitor = exportPipelineChannelOptions.AddExportMapperServiceCollectionVisitor
            ?? throw new UnreachableException($"An {exportPipelineChannelOptionsClassName} instance has its {addExportMapperServiceCollectionVisitorPropertyName} property set to null after the {buildMethodName} method has been called.");

        addExportFormatterServiceCollectionVisitor(services);
        addExportSerializerServiceCollectionVisitor(services);
        addExportMapperServiceCollectionVisitor(services);
    }

    private static void ConfigureCustomExportPipelineChannelServices<TExportPipelineChannel, TExportIntermediateObjectPackageCurrent>(IServiceCollection services, ExportPipelineChannelOptions<TExportPipelineChannel, TExportRequest, TExportResponse, TExportIntermediateObjectPackage, TExportIntermediateObjectPackageCurrent, TExportObjectPackage> exportPipelineChannelOptions)
        where TExportPipelineChannel : class, IExportPipelineChannel<TExportRequest, TExportResponse, TExportIntermediateObjectPackage, TExportObjectPackage>
        where TExportIntermediateObjectPackageCurrent : class, TExportIntermediateObjectPackage
    {
    }
}

internal class ExportPipelineOptionsBuilder<TExportPipeline, TExportRequest, TExportResponse, TExportIntermediateObjectPackage, TExportObjectPackage> :
    ExportPipelineOptionsBuilder<TExportRequest, TExportResponse, TExportIntermediateObjectPackage, TExportObjectPackage>,
    IExportPipelineOptionsBuilderInternal<TExportPipeline, TExportRequest, TExportResponse, TExportIntermediateObjectPackage, TExportObjectPackage>
    where TExportPipeline : class, IExportPipeline<TExportRequest, TExportResponse>
    where TExportRequest : ExportRequest
    where TExportResponse : ExportResponse
    where TExportIntermediateObjectPackage : class
    where TExportObjectPackage : class
{
    private readonly ExportPipelineOptions<TExportPipeline, TExportRequest, TExportResponse, TExportIntermediateObjectPackage, TExportObjectPackage> m_exportPipelineOptions;

    public ExportPipelineOptionsBuilder(ExportPipelineOptions<TExportPipeline, TExportRequest, TExportResponse, TExportIntermediateObjectPackage, TExportObjectPackage> exportPipelineOptions)
        : base(exportPipelineOptions)
    {
        m_exportPipelineOptions = exportPipelineOptions;
    }

    /// <inheritdoc />
    public new ExportPipelineOptions<TExportPipeline, TExportRequest, TExportResponse, TExportIntermediateObjectPackage, TExportObjectPackage> Build()
    {
        Assert();

        return m_exportPipelineOptions;
    }
}
