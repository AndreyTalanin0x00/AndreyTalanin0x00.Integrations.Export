using System;
using System.Threading;
using System.Threading.Tasks;

using AndreyTalanin0x00.Integrations.Export.Requests;
using AndreyTalanin0x00.Integrations.Export.Responses;
using AndreyTalanin0x00.Integrations.Export.Services.Abstractions;

namespace AndreyTalanin0x00.Integrations.Export.Services;

public class ExportPipelineChannel<TExportRequest, TExportResponse, TExportIntermediateObjectPackage, TExportIntermediateObjectPackageCurrent, TExportObjectPackage> :
    IExportPipelineChannel<TExportRequest, TExportResponse, TExportIntermediateObjectPackage, TExportObjectPackage>
    where TExportRequest : ExportRequest
    where TExportResponse : ExportResponse
    where TExportIntermediateObjectPackage : class
    where TExportIntermediateObjectPackageCurrent : class, TExportIntermediateObjectPackage
    where TExportObjectPackage : class
{
    /// <inheritdoc />
    public ExportPipelineChannelKey Key => throw new NotImplementedException();

    /// <inheritdoc />
    public Task<ExportTargetBatch> SerializeAsync(ExportObjectPackageBatch<TExportIntermediateObjectPackage, TExportObjectPackage> exportObjectPackageBatch, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}
