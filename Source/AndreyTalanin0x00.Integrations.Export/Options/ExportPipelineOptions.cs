using System;
using System.Collections.ObjectModel;

using AndreyTalanin0x00.Integrations.Export.DependencyInjection;
using AndreyTalanin0x00.Integrations.Export.Requests;
using AndreyTalanin0x00.Integrations.Export.Responses;
using AndreyTalanin0x00.Integrations.Export.Services.Abstractions;

using Microsoft.Extensions.DependencyInjection;

namespace AndreyTalanin0x00.Integrations.Export.Options;

internal class ExportPipelineOptions<TExportRequest, TExportResponse, TExportIntermediateObjectPackage, TExportObjectPackage>
    where TExportRequest : ExportRequest
    where TExportResponse : ExportResponse
    where TExportIntermediateObjectPackage : class
    where TExportObjectPackage : class
{
    public Action<IServiceCollection>? AddExportWriterServiceCollectionVisitor { get; set; }

    public Action<IServiceCollection>? AddExportProcessorServiceCollectionVisitor { get; set; }

    public Action<IServiceCollection>? AddExportPipelineChannelKeyResolverServiceCollectionVisitor { get; set; }

    public Collection<ExportPipelineChannelServiceDescriptor> ExportPipelineChannelServiceDescriptors { get; set; } = [];
}

internal class ExportPipelineOptions<TExportPipeline, TExportRequest, TExportResponse, TExportIntermediateObjectPackage, TExportObjectPackage> :
    ExportPipelineOptions<TExportRequest, TExportResponse, TExportIntermediateObjectPackage, TExportObjectPackage>
    where TExportPipeline : class, IExportPipeline<TExportRequest, TExportResponse>
    where TExportRequest : ExportRequest
    where TExportResponse : ExportResponse
    where TExportIntermediateObjectPackage : class
    where TExportObjectPackage : class
{
}
