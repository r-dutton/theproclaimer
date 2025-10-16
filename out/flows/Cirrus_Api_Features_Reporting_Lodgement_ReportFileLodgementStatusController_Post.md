[web] POST /api/reportFile/lodgement/status  (Cirrus.Api.Features.Reporting.Lodgement.ReportFileLodgementStatusController.Post)  [L14–L22] status=201
  └─ sends_request UpdateReportFileLodgementStatusCommand [L19]
    └─ handled_by Cirrus.ApplicationService.Accounting.Reports.Lodgement.UpdateReportFileLodgementStatusCommandHandler.Handle [L30–L65]
      └─ uses_service IControlledRepository<PublishedReportFile>
        └─ method WriteQuery [L49]
          └─ ... (no implementation details available)
    └─ handled_by Cirrus.Tests.Integration.Database.Accounting.Reports.TestUpdateReportFileLodgementStatusCommandHandler.Handle [L115–L126]
      └─ uses_service IControlledRepository<PublishedReportFile>
        └─ method WriteQuery [L121]
          └─ ... (no implementation details available)

