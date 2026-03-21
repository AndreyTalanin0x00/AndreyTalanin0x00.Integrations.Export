namespace AndreyTalanin0x00.Integrations.Export;

public class ExportTargetBatch
{
    public required int Size { get; set; }

    public required ExportTarget[] ExportTargets { get; set; }

    public required ExportTargetContext ExportTargetContext { get; set; }
}
