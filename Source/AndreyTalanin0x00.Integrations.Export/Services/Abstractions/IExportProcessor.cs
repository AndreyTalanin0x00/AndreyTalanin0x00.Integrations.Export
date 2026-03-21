using System.Threading;
using System.Threading.Tasks;

using AndreyTalanin0x00.Integrations.Export.Requests;
using AndreyTalanin0x00.Integrations.Export.Responses;

namespace AndreyTalanin0x00.Integrations.Export.Services.Abstractions;

public interface IExportProcessor<TExportRequest, TExportResponse, TExportIntermediateObjectPackage, TExportObjectPackage>
    where TExportRequest : ExportRequest
    where TExportResponse : ExportResponse
    where TExportIntermediateObjectPackage : class
    where TExportObjectPackage : class
{
    public Task<ExportObjectPackageBatch<TExportIntermediateObjectPackage, TExportObjectPackage>> ProcessAsync(TExportRequest exportRequest, CancellationToken cancellationToken = default);
}
