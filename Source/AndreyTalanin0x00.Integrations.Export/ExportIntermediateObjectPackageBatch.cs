namespace AndreyTalanin0x00.Integrations.Export;

public class ExportIntermediateObjectPackageBatch<TExportIntermediateObjectPackage, TExportObjectPackage>
    where TExportIntermediateObjectPackage : class
    where TExportObjectPackage : class
{
    public required int Size { get; set; }

    public required ExportTarget[] ExportTargets { get; set; }

    public required ExportIntermediateObjectPackageWrapper<TExportIntermediateObjectPackage>[] ExportIntermediateObjectPackageWrappers { get; set; }

    public required ExportObjectPackageWrapper<TExportObjectPackage>[] ExportObjectPackageWrappers { get; set; }

    public required ExportTargetContext ExportTargetContext { get; set; }
}
