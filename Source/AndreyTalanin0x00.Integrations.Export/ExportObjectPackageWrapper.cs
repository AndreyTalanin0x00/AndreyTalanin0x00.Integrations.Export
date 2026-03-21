namespace AndreyTalanin0x00.Integrations.Export;

public class ExportObjectPackageWrapper<TExportObjectPackage>
    where TExportObjectPackage : class?
{
    public required TExportObjectPackage ExportObjectPackage { get; set; }
}
