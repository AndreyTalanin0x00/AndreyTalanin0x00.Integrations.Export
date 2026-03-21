using System;

using AndreyTalanin0x00.Integrations.Export.Exceptions.Factories;
using AndreyTalanin0x00.Integrations.Export.Requests;
using AndreyTalanin0x00.Integrations.Export.Responses;
using AndreyTalanin0x00.Integrations.Export.Services.Abstractions;

namespace AndreyTalanin0x00.Integrations.Export.Services.Specialized;

public class PassThroughExportMapper<TExportRequest, TExportResponse, TExportIntermediateObjectPackageCurrent, TExportObjectPackage> :
    IExportMapper<TExportRequest, TExportResponse, TExportIntermediateObjectPackageCurrent, TExportObjectPackage>
    where TExportRequest : ExportRequest
    where TExportResponse : ExportResponse
    where TExportIntermediateObjectPackageCurrent : class
    where TExportObjectPackage : class
{
    /// <inheritdoc />
    public ExportIntermediateObjectPackageBatch<TExportIntermediateObjectPackageCurrent, TExportObjectPackage> Map(ExportIntermediateObjectPackageBatch<TExportIntermediateObjectPackageCurrent, TExportObjectPackage> exportIntermediateObjectPackageBatch)
    {
        if (typeof(TExportIntermediateObjectPackageCurrent) == typeof(TExportObjectPackage))
        {
            int size = exportIntermediateObjectPackageBatch.Size;

            ExportIntermediateObjectPackageWrapper<TExportIntermediateObjectPackageCurrent>[] exportIntermediateObjectPackageWrappers =
                exportIntermediateObjectPackageBatch.ExportIntermediateObjectPackageWrappers;
            ExportObjectPackageWrapper<TExportObjectPackage>[] exportObjectPackageWrappers =
                exportIntermediateObjectPackageBatch.ExportObjectPackageWrappers;

            if (size != exportIntermediateObjectPackageWrappers.Length)
                throw ExportExceptionFactory.CreateExportIntermediateObjectPackageWrapperCountMismatchException<TExportRequest, TExportResponse, TExportIntermediateObjectPackageCurrent, TExportObjectPackage>();
            if (size != exportObjectPackageWrappers.Length)
                throw ExportExceptionFactory.CreateExportObjectPackageWrapperCountMismatchException<TExportRequest, TExportResponse, TExportIntermediateObjectPackageCurrent, TExportObjectPackage>();

            for (int index = 0; index < size; index++)
            {
                const string passThroughExportMapperClassName =
                    nameof(PassThroughExportMapper<TExportRequest, TExportResponse, TExportIntermediateObjectPackageCurrent, TExportObjectPackage>);

                ExportIntermediateObjectPackageWrapper<TExportIntermediateObjectPackageCurrent> exportIntermediateObjectPackageWrapper =
                    (object)exportIntermediateObjectPackageBatch.ExportObjectPackageWrappers[index] as ExportIntermediateObjectPackageWrapper<TExportIntermediateObjectPackageCurrent>
                    ?? throw new InvalidCastException($"Unable to use the {passThroughExportMapperClassName} export mapper implementation, export object package types are different.");

                exportIntermediateObjectPackageBatch.ExportIntermediateObjectPackageWrappers[index] = exportIntermediateObjectPackageWrapper;
            }
        }

        return exportIntermediateObjectPackageBatch;
    }
}
