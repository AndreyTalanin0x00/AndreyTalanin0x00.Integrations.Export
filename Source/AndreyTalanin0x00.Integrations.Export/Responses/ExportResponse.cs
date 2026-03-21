using AndreyTalanin0x00.Integrations.Blobs;
using AndreyTalanin0x00.Integrations.Export.Enumerations;

namespace AndreyTalanin0x00.Integrations.Export.Responses;

public class ExportResponse
{
    public required ExportStatus Status { get; set; }

    public required ExportResponseMessage[] Messages { get; set; }

    public required BlobReference BlobReference { get; set; }
}
