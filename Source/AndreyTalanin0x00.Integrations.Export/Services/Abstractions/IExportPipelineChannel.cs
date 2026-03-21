using System.Threading;
using System.Threading.Tasks;

using AndreyTalanin0x00.Integrations.Export.Requests;
using AndreyTalanin0x00.Integrations.Export.Responses;

namespace AndreyTalanin0x00.Integrations.Export.Services.Abstractions;

public interface IExportPipelineChannel<TExportRequest, TExportResponse, TExportIntermediateObjectPackage, TExportObjectPackage>
    where TExportRequest : ExportRequest
    where TExportResponse : ExportResponse
    where TExportIntermediateObjectPackage : class
    where TExportObjectPackage : class
{
    public ExportPipelineChannelKey Key { get; }

    public Task<ExportTargetBatch> SerializeAsync(ExportObjectPackageBatch<TExportIntermediateObjectPackage, TExportObjectPackage> exportObjectPackageBatch, CancellationToken cancellationToken = default);
}
