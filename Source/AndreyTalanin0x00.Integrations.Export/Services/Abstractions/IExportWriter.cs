using System.Threading;
using System.Threading.Tasks;

using AndreyTalanin0x00.Integrations.Export.Requests;
using AndreyTalanin0x00.Integrations.Export.Responses;

namespace AndreyTalanin0x00.Integrations.Export.Services.Abstractions;

public interface IExportWriter<TExportRequest, TExportResponse>
    where TExportRequest : ExportRequest
    where TExportResponse : ExportResponse
{
    public Task<TExportResponse> WriteAsync(ExportTargetBatch exportTargetBatch, TExportRequest exportRequest, CancellationToken cancellationToken = default);
}
