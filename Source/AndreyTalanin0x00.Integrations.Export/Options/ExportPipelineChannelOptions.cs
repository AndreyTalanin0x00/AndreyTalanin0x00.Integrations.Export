using System;

using AndreyTalanin0x00.Integrations.Export.Requests;
using AndreyTalanin0x00.Integrations.Export.Responses;
using AndreyTalanin0x00.Integrations.Export.Services.Abstractions;

using Microsoft.Extensions.DependencyInjection;

namespace AndreyTalanin0x00.Integrations.Export.Options;

internal class ExportPipelineChannelOptions<TExportRequest, TExportResponse, TExportIntermediateObjectPackage, TExportIntermediateObjectPackageCurrent, TExportObjectPackage>
    where TExportRequest : ExportRequest
    where TExportResponse : ExportResponse
    where TExportIntermediateObjectPackage : class
    where TExportIntermediateObjectPackageCurrent : class, TExportIntermediateObjectPackage
    where TExportObjectPackage : class
{
    public ExportPipelineChannelKey? ExportPipelineChannelKey { get; set; }

    public Action<IServiceCollection>? AddExportFormatterServiceCollectionVisitor { get; set; }

    public Action<IServiceCollection>? AddExportSerializerServiceCollectionVisitor { get; set; }

    public Action<IServiceCollection>? AddExportMapperServiceCollectionVisitor { get; set; }
}

internal class ExportPipelineChannelOptions<TExportPipelineChannel, TExportRequest, TExportResponse, TExportIntermediateObjectPackage, TExportIntermediateObjectPackageCurrent, TExportObjectPackage> :
    ExportPipelineChannelOptions<TExportRequest, TExportResponse, TExportIntermediateObjectPackage, TExportIntermediateObjectPackageCurrent, TExportObjectPackage>
    where TExportPipelineChannel : class, IExportPipelineChannel<TExportRequest, TExportResponse, TExportIntermediateObjectPackage, TExportObjectPackage>
    where TExportRequest : ExportRequest
    where TExportResponse : ExportResponse
    where TExportIntermediateObjectPackage : class
    where TExportIntermediateObjectPackageCurrent : class, TExportIntermediateObjectPackage
    where TExportObjectPackage : class
{
}
