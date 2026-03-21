using System;
using System.Diagnostics.CodeAnalysis;

using AndreyTalanin0x00.Extensions.DependencyInjection;
using AndreyTalanin0x00.Integrations.Export.Options.Builders.Abstractions;
using AndreyTalanin0x00.Integrations.Export.Requests;
using AndreyTalanin0x00.Integrations.Export.Responses;
using AndreyTalanin0x00.Integrations.Export.Services.Abstractions;

namespace AndreyTalanin0x00.Integrations.Export.Options.Builders;

internal class ExportPipelineOptionsBuilder<TExportRequest, TExportResponse, TExportIntermediateObjectPackage, TExportObjectPackage> :
    IExportPipelineOptionsBuilder<TExportRequest, TExportResponse, TExportIntermediateObjectPackage, TExportObjectPackage>
    where TExportRequest : ExportRequest
    where TExportResponse : ExportResponse
    where TExportIntermediateObjectPackage : class
    where TExportObjectPackage : class
{
    /// <inheritdoc />
    public IExportPipelineOptionsBuilder<TExportRequest, TExportResponse, TExportIntermediateObjectPackage, TExportObjectPackage> UseExportWriter<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] TExportWriter>()
        where TExportWriter : class, IExportWriter<TExportRequest, TExportResponse>
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public IExportPipelineOptionsBuilder<TExportRequest, TExportResponse, TExportIntermediateObjectPackage, TExportObjectPackage> UseExportWriter<TExportWriter>(ServiceImplementationFactory<TExportWriter> exportWriterImplementationFactory)
        where TExportWriter : class, IExportWriter<TExportRequest, TExportResponse>
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public IExportPipelineOptionsBuilder<TExportRequest, TExportResponse, TExportIntermediateObjectPackage, TExportObjectPackage> UseExportProcessor<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] TExportProcessor>()
        where TExportProcessor : class, IExportProcessor<TExportRequest, TExportResponse, TExportIntermediateObjectPackage, TExportObjectPackage>
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public IExportPipelineOptionsBuilder<TExportRequest, TExportResponse, TExportIntermediateObjectPackage, TExportObjectPackage> UseExportProcessor<TExportProcessor>(ServiceImplementationFactory<TExportProcessor> exportProcessorImplementationFactory)
        where TExportProcessor : class, IExportProcessor<TExportRequest, TExportResponse, TExportIntermediateObjectPackage, TExportObjectPackage>
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public IExportPipelineOptionsBuilder<TExportRequest, TExportResponse, TExportIntermediateObjectPackage, TExportObjectPackage> UseExportPipelineChannelKeyResolver<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] TExportPipelineChannelKeyResolver>()
        where TExportPipelineChannelKeyResolver : class, IExportPipelineChannelKeyResolver<TExportRequest, TExportResponse>
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public IExportPipelineOptionsBuilder<TExportRequest, TExportResponse, TExportIntermediateObjectPackage, TExportObjectPackage> UseExportPipelineChannelKeyResolver<TExportPipelineChannelKeyResolver>(ServiceImplementationFactory<TExportPipelineChannelKeyResolver> exportPipelineChannelKeyResolverImplementationFactory)
        where TExportPipelineChannelKeyResolver : class, IExportPipelineChannelKeyResolver<TExportRequest, TExportResponse>
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public IExportPipelineOptionsBuilder<TExportRequest, TExportResponse, TExportIntermediateObjectPackage, TExportObjectPackage> AddExportPipelineChannel<TExportIntermediateObjectPackageCurrent>(Action<IExportPipelineChannelOptionsBuilder<TExportRequest, TExportResponse, TExportIntermediateObjectPackage, TExportIntermediateObjectPackageCurrent, TExportObjectPackage>> configureAction)
        where TExportIntermediateObjectPackageCurrent : class, TExportIntermediateObjectPackage
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public IExportPipelineOptionsBuilder<TExportRequest, TExportResponse, TExportIntermediateObjectPackage, TExportObjectPackage> AddCustomExportPipelineChannel<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] TExportPipelineChannel, TExportIntermediateObjectPackageCurrent>(Action<IExportPipelineChannelOptionsBuilder<TExportPipelineChannel, TExportRequest, TExportResponse, TExportIntermediateObjectPackage, TExportIntermediateObjectPackageCurrent, TExportObjectPackage>> configureAction)
        where TExportPipelineChannel : class, IExportPipelineChannel<TExportRequest, TExportResponse, TExportIntermediateObjectPackage, TExportObjectPackage>
        where TExportIntermediateObjectPackageCurrent : class, TExportIntermediateObjectPackage
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public IExportPipelineOptionsBuilder<TExportRequest, TExportResponse, TExportIntermediateObjectPackage, TExportObjectPackage> AddCustomExportPipelineChannel<TExportPipelineChannel, TExportIntermediateObjectPackageCurrent>(KeyedServiceImplementationFactory<TExportPipelineChannel, ExportPipelineChannelKey> exportPipelineChannelImplementationFactory, Action<IExportPipelineChannelOptionsBuilder<TExportPipelineChannel, TExportRequest, TExportResponse, TExportIntermediateObjectPackage, TExportIntermediateObjectPackageCurrent, TExportObjectPackage>> configureAction)
        where TExportPipelineChannel : class, IExportPipelineChannel<TExportRequest, TExportResponse, TExportIntermediateObjectPackage, TExportObjectPackage>
        where TExportIntermediateObjectPackageCurrent : class, TExportIntermediateObjectPackage
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public ExportPipelineOptions<TExportRequest, TExportResponse, TExportIntermediateObjectPackage, TExportObjectPackage> Build()
    {
        throw new NotImplementedException();
    }
}

internal class ExportPipelineOptionsBuilder<TExportPipeline, TExportRequest, TExportResponse, TExportIntermediateObjectPackage, TExportObjectPackage> :
    ExportPipelineOptionsBuilder<TExportRequest, TExportResponse, TExportIntermediateObjectPackage, TExportObjectPackage>,
    IExportPipelineOptionsBuilder<TExportPipeline, TExportRequest, TExportResponse, TExportIntermediateObjectPackage, TExportObjectPackage>
    where TExportPipeline : class, IExportPipeline<TExportRequest, TExportResponse>
    where TExportRequest : ExportRequest
    where TExportResponse : ExportResponse
    where TExportIntermediateObjectPackage : class
    where TExportObjectPackage : class
{
    /// <inheritdoc />
    public new ExportPipelineOptions<TExportPipeline, TExportRequest, TExportResponse, TExportIntermediateObjectPackage, TExportObjectPackage> Build()
    {
        throw new NotImplementedException();
    }
}
