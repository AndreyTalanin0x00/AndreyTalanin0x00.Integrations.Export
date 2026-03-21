using System.Diagnostics.CodeAnalysis;

using AndreyTalanin0x00.Extensions.DependencyInjection;
using AndreyTalanin0x00.Integrations.Export.Requests;
using AndreyTalanin0x00.Integrations.Export.Responses;
using AndreyTalanin0x00.Integrations.Export.Services.Abstractions;

namespace AndreyTalanin0x00.Integrations.Export.Options.Builders.Abstractions;

public interface IExportPipelineChannelOptionsBuilder<TExportRequest, TExportResponse, TExportIntermediateObjectPackage, TExportIntermediateObjectPackageCurrent, TExportObjectPackage>
    where TExportRequest : ExportRequest
    where TExportResponse : ExportResponse
    where TExportIntermediateObjectPackage : class
    where TExportIntermediateObjectPackageCurrent : class, TExportIntermediateObjectPackage
    where TExportObjectPackage : class
{
    public IExportPipelineChannelOptionsBuilder<TExportRequest, TExportResponse, TExportIntermediateObjectPackage, TExportIntermediateObjectPackageCurrent, TExportObjectPackage> UseKey(string exportPipelineChannelKey);

    public IExportPipelineChannelOptionsBuilder<TExportRequest, TExportResponse, TExportIntermediateObjectPackage, TExportIntermediateObjectPackageCurrent, TExportObjectPackage> UseKey(ExportPipelineChannelKey exportPipelineChannelKey);

    public IExportPipelineChannelOptionsBuilder<TExportRequest, TExportResponse, TExportIntermediateObjectPackage, TExportIntermediateObjectPackageCurrent, TExportObjectPackage> UseExportFormatter<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] TExportFormatter>()
        where TExportFormatter : class, IExportFormatter<TExportRequest, TExportResponse, TExportIntermediateObjectPackageCurrent, TExportObjectPackage>;

    public IExportPipelineChannelOptionsBuilder<TExportRequest, TExportResponse, TExportIntermediateObjectPackage, TExportIntermediateObjectPackageCurrent, TExportObjectPackage> UseExportFormatter<TExportFormatter>(ServiceImplementationFactory<TExportFormatter> exportFormatterImplementationFactory)
        where TExportFormatter : class, IExportFormatter<TExportRequest, TExportResponse, TExportIntermediateObjectPackageCurrent, TExportObjectPackage>;

    public IExportPipelineChannelOptionsBuilder<TExportRequest, TExportResponse, TExportIntermediateObjectPackage, TExportIntermediateObjectPackageCurrent, TExportObjectPackage> UseExportSerializer<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] TExportSerializer>()
        where TExportSerializer : class, IExportSerializer<TExportRequest, TExportResponse, TExportIntermediateObjectPackageCurrent, TExportObjectPackage>;

    public IExportPipelineChannelOptionsBuilder<TExportRequest, TExportResponse, TExportIntermediateObjectPackage, TExportIntermediateObjectPackageCurrent, TExportObjectPackage> UseExportSerializer<TExportSerializer>(ServiceImplementationFactory<TExportSerializer> exportSerializerImplementationFactory)
        where TExportSerializer : class, IExportSerializer<TExportRequest, TExportResponse, TExportIntermediateObjectPackageCurrent, TExportObjectPackage>;

    public IExportPipelineChannelOptionsBuilder<TExportRequest, TExportResponse, TExportIntermediateObjectPackage, TExportIntermediateObjectPackageCurrent, TExportObjectPackage> UseExportMapper<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] TExportMapper>()
        where TExportMapper : class, IExportMapper<TExportRequest, TExportResponse, TExportIntermediateObjectPackageCurrent, TExportObjectPackage>;

    public IExportPipelineChannelOptionsBuilder<TExportRequest, TExportResponse, TExportIntermediateObjectPackage, TExportIntermediateObjectPackageCurrent, TExportObjectPackage> UseExportMapper<TExportMapper>(ServiceImplementationFactory<TExportMapper> exportMapperImplementationFactory)
        where TExportMapper : class, IExportMapper<TExportRequest, TExportResponse, TExportIntermediateObjectPackageCurrent, TExportObjectPackage>;
}

internal interface IExportPipelineChannelOptionsBuilderInternal<TExportRequest, TExportResponse, TExportIntermediateObjectPackage, TExportIntermediateObjectPackageCurrent, TExportObjectPackage> :
    IExportPipelineChannelOptionsBuilder<TExportRequest, TExportResponse, TExportIntermediateObjectPackage, TExportIntermediateObjectPackageCurrent, TExportObjectPackage>
    where TExportRequest : ExportRequest
    where TExportResponse : ExportResponse
    where TExportIntermediateObjectPackage : class
    where TExportIntermediateObjectPackageCurrent : class, TExportIntermediateObjectPackage
    where TExportObjectPackage : class
{
    public ExportPipelineChannelOptions<TExportRequest, TExportResponse, TExportIntermediateObjectPackage, TExportIntermediateObjectPackageCurrent, TExportObjectPackage> Build();
}

public interface IExportPipelineChannelOptionsBuilder<TExportPipelineChannel, TExportRequest, TExportResponse, TExportIntermediateObjectPackage, TExportIntermediateObjectPackageCurrent, TExportObjectPackage> :
    IExportPipelineChannelOptionsBuilder<TExportRequest, TExportResponse, TExportIntermediateObjectPackage, TExportIntermediateObjectPackageCurrent, TExportObjectPackage>
    where TExportPipelineChannel : class, IExportPipelineChannel<TExportRequest, TExportResponse, TExportIntermediateObjectPackage, TExportObjectPackage>
    where TExportRequest : ExportRequest
    where TExportResponse : ExportResponse
    where TExportIntermediateObjectPackage : class
    where TExportIntermediateObjectPackageCurrent : class, TExportIntermediateObjectPackage
    where TExportObjectPackage : class
{
}

internal interface IExportPipelineChannelOptionsBuilderInternal<TExportPipelineChannel, TExportRequest, TExportResponse, TExportIntermediateObjectPackage, TExportIntermediateObjectPackageCurrent, TExportObjectPackage> :
    IExportPipelineChannelOptionsBuilder<TExportPipelineChannel, TExportRequest, TExportResponse, TExportIntermediateObjectPackage, TExportIntermediateObjectPackageCurrent, TExportObjectPackage>
    where TExportPipelineChannel : class, IExportPipelineChannel<TExportRequest, TExportResponse, TExportIntermediateObjectPackage, TExportObjectPackage>
    where TExportRequest : ExportRequest
    where TExportResponse : ExportResponse
    where TExportIntermediateObjectPackage : class
    where TExportIntermediateObjectPackageCurrent : class, TExportIntermediateObjectPackage
    where TExportObjectPackage : class
{
    public ExportPipelineChannelOptions<TExportPipelineChannel, TExportRequest, TExportResponse, TExportIntermediateObjectPackage, TExportIntermediateObjectPackageCurrent, TExportObjectPackage> Build();
}
