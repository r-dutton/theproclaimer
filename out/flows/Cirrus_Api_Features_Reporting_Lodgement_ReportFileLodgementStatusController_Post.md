[web] POST /api/reportFile/lodgement/status  (Cirrus.Api.Features.Reporting.Lodgement.ReportFileLodgementStatusController.Post)  [L14–L22]
  └─ sends_request UpdateReportFileLodgementStatusCommand [L19]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Cirrus.ApplicationService.Accounting.Reports.Lodgement.UpdateReportFileLodgementStatusCommandHandler.Handle [L30–L65]
      └─ uses_service IControlledRepository<PublishedReportFile>
        └─ method WriteQuery [L49]
    └─ handled_by Cirrus.Tests.Integration.Database.Accounting.Reports.TestUpdateReportFileLodgementStatusCommandHandler.Handle [L115–L126]
      └─ uses_service IControlledRepository<PublishedReportFile>
        └─ method WriteQuery [L121]

