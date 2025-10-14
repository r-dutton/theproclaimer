[web] POST /api/reportBatch/lodge  (Cirrus.Api.Controllers.Accounting.Reports.ReportBatchLodgmentController.Lodge)  [L32–L42]
  └─ sends_request LodgeReportBatchCommand [L40]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Cirrus.ApplicationService.Accounting.Reports.Lodgement.LodgeReportBatchCommandHandler.Handle [L28–L83]
      └─ uses_service IControlledRepository<PublishedReportBatch>
        └─ method WriteQuery [L45]
      └─ uses_service IRequestProcessor (InstancePerDependency)
        └─ method ProcessAsync [L78]
    └─ handled_by Cirrus.Tests.Integration.Database.Accounting.Reports.TestLodgeReportBatchCommandHandler.Handle [L89–L108]
      └─ uses_service IControlledRepository<PublishedReportBatch>
        └─ method WriteQuery [L97]

