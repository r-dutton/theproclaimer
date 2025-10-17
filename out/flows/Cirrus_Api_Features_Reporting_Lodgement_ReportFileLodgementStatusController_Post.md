[web] POST /api/reportFile/lodgement/status  (Cirrus.Api.Features.Reporting.Lodgement.ReportFileLodgementStatusController.Post)  [L14–L22] status=201
  └─ sends_request UpdateReportFileLodgementStatusCommand -> TestUpdateReportFileLodgementStatusCommandHandler [L19]
    └─ handled_by Cirrus.Tests.Integration.Database.Accounting.Reports.TestUpdateReportFileLodgementStatusCommandHandler.Handle [L115–L126]
      └─ calls PublishedReportFileRepository.WriteQuery [L121]
    └─ handled_by Cirrus.ApplicationService.Accounting.Reports.Lodgement.UpdateReportFileLodgementStatusCommandHandler.Handle [L30–L65]
      └─ uses_service IControlledRepository<PublishedReportFile> (Scoped (inferred))
        └─ method WriteQuery [L49]
          └─ implementation Cirrus.Data.Repository.Accounting.Report.PublishedReportFileRepository.WriteQuery
  └─ impact_summary
    └─ requests 1
      └─ UpdateReportFileLodgementStatusCommand
    └─ handlers 2
      └─ TestUpdateReportFileLodgementStatusCommandHandler
      └─ UpdateReportFileLodgementStatusCommandHandler

