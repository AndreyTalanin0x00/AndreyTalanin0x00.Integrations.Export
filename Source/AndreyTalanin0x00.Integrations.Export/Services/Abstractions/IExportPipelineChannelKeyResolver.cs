using AndreyTalanin0x00.Integrations.Export.Requests;
using AndreyTalanin0x00.Integrations.Export.Responses;

namespace AndreyTalanin0x00.Integrations.Export.Services.Abstractions;

public interface IExportPipelineChannelKeyResolver<TExportRequest, TExportResponse>
    where TExportRequest : ExportRequest
    where TExportResponse : ExportResponse
{
    public ExportPipelineChannelKey ResolveExportPipelineChannelKey(TExportRequest exportRequest);
}
