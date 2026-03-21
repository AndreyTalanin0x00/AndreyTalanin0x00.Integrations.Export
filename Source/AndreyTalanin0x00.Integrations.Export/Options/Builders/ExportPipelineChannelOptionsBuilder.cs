using System;
using System.Diagnostics.CodeAnalysis;

using AndreyTalanin0x00.Extensions.DependencyInjection;
using AndreyTalanin0x00.Integrations.Export.Options.Builders.Abstractions;
using AndreyTalanin0x00.Integrations.Export.Requests;
using AndreyTalanin0x00.Integrations.Export.Responses;
using AndreyTalanin0x00.Integrations.Export.Services.Abstractions;

namespace AndreyTalanin0x00.Integrations.Export.Options.Builders;

internal class ExportPipelineChannelOptionsBuilder<TExportRequest, TExportResponse, TExportIntermediateObjectPackage, TExportIntermediateObjectPackageCurrent, TExportObjectPackage> :
    IExportPipelineChannelOptionsBuilder<TExportRequest, TExportResponse, TExportIntermediateObjectPackage, TExportIntermediateObjectPackageCurrent, TExportObjectPackage>
    where TExportRequest : ExportRequest
    where TExportResponse : ExportResponse
    where TExportIntermediateObjectPackage : class
    where TExportIntermediateObjectPackageCurrent : class, TExportIntermediateObjectPackage
    where TExportObjectPackage : class
{
    /// <inheritdoc />
    public IExportPipelineChannelOptionsBuilder<TExportRequest, TExportResponse, TExportIntermediateObjectPackage, TExportIntermediateObjectPackageCurrent, TExportObjectPackage> UseKey(string exportPipelineChannelKey)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public IExportPipelineChannelOptionsBuilder<TExportRequest, TExportResponse, TExportIntermediateObjectPackage, TExportIntermediateObjectPackageCurrent, TExportObjectPackage> UseKey(ExportPipelineChannelKey exportPipelineChannelKey)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public IExportPipelineChannelOptionsBuilder<TExportRequest, TExportResponse, TExportIntermediateObjectPackage, TExportIntermediateObjectPackageCurrent, TExportObjectPackage> UseExportFormatter<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] TExportFormatter>()
        where TExportFormatter : class, IExportFormatter<TExportRequest, TExportResponse, TExportIntermediateObjectPackageCurrent, TExportObjectPackage>
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public IExportPipelineChannelOptionsBuilder<TExportRequest, TExportResponse, TExportIntermediateObjectPackage, TExportIntermediateObjectPackageCurrent, TExportObjectPackage> UseExportFormatter<TExportFormatter>(ServiceImplementationFactory<TExportFormatter> exportFormatterImplementationFactory)
        where TExportFormatter : class, IExportFormatter<TExportRequest, TExportResponse, TExportIntermediateObjectPackageCurrent, TExportObjectPackage>
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public IExportPipelineChannelOptionsBuilder<TExportRequest, TExportResponse, TExportIntermediateObjectPackage, TExportIntermediateObjectPackageCurrent, TExportObjectPackage> UseExportSerializer<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] TExportSerializer>()
        where TExportSerializer : class, IExportSerializer<TExportRequest, TExportResponse, TExportIntermediateObjectPackageCurrent, TExportObjectPackage>
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public IExportPipelineChannelOptionsBuilder<TExportRequest, TExportResponse, TExportIntermediateObjectPackage, TExportIntermediateObjectPackageCurrent, TExportObjectPackage> UseExportSerializer<TExportSerializer>(ServiceImplementationFactory<TExportSerializer> exportSerializerImplementationFactory)
        where TExportSerializer : class, IExportSerializer<TExportRequest, TExportResponse, TExportIntermediateObjectPackageCurrent, TExportObjectPackage>
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public IExportPipelineChannelOptionsBuilder<TExportRequest, TExportResponse, TExportIntermediateObjectPackage, TExportIntermediateObjectPackageCurrent, TExportObjectPackage> UseExportMapper<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] TExportMapper>()
        where TExportMapper : class, IExportMapper<TExportRequest, TExportResponse, TExportIntermediateObjectPackageCurrent, TExportObjectPackage>
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public IExportPipelineChannelOptionsBuilder<TExportRequest, TExportResponse, TExportIntermediateObjectPackage, TExportIntermediateObjectPackageCurrent, TExportObjectPackage> UseExportMapper<TExportMapper>(ServiceImplementationFactory<TExportMapper> exportMapperImplementationFactory)
        where TExportMapper : class, IExportMapper<TExportRequest, TExportResponse, TExportIntermediateObjectPackageCurrent, TExportObjectPackage>
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public ExportPipelineChannelOptions<TExportRequest, TExportResponse, TExportIntermediateObjectPackage, TExportIntermediateObjectPackageCurrent, TExportObjectPackage> Build()
    {
        throw new NotImplementedException();
    }
}

internal class ExportPipelineChannelOptionsBuilder<TExportPipelineChannel, TExportRequest, TExportResponse, TExportIntermediateObjectPackage, TExportIntermediateObjectPackageCurrent, TExportObjectPackage> :
    ExportPipelineChannelOptionsBuilder<TExportRequest, TExportResponse, TExportIntermediateObjectPackage, TExportIntermediateObjectPackageCurrent, TExportObjectPackage>,
    IExportPipelineChannelOptionsBuilder<TExportPipelineChannel, TExportRequest, TExportResponse, TExportIntermediateObjectPackage, TExportIntermediateObjectPackageCurrent, TExportObjectPackage>
    where TExportPipelineChannel : class, IExportPipelineChannel<TExportRequest, TExportResponse, TExportIntermediateObjectPackage, TExportObjectPackage>
    where TExportRequest : ExportRequest
    where TExportResponse : ExportResponse
    where TExportIntermediateObjectPackage : class
    where TExportIntermediateObjectPackageCurrent : class, TExportIntermediateObjectPackage
    where TExportObjectPackage : class
{
    /// <inheritdoc />
    public new ExportPipelineChannelOptions<TExportPipelineChannel, TExportRequest, TExportResponse, TExportIntermediateObjectPackage, TExportIntermediateObjectPackageCurrent, TExportObjectPackage> Build()
    {
        throw new NotImplementedException();
    }
}
