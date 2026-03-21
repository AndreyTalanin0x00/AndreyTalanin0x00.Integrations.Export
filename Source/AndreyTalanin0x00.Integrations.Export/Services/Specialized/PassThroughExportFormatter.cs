using System.Threading;
using System.Threading.Tasks;

using AndreyTalanin0x00.Integrations.Export.Requests;
using AndreyTalanin0x00.Integrations.Export.Responses;
using AndreyTalanin0x00.Integrations.Export.Services.Abstractions;

namespace AndreyTalanin0x00.Integrations.Export.Services.Specialized;

public class PassThroughExportFormatter<TExportRequest, TExportResponse, TExportIntermediateObjectPackageCurrent, TExportObjectPackage> :
    IExportFormatter<TExportRequest, TExportResponse, TExportIntermediateObjectPackageCurrent, TExportObjectPackage>
    where TExportRequest : ExportRequest
    where TExportResponse : ExportResponse
    where TExportIntermediateObjectPackageCurrent : class
    where TExportObjectPackage : class
{
    /// <inheritdoc />
    public Task<ExportIntermediateObjectPackageBatch<TExportIntermediateObjectPackageCurrent, TExportObjectPackage>> FormatAsync(ExportIntermediateObjectPackageBatch<TExportIntermediateObjectPackageCurrent, TExportObjectPackage> exportIntermediateObjectPackageBatch, CancellationToken cancellationToken = default)
    {
        Task<ExportIntermediateObjectPackageBatch<TExportIntermediateObjectPackageCurrent, TExportObjectPackage>> completedTask =
            Task.FromResult(exportIntermediateObjectPackageBatch);

        return completedTask;
    }
}
