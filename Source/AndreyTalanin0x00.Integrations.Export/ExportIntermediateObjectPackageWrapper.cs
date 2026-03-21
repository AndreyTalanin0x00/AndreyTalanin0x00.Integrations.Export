namespace AndreyTalanin0x00.Integrations.Export;

public class ExportIntermediateObjectPackageWrapper<TExportIntermediateObjectPackage>
    where TExportIntermediateObjectPackage : class?
{
    public required TExportIntermediateObjectPackage ExportIntermediateObjectPackage { get; set; }
}
