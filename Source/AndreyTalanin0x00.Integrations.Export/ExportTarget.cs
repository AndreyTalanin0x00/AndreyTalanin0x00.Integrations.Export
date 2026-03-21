using AndreyTalanin0x00.Integrations.Blobs;

namespace AndreyTalanin0x00.Integrations.Export;

public class ExportTarget
{
    public required BlobReference BlobReference { get; set; }

    public required BlobMetadata BlobMetadata { get; set; }
}
