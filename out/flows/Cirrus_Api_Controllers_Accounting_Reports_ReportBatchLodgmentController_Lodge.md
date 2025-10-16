[web] POST /api/reportBatch/lodge  (Cirrus.Api.Controllers.Accounting.Reports.ReportBatchLodgmentController.Lodge)  [L32–L42] status=201
  └─ sends_request LodgeReportBatchCommand -> TestLodgeReportBatchCommandHandler [L40]
    └─ handled_by Cirrus.Tests.Integration.Database.Accounting.Reports.TestLodgeReportBatchCommandHandler.Handle [L89–L108]
      └─ calls PublishedReportBatchRepository.WriteQuery [L97]
    └─ handled_by Cirrus.ApplicationService.Accounting.Reports.Lodgement.LodgeReportBatchCommandHandler.Handle [L28–L83]
      └─ uses_service IRequestProcessor (InstancePerDependency)
        └─ method ProcessAsync [L78]
          └─ implementation DataGet.Services.Features.Requests.RequestProcessor.ProcessAsync [L7-L35]
            └─ ... (no dispatches detected)
      └─ uses_service IControlledRepository<PublishedReportBatch> (Scoped (inferred))
        └─ method WriteQuery [L45]
          └─ implementation Cirrus.Data.Repository.Accounting.Report.PublishedReportBatchRepository.WriteQuery
  └─ impact_summary
    └─ requests 1
      └─ LodgeReportBatchCommand
    └─ handlers 2
      └─ LodgeReportBatchCommandHandler
      └─ TestLodgeReportBatchCommandHandler

