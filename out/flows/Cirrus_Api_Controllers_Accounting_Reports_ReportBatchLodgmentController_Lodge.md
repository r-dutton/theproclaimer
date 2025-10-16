[web] POST /api/reportBatch/lodge  (Cirrus.Api.Controllers.Accounting.Reports.ReportBatchLodgmentController.Lodge)  [L32–L42] status=201
  └─ sends_request LodgeReportBatchCommand [L40]
    └─ handled_by Cirrus.ApplicationService.Accounting.Reports.Lodgement.LodgeReportBatchCommandHandler.Handle [L28–L83]
      └─ uses_service IControlledRepository<PublishedReportBatch>
        └─ method WriteQuery [L45]
          └─ ... (no implementation details available)
      └─ uses_service IRequestProcessor (InstancePerDependency)
        └─ method ProcessAsync [L78]
          └─ implementation IRequestProcessor.ProcessAsync [L9-L9]
          └─ ... (no implementation details available)
    └─ handled_by Cirrus.Tests.Integration.Database.Accounting.Reports.TestLodgeReportBatchCommandHandler.Handle [L89–L108]
      └─ uses_service IControlledRepository<PublishedReportBatch>
        └─ method WriteQuery [L97]
          └─ ... (no implementation details available)

