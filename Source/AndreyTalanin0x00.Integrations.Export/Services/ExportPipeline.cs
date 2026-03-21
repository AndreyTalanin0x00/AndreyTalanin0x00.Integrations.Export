using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using AndreyTalanin0x00.Integrations.Export.Exceptions.Factories;
using AndreyTalanin0x00.Integrations.Export.Requests;
using AndreyTalanin0x00.Integrations.Export.Responses;
using AndreyTalanin0x00.Integrations.Export.Services.Abstractions;

// Disable the IDE0032 (Use auto property) notification to preserve easily recognizable 'injected services' field pattern.
#pragma warning disable IDE0032

namespace AndreyTalanin0x00.Integrations.Export.Services;

public class ExportPipeline<TExportRequest, TExportResponse, TExportIntermediateObjectPackage, TExportObjectPackage> :
    IExportPipeline<TExportRequest, TExportResponse>
    where TExportRequest : ExportRequest
    where TExportResponse : ExportResponse
    where TExportIntermediateObjectPackage : class
    where TExportObjectPackage : class
{
    private readonly IExportWriter<TExportRequest, TExportResponse> m_exportWriter;
    private readonly IExportPipelineChannelKeyResolver<TExportRequest, TExportResponse> m_exportPipelineChannelKeyResolver;
    private readonly List<IExportPipelineChannel<TExportRequest, TExportResponse, TExportIntermediateObjectPackage, TExportObjectPackage>> m_exportPipelineChannels;
    private readonly Dictionary<ExportPipelineChannelKey, IExportPipelineChannel<TExportRequest, TExportResponse, TExportIntermediateObjectPackage, TExportObjectPackage>> m_exportPipelineChannelsDictionary;
    private readonly IExportProcessor<TExportRequest, TExportResponse, TExportIntermediateObjectPackage, TExportObjectPackage> m_exportProcessor;

    public ExportPipeline(
        IExportWriter<TExportRequest, TExportResponse> exportWriter,
        IExportPipelineChannelKeyResolver<TExportRequest, TExportResponse> exportPipelineChannelKeyResolver,
        IEnumerable<IExportPipelineChannel<TExportRequest, TExportResponse, TExportIntermediateObjectPackage, TExportObjectPackage>> exportPipelineChannels,
        IExportProcessor<TExportRequest, TExportResponse, TExportIntermediateObjectPackage, TExportObjectPackage> exportProcessor)
    {
        m_exportWriter = exportWriter;
        m_exportPipelineChannelKeyResolver = exportPipelineChannelKeyResolver;
        m_exportPipelineChannels = [.. exportPipelineChannels];
        m_exportPipelineChannelsDictionary = exportPipelineChannels.ToDictionary(exportPipelineChannel => exportPipelineChannel.Key);
        m_exportProcessor = exportProcessor;
    }

    protected IExportWriter<TExportRequest, TExportResponse> ExportWriter => m_exportWriter;

    protected IExportPipelineChannelKeyResolver<TExportRequest, TExportResponse> ExportPipelineChannelKeyResolver => m_exportPipelineChannelKeyResolver;

    protected IEnumerable<IExportPipelineChannel<TExportRequest, TExportResponse, TExportIntermediateObjectPackage, TExportObjectPackage>> ExportPipelineChannels => m_exportPipelineChannels;

    protected IExportProcessor<TExportRequest, TExportResponse, TExportIntermediateObjectPackage, TExportObjectPackage> ExportProcessor => m_exportProcessor;

    /// <inheritdoc />
    public async Task<TExportResponse> ExportAsync(TExportRequest exportRequest, CancellationToken cancellationToken = default)
    {
        ExportObjectPackageBatch<TExportIntermediateObjectPackage, TExportObjectPackage> exportObjectPackageBatch =
            await ProcessAsync(exportRequest, cancellationToken);

        ExportTargetBatch exportTargetBatch =
            await SerializeAsync(exportObjectPackageBatch, exportRequest, cancellationToken);

        TExportResponse exportResponse =
            await WriteAsync(exportTargetBatch, exportRequest, cancellationToken);

        return exportResponse;
    }

    protected virtual async Task<ExportObjectPackageBatch<TExportIntermediateObjectPackage, TExportObjectPackage>> ProcessAsync(TExportRequest exportRequest, CancellationToken cancellationToken = default)
    {
        ExportObjectPackageBatch<TExportIntermediateObjectPackage, TExportObjectPackage> exportObjectPackageBatches =
            await m_exportProcessor.ProcessAsync(exportRequest, cancellationToken);

        return exportObjectPackageBatches;
    }

    protected virtual async Task<ExportTargetBatch> SerializeAsync(ExportObjectPackageBatch<TExportIntermediateObjectPackage, TExportObjectPackage> exportObjectPackageBatch, TExportRequest exportRequest, CancellationToken cancellationToken = default)
    {
        ExportPipelineChannelKey exportPipelineChannelKey =
            m_exportPipelineChannelKeyResolver.ResolveExportPipelineChannelKey(exportRequest);

#pragma warning disable IDE0018 // Inline variable declaration (If the variable is inlined, the line becomes unnecessarily too long.)
        IExportPipelineChannel<TExportRequest, TExportResponse, TExportIntermediateObjectPackage, TExportObjectPackage>? exportPipelineChannel;
        if (!m_exportPipelineChannelsDictionary.TryGetValue(exportPipelineChannelKey, out exportPipelineChannel))
            throw ExportExceptionFactory.CreateNoSuitableExportPipelineChannelException<TExportRequest, TExportResponse, TExportIntermediateObjectPackage, TExportObjectPackage>(exportPipelineChannelKey);
#pragma warning restore IDE0018 // Inline variable declaration

        ExportTargetBatch exportTargetBatch =
            await exportPipelineChannel.SerializeAsync(exportObjectPackageBatch, cancellationToken);

        return exportTargetBatch;
    }

    protected virtual async Task<TExportResponse> WriteAsync(ExportTargetBatch exportTargetBatch, TExportRequest exportRequest, CancellationToken cancellationToken = default)
    {
        TExportResponse exportResponse =
            await m_exportWriter.WriteAsync(exportTargetBatch, exportRequest, cancellationToken);

        return exportResponse;
    }
}
