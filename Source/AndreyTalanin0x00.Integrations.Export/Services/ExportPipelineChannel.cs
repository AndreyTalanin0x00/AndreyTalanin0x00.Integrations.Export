using System;
using System.Threading;
using System.Threading.Tasks;

using AndreyTalanin0x00.Integrations.Export.Requests;
using AndreyTalanin0x00.Integrations.Export.Responses;
using AndreyTalanin0x00.Integrations.Export.Services.Abstractions;

using Microsoft.Extensions.DependencyInjection;

// Disable the IDE0032 (Use auto property) notification to preserve easily recognizable 'injected services' field pattern.
#pragma warning disable IDE0032

namespace AndreyTalanin0x00.Integrations.Export.Services;

public class ExportPipelineChannel<TExportRequest, TExportResponse, TExportIntermediateObjectPackage, TExportIntermediateObjectPackageCurrent, TExportObjectPackage> :
    IExportPipelineChannel<TExportRequest, TExportResponse, TExportIntermediateObjectPackage, TExportObjectPackage>
    where TExportRequest : ExportRequest
    where TExportResponse : ExportResponse
    where TExportIntermediateObjectPackage : class
    where TExportIntermediateObjectPackageCurrent : class, TExportIntermediateObjectPackage
    where TExportObjectPackage : class
{
    private readonly ExportPipelineChannelKey m_exportPipelineChannelKey;
    private readonly IExportFormatter<TExportRequest, TExportResponse, TExportIntermediateObjectPackageCurrent, TExportObjectPackage> m_exportFormatter;
    private readonly IExportSerializer<TExportRequest, TExportResponse, TExportIntermediateObjectPackageCurrent, TExportObjectPackage> m_exportSerializer;
    private readonly IExportMapper<TExportRequest, TExportResponse, TExportIntermediateObjectPackageCurrent, TExportObjectPackage> m_exportMapper;

    public ExportPipelineChannel(
        [ServiceKey] ExportPipelineChannelKey exportPipelineChannelKey,
        IExportFormatter<TExportRequest, TExportResponse, TExportIntermediateObjectPackageCurrent, TExportObjectPackage> exportFormatter,
        IExportSerializer<TExportRequest, TExportResponse, TExportIntermediateObjectPackageCurrent, TExportObjectPackage> exportSerializer,
        IExportMapper<TExportRequest, TExportResponse, TExportIntermediateObjectPackageCurrent, TExportObjectPackage> exportMapper)
    {
        m_exportPipelineChannelKey = exportPipelineChannelKey;
        m_exportFormatter = exportFormatter;
        m_exportSerializer = exportSerializer;
        m_exportMapper = exportMapper;
    }

    /// <inheritdoc />
    public ExportPipelineChannelKey Key => m_exportPipelineChannelKey;

    /// <inheritdoc />
    public async Task<ExportTargetBatch> SerializeAsync(ExportObjectPackageBatch<TExportIntermediateObjectPackage, TExportObjectPackage> exportObjectPackageBatch, CancellationToken cancellationToken = default)
    {
        int size = exportObjectPackageBatch.Size;

        ExportTargetBatch exportTargetBatch;
        ExportIntermediateObjectPackageBatch<TExportIntermediateObjectPackageCurrent, TExportObjectPackage> exportIntermediateObjectPackageBatch;

        ExportTarget[] exportTargets = new ExportTarget[size];
        ExportTargetContext exportTargetContext = exportObjectPackageBatch.ExportTargetContext;

        ExportIntermediateObjectPackageWrapper<TExportIntermediateObjectPackageCurrent>[] exportIntermediateObjectPackageWrappers =
            new ExportIntermediateObjectPackageWrapper<TExportIntermediateObjectPackageCurrent>[size];
        ExportObjectPackageWrapper<TExportObjectPackage>[] exportObjectPackageWrappers =
            new ExportObjectPackageWrapper<TExportObjectPackage>[size];

        Array.Copy(exportObjectPackageBatch.ExportObjectPackageWrappers, exportObjectPackageWrappers, size);

        exportIntermediateObjectPackageBatch = new()
        {
            Size = size,
            ExportTargets = exportTargets,
            ExportTargetContext = exportTargetContext,
            ExportIntermediateObjectPackageWrappers = exportIntermediateObjectPackageWrappers,
            ExportObjectPackageWrappers = exportObjectPackageWrappers,
        };

        exportIntermediateObjectPackageBatch = m_exportMapper.Map(exportIntermediateObjectPackageBatch);

        exportIntermediateObjectPackageBatch = await m_exportSerializer.SerializeAsync(exportIntermediateObjectPackageBatch, cancellationToken);

        exportIntermediateObjectPackageBatch = await m_exportFormatter.FormatAsync(exportIntermediateObjectPackageBatch, cancellationToken);

        exportTargetBatch = new ExportTargetBatch()
        {
            Size = size,
            ExportTargets = exportIntermediateObjectPackageBatch.ExportTargets,
            ExportTargetContext = exportIntermediateObjectPackageBatch.ExportTargetContext,
        };

        return exportTargetBatch;
    }
}
