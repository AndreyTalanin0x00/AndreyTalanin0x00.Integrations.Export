# AndreyTalanin0x00.Integrations.Export

A set of abstractions to add generic export pipeline capabilities to a .NET application.

Core services include:

- `IExportPipeline` - Defines the contract for an export pipeline that processes export requests and produces export responses.
- `IExportPipelineChannel` - Defines the contract for a channel within an export pipeline that can process intermediate object packages. Different channels handle different file formats (e.g., XML, CSV).
- `IExportPipelineChannelKeyResolver` - Resolves the appropriate channel key based on the export request.
- `IExportProcessor` - Prepares application data for export by processing the export request and returning an object package with application-level DTO models.
- `IExportWriter` - Writes a final blob containing a serialized and formatted file.

An export pipeline channel consists of multiple services:

- `IExportMapper` - Maps an application-level DTO model package to a serializable model package.
- `IExportSerializer` - Serializes a serializable model package into a blob of a specific file format (e.g., XML, CSV).
- `IExportFormatter` - Formats a serialized file, adds headers or footers, XML comments, or other metadata.

The export pipeline can be configured in a Fluent API style, allowing for easy setup and customization of export channels and their associated services:

```csharp
// This extension method is taken from the 'JapaneseLanguageTools' project, where it is used to set up an export pipeline for exporting the application dictionary in both JSON and XML formats.
// See the original source code here: https://github.com/AndreyTalanin0x00/JapaneseLanguageTools/blob/development-preview/Source/JapaneseLanguageTools.Core.Export/Extensions/ServiceCollectionExtensions.cs
public static IServiceCollection AddApplicationDictionaryExportPipeline(this IServiceCollection services)
{
    services.AddExportPipeline<ApplicationDictionaryExportRequest, ApplicationDictionaryExportResponse, Object, ApplicationDictionaryObjectPackageIntegrationModel>(exportPipelineOptionsBuilder =>
    {
        exportPipelineOptionsBuilder
            .UseExportWriter<ApplicationDictionaryExportWriter>()
            .AddExportPipelineChannel<ApplicationDictionaryObjectPackageJsonModel>(exportPipelineChannelOptionsBuilder =>
            {
                exportPipelineChannelOptionsBuilder
                    .UseKey(ApplicationDictionaryExportPipelineChannelKeys.ApplicationDictionaryExportPipelineChannelKeyJson)
                    .UseExportFormatter<ApplicationDictionaryJsonExportFormatter>()
                    .UseExportSerializer<ApplicationDictionaryJsonExportSerializer>()
                    .UseExportMapper<ApplicationDictionaryJsonExportMapper>();
            })
            .AddExportPipelineChannel<ApplicationDictionaryObjectPackageXmlModel>(exportPipelineChannelOptionsBuilder =>
            {
                exportPipelineChannelOptionsBuilder
                    .UseKey(ApplicationDictionaryExportPipelineChannelKeys.ApplicationDictionaryExportPipelineChannelKeyXml)
                    .UseExportFormatter<ApplicationDictionaryXmlExportFormatter>()
                    .UseExportSerializer<ApplicationDictionaryXmlExportSerializer>()
                    .UseExportMapper<ApplicationDictionaryXmlExportMapper>();
            })
            .UseExportPipelineChannelKeyResolver<ApplicationDictionaryExportPipelineChannelKeyResolver>()
            .UseExportProcessor<ApplicationDictionaryExportProcessor>();
    });

    return services;
}
```
