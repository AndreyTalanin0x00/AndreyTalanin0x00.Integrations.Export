using System.Threading;
using System.Threading.Tasks;

using AndreyTalanin0x00.Integrations.Export.Requests;
using AndreyTalanin0x00.Integrations.Export.Responses;

namespace AndreyTalanin0x00.Integrations.Export.Services.Abstractions;

public interface IExportPipeline<TExportRequest, TExportResponse>
    where TExportRequest : ExportRequest
    where TExportResponse : ExportResponse
{
    public Task<TExportResponse> ExportAsync(TExportRequest exportRequest, CancellationToken cancellationToken = default);
}
