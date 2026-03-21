using System.Threading;
using System.Threading.Tasks;

using AndreyTalanin0x00.Integrations.Export.Requests;
using AndreyTalanin0x00.Integrations.Export.Responses;

namespace AndreyTalanin0x00.Integrations.Export.Services.Abstractions;

public interface IExportFormatter<TExportRequest, TExportResponse, TExportIntermediateObjectPackageCurrent, TExportObjectPackage>
    where TExportRequest : ExportRequest
    where TExportResponse : ExportResponse
    where TExportIntermediateObjectPackageCurrent : class
    where TExportObjectPackage : class
{
    public Task<ExportIntermediateObjectPackageBatch<TExportIntermediateObjectPackageCurrent, TExportObjectPackage>> FormatAsync(ExportIntermediateObjectPackageBatch<TExportIntermediateObjectPackageCurrent, TExportObjectPackage> exportIntermediateObjectPackageBatch, CancellationToken cancellationToken = default);
}
