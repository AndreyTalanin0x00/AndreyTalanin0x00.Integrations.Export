using System;
using System.Diagnostics.CodeAnalysis;

using AndreyTalanin0x00.Extensions.DependencyInjection;
using AndreyTalanin0x00.Integrations.Export.Options.Builders.Abstractions;
using AndreyTalanin0x00.Integrations.Export.Requests;
using AndreyTalanin0x00.Integrations.Export.Responses;
using AndreyTalanin0x00.Integrations.Export.Services.Abstractions;
using AndreyTalanin0x00.Integrations.Export.Services.Specialized;

using Microsoft.Extensions.DependencyInjection;

namespace AndreyTalanin0x00.Integrations.Export.Options.Builders;

internal class ExportPipelineChannelOptionsBuilder<TExportRequest, TExportResponse, TExportIntermediateObjectPackage, TExportIntermediateObjectPackageCurrent, TExportObjectPackage> :
    IExportPipelineChannelOptionsBuilderInternal<TExportRequest, TExportResponse, TExportIntermediateObjectPackage, TExportIntermediateObjectPackageCurrent, TExportObjectPackage>
    where TExportRequest : ExportRequest
    where TExportResponse : ExportResponse
    where TExportIntermediateObjectPackage : class
    where TExportIntermediateObjectPackageCurrent : class, TExportIntermediateObjectPackage
    where TExportObjectPackage : class
{
    private readonly ExportPipelineChannelOptions<TExportRequest, TExportResponse, TExportIntermediateObjectPackage, TExportIntermediateObjectPackageCurrent, TExportObjectPackage> m_exportPipelineChannelOptions;

    public ExportPipelineChannelOptionsBuilder(ExportPipelineChannelOptions<TExportRequest, TExportResponse, TExportIntermediateObjectPackage, TExportIntermediateObjectPackageCurrent, TExportObjectPackage> exportPipelineChannelOptions)
    {
        m_exportPipelineChannelOptions = exportPipelineChannelOptions;

        UseExportFormatter<PassThroughExportFormatter<TExportRequest, TExportResponse, TExportIntermediateObjectPackageCurrent, TExportObjectPackage>>();

        if (typeof(TExportIntermediateObjectPackageCurrent) == typeof(TExportObjectPackage))
            UseExportMapper<PassThroughExportMapper<TExportRequest, TExportResponse, TExportIntermediateObjectPackageCurrent, TExportObjectPackage>>();

        return;
    }

    /// <inheritdoc />
    public IExportPipelineChannelOptionsBuilder<TExportRequest, TExportResponse, TExportIntermediateObjectPackage, TExportIntermediateObjectPackageCurrent, TExportObjectPackage> UseKey(string exportPipelineChannelKey)
    {
        m_exportPipelineChannelOptions.ExportPipelineChannelKey = new ExportPipelineChannelKey(exportPipelineChannelKey);

        return this;
    }

    /// <inheritdoc />
    public IExportPipelineChannelOptionsBuilder<TExportRequest, TExportResponse, TExportIntermediateObjectPackage, TExportIntermediateObjectPackageCurrent, TExportObjectPackage> UseKey(ExportPipelineChannelKey exportPipelineChannelKey)
    {
        m_exportPipelineChannelOptions.ExportPipelineChannelKey = exportPipelineChannelKey;

        return this;
    }

    /// <inheritdoc />
    public IExportPipelineChannelOptionsBuilder<TExportRequest, TExportResponse, TExportIntermediateObjectPackage, TExportIntermediateObjectPackageCurrent, TExportObjectPackage> UseExportFormatter<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] TExportFormatter>()
        where TExportFormatter : class, IExportFormatter<TExportRequest, TExportResponse, TExportIntermediateObjectPackageCurrent, TExportObjectPackage>
    {
        m_exportPipelineChannelOptions.AddExportFormatterServiceCollectionVisitor = AddExportFormatter;

        static void AddExportFormatter(IServiceCollection services) =>
            services.AddTransient<IExportFormatter<TExportRequest, TExportResponse, TExportIntermediateObjectPackageCurrent, TExportObjectPackage>, TExportFormatter>();

        return this;
    }

    /// <inheritdoc />
    public IExportPipelineChannelOptionsBuilder<TExportRequest, TExportResponse, TExportIntermediateObjectPackage, TExportIntermediateObjectPackageCurrent, TExportObjectPackage> UseExportFormatter<TExportFormatter>(ServiceImplementationFactory<TExportFormatter> exportFormatterImplementationFactory)
        where TExportFormatter : class, IExportFormatter<TExportRequest, TExportResponse, TExportIntermediateObjectPackageCurrent, TExportObjectPackage>
    {
        m_exportPipelineChannelOptions.AddExportFormatterServiceCollectionVisitor = AddExportFormatter;

        void AddExportFormatter(IServiceCollection services) =>
            services.AddTransient<IExportFormatter<TExportRequest, TExportResponse, TExportIntermediateObjectPackageCurrent, TExportObjectPackage>, TExportFormatter>(serviceProvider => exportFormatterImplementationFactory(serviceProvider));

        return this;
    }

    /// <inheritdoc />
    public IExportPipelineChannelOptionsBuilder<TExportRequest, TExportResponse, TExportIntermediateObjectPackage, TExportIntermediateObjectPackageCurrent, TExportObjectPackage> UseExportSerializer<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] TExportSerializer>()
        where TExportSerializer : class, IExportSerializer<TExportRequest, TExportResponse, TExportIntermediateObjectPackageCurrent, TExportObjectPackage>
    {
        m_exportPipelineChannelOptions.AddExportSerializerServiceCollectionVisitor = AddExportSerializer;

        static void AddExportSerializer(IServiceCollection services) =>
            services.AddTransient<IExportSerializer<TExportRequest, TExportResponse, TExportIntermediateObjectPackageCurrent, TExportObjectPackage>, TExportSerializer>();

        return this;
    }

    /// <inheritdoc />
    public IExportPipelineChannelOptionsBuilder<TExportRequest, TExportResponse, TExportIntermediateObjectPackage, TExportIntermediateObjectPackageCurrent, TExportObjectPackage> UseExportSerializer<TExportSerializer>(ServiceImplementationFactory<TExportSerializer> exportSerializerImplementationFactory)
        where TExportSerializer : class, IExportSerializer<TExportRequest, TExportResponse, TExportIntermediateObjectPackageCurrent, TExportObjectPackage>
    {
        m_exportPipelineChannelOptions.AddExportSerializerServiceCollectionVisitor = AddExportSerializer;

        void AddExportSerializer(IServiceCollection services) =>
            services.AddTransient<IExportSerializer<TExportRequest, TExportResponse, TExportIntermediateObjectPackageCurrent, TExportObjectPackage>, TExportSerializer>(serviceProvider => exportSerializerImplementationFactory(serviceProvider));

        return this;
    }

    /// <inheritdoc />
    public IExportPipelineChannelOptionsBuilder<TExportRequest, TExportResponse, TExportIntermediateObjectPackage, TExportIntermediateObjectPackageCurrent, TExportObjectPackage> UseExportMapper<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] TExportMapper>()
        where TExportMapper : class, IExportMapper<TExportRequest, TExportResponse, TExportIntermediateObjectPackageCurrent, TExportObjectPackage>
    {
        m_exportPipelineChannelOptions.AddExportMapperServiceCollectionVisitor = AddExportMapper;

        static void AddExportMapper(IServiceCollection services) =>
            services.AddTransient<IExportMapper<TExportRequest, TExportResponse, TExportIntermediateObjectPackageCurrent, TExportObjectPackage>, TExportMapper>();

        return this;
    }

    /// <inheritdoc />
    public IExportPipelineChannelOptionsBuilder<TExportRequest, TExportResponse, TExportIntermediateObjectPackage, TExportIntermediateObjectPackageCurrent, TExportObjectPackage> UseExportMapper<TExportMapper>(ServiceImplementationFactory<TExportMapper> exportMapperImplementationFactory)
        where TExportMapper : class, IExportMapper<TExportRequest, TExportResponse, TExportIntermediateObjectPackageCurrent, TExportObjectPackage>
    {
        m_exportPipelineChannelOptions.AddExportMapperServiceCollectionVisitor = AddExportMapper;

        void AddExportMapper(IServiceCollection services) =>
            services.AddTransient<IExportMapper<TExportRequest, TExportResponse, TExportIntermediateObjectPackageCurrent, TExportObjectPackage>, TExportMapper>(serviceProvider => exportMapperImplementationFactory(serviceProvider));

        return this;
    }

    /// <inheritdoc />
    public ExportPipelineChannelOptions<TExportRequest, TExportResponse, TExportIntermediateObjectPackage, TExportIntermediateObjectPackageCurrent, TExportObjectPackage> Build()
    {
        Assert();

        return m_exportPipelineChannelOptions;
    }

    protected virtual void Assert()
    {
        if (m_exportPipelineChannelOptions.AddExportFormatterServiceCollectionVisitor is null)
            throw new InvalidOperationException("The export pipeline channel's configuration is invalid: no export formatter is specified.");
        if (m_exportPipelineChannelOptions.AddExportSerializerServiceCollectionVisitor is null)
            throw new InvalidOperationException("The export pipeline channel's configuration is invalid: no export serializer is specified.");
        if (m_exportPipelineChannelOptions.AddExportMapperServiceCollectionVisitor is null)
            throw new InvalidOperationException("The export pipeline channel's configuration is invalid: no export mapper is specified.");

        return;
    }
}

internal class ExportPipelineChannelOptionsBuilder<TExportPipelineChannel, TExportRequest, TExportResponse, TExportIntermediateObjectPackage, TExportIntermediateObjectPackageCurrent, TExportObjectPackage> :
    ExportPipelineChannelOptionsBuilder<TExportRequest, TExportResponse, TExportIntermediateObjectPackage, TExportIntermediateObjectPackageCurrent, TExportObjectPackage>,
    IExportPipelineChannelOptionsBuilderInternal<TExportPipelineChannel, TExportRequest, TExportResponse, TExportIntermediateObjectPackage, TExportIntermediateObjectPackageCurrent, TExportObjectPackage>
    where TExportPipelineChannel : class, IExportPipelineChannel<TExportRequest, TExportResponse, TExportIntermediateObjectPackage, TExportObjectPackage>
    where TExportRequest : ExportRequest
    where TExportResponse : ExportResponse
    where TExportIntermediateObjectPackage : class
    where TExportIntermediateObjectPackageCurrent : class, TExportIntermediateObjectPackage
    where TExportObjectPackage : class
{
    private readonly ExportPipelineChannelOptions<TExportPipelineChannel, TExportRequest, TExportResponse, TExportIntermediateObjectPackage, TExportIntermediateObjectPackageCurrent, TExportObjectPackage> m_exportPipelineChannelOptions;

    public ExportPipelineChannelOptionsBuilder(ExportPipelineChannelOptions<TExportPipelineChannel, TExportRequest, TExportResponse, TExportIntermediateObjectPackage, TExportIntermediateObjectPackageCurrent, TExportObjectPackage> exportPipelineChannelOptions)
        : base(exportPipelineChannelOptions)
    {
        m_exportPipelineChannelOptions = exportPipelineChannelOptions;
    }

    /// <inheritdoc />
    public new ExportPipelineChannelOptions<TExportPipelineChannel, TExportRequest, TExportResponse, TExportIntermediateObjectPackage, TExportIntermediateObjectPackageCurrent, TExportObjectPackage> Build()
    {
        Assert();

        return m_exportPipelineChannelOptions;
    }
}
