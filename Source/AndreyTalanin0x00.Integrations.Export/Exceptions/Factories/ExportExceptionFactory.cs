using System;

using AndreyTalanin0x00.Integrations.Export.Requests;
using AndreyTalanin0x00.Integrations.Export.Responses;
using AndreyTalanin0x00.Integrations.Export.Services.Abstractions;

namespace AndreyTalanin0x00.Integrations.Export.Exceptions.Factories;

public static class ExportExceptionFactory
{
    public static InvalidOperationException CreateNoSuitableExportPipelineChannelException<TExportRequest, TExportResponse, TExportIntermediateObjectPackage, TExportObjectPackage>(ExportPipelineChannelKey exportPipelineChannelKey)
        where TExportRequest : ExportRequest
        where TExportResponse : ExportResponse
        where TExportIntermediateObjectPackage : class
        where TExportObjectPackage : class
    {
        const string exportPipelineChannelInterfaceName =
            nameof(IExportPipelineChannel<TExportRequest, TExportResponse, TExportIntermediateObjectPackage, TExportObjectPackage>);

        const string exceptionMessageFormat = ""
            + "Could not find a suitable " + exportPipelineChannelInterfaceName + " for this export. "
            + "The export pipeline channel key that could not be handled: {0}. "
            + "Generic parameters: {1}, {2}, {3}, {4}. "
            + "Check export services (dependency injection container configuration).";

        string exceptionMessage = string.Format(exceptionMessageFormat, exportPipelineChannelKey.Value, typeof(TExportRequest), typeof(TExportResponse), typeof(TExportIntermediateObjectPackage), typeof(TExportObjectPackage));

        InvalidOperationException exception = new(exceptionMessage);

        return exception;
    }

    public static ArgumentException CreateExportIntermediateObjectPackageWrapperCountMismatchException<TExportRequest, TExportResponse, TExportIntermediateObjectPackageCurrent, TExportObjectPackage>()
        where TExportRequest : ExportRequest
        where TExportResponse : ExportResponse
        where TExportIntermediateObjectPackageCurrent : class
        where TExportObjectPackage : class
    {
        const string exceptionMessageFormat = ""
            + "Could not process an export intermediate object package batch. "
            + "The object was not created correctly: arrays' lengths do not match. "
            + "Generic parameters: {0}, {1}, {2}, {3}. "
            + "Check export services (API consumer services - export pipeline channel).";

        string exceptionMessage = string.Format(exceptionMessageFormat, typeof(TExportRequest), typeof(TExportResponse), typeof(TExportIntermediateObjectPackageCurrent), typeof(TExportObjectPackage));

        ArgumentException exception = new(exceptionMessage);

        return exception;
    }

    public static ArgumentException CreateExportObjectPackageWrapperCountMismatchException<TExportRequest, TExportResponse, TExportIntermediateObjectPackageCurrent, TExportObjectPackage>()
        where TExportRequest : ExportRequest
        where TExportResponse : ExportResponse
        where TExportIntermediateObjectPackageCurrent : class
        where TExportObjectPackage : class
    {
        const string exceptionMessageFormat = ""
            + "Could not process an export object package batch. "
            + "The object was not created correctly: arrays' lengths do not match. "
            + "Generic parameters: {0}, {1}, {2}, {3}. "
            + "Check export services (API consumer services - export pipeline channel).";

        string exceptionMessage = string.Format(exceptionMessageFormat, typeof(TExportRequest), typeof(TExportResponse), typeof(TExportIntermediateObjectPackageCurrent), typeof(TExportObjectPackage));

        ArgumentException exception = new(exceptionMessage);

        return exception;
    }
}
