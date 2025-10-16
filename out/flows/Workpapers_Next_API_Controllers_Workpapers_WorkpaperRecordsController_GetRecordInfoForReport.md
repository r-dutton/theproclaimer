[web] GET /api/workpaper-records/for-report  (Workpapers.Next.API.Controllers.Workpapers.WorkpaperRecordsController.GetRecordInfoForReport)  [L146–L151] status=200 [auth=AuthorizationPolicies.User]
  └─ sends_request GetWorkpaperRecordInfoForReportQuery -> GetWorkpaperRecordInfoForReportQueryHandler [L150]
    └─ handled_by Workpapers.Next.ApplicationService.Queries.WorkpaperRecords.GetWorkpaperRecordInfoForReportQueryHandler.Handle [L32–L100]
      └─ maps_to WorkpaperRecordForReportDto [L55]
        └─ automapper.registration WorkpapersMappingProfile (WorkpaperRecord->WorkpaperRecordForReportDto) [L539]
      └─ uses_service MatterCountQueryProcessor
        └─ method ProcessMatterCounts [L72]
          └─ ... (no implementation details available)
      └─ uses_service IControlledRepository<WorkpaperRecord> (Scoped (inferred))
        └─ method ReadQuery [L55]
          └─ implementation Workpapers.Next.Data.Repository.Workpapers.WorkpaperRecordRepository.ReadQuery
      └─ uses_service RequestProcessor
        └─ method ProcessAsync [L53]
          └─ resolves_request Workpapers.Next.Services.Features.Requests.RequestProcessor.ProcessAsync
          └─ resolves_request Cirrus.Services.Features.Requests.RequestProcessor.ProcessAsync
          └─ resolves_request DataGet.Services.Features.Requests.RequestProcessor.ProcessAsync
          └─ +1 additional_requests elided
  └─ impact_summary
    └─ requests 1
      └─ GetWorkpaperRecordInfoForReportQuery
    └─ handlers 1
      └─ GetWorkpaperRecordInfoForReportQueryHandler
    └─ mappings 1
      └─ WorkpaperRecordForReportDto

