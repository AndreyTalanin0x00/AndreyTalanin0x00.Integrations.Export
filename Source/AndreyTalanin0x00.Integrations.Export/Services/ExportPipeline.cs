using System;
using System.Threading;
using System.Threading.Tasks;

using AndreyTalanin0x00.Integrations.Export.Requests;
using AndreyTalanin0x00.Integrations.Export.Responses;
using AndreyTalanin0x00.Integrations.Export.Services.Abstractions;

namespace AndreyTalanin0x00.Integrations.Export.Services;

public class ExportPipeline<TExportRequest, TExportResponse, TExportIntermediateObjectPackage, TExportObjectPackage> :
    IExportPipeline<TExportRequest, TExportResponse>
    where TExportRequest : ExportRequest
    where TExportResponse : ExportResponse
    where TExportIntermediateObjectPackage : class
    where TExportObjectPackage : class
{
    /// <inheritdoc />
    public Task<TExportResponse> ExportAsync(TExportRequest exportRequest, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}
