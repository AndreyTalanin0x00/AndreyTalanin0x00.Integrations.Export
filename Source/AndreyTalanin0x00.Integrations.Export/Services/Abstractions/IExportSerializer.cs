using System.IO;
using System.Threading;
using System.Threading.Tasks;

using AndreyTalanin0x00.Integrations.Export.Requests;
using AndreyTalanin0x00.Integrations.Export.Responses;

namespace AndreyTalanin0x00.Integrations.Export.Services.Abstractions;

public interface IExportSerializer
{
    public void Serialize<TExportIntermediateObject>(Stream stream, TExportIntermediateObject exportIntermediateObject)
        where TExportIntermediateObject : class;

    public Task SerializeAsync<TExportIntermediateObject>(Stream stream, TExportIntermediateObject exportIntermediateObject, CancellationToken cancellationToken = default)
        where TExportIntermediateObject : class;
}

public interface IExportSerializer<TExportRequest, TExportResponse, TExportIntermediateObjectPackageCurrent, TExportObjectPackage> :
    IExportSerializer
    where TExportRequest : ExportRequest
    where TExportResponse : ExportResponse
    where TExportIntermediateObjectPackageCurrent : class
    where TExportObjectPackage : class
{
    public Task<ExportIntermediateObjectPackageBatch<TExportIntermediateObjectPackageCurrent, TExportObjectPackage>> SerializeAsync(ExportIntermediateObjectPackageBatch<TExportIntermediateObjectPackageCurrent, TExportObjectPackage> exportIntermediateObjectPackageBatch, CancellationToken cancellationToken = default);
}
