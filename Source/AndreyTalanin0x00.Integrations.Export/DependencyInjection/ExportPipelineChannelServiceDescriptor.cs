using System;

using Microsoft.Extensions.DependencyInjection;

namespace AndreyTalanin0x00.Integrations.Export.DependencyInjection;

internal class ExportPipelineChannelServiceDescriptor
{
    public required ExportPipelineChannelKey? ExportPipelineChannelKey { get; set; }

    public required Action<IServiceCollection> AddExportPipelineChannelServiceCollectionVisitor { get; set; }
}
