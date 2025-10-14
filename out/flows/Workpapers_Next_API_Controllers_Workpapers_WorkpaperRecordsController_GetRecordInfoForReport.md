[web] GET /api/workpaper-records/for-report  (Workpapers.Next.API.Controllers.Workpapers.WorkpaperRecordsController.GetRecordInfoForReport)  [L146–L151] [auth=AuthorizationPolicies.User]
  └─ sends_request GetWorkpaperRecordInfoForReportQuery [L150]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Workpapers.Next.ApplicationService.Queries.WorkpaperRecords.GetWorkpaperRecordInfoForReportQueryHandler.Handle [L32–L100]
      └─ maps_to WorkpaperRecordForReportDto [L55]
        └─ automapper.registration WorkpapersMappingProfile (WorkpaperRecord->WorkpaperRecordForReportDto) [L539]
      └─ uses_service IControlledRepository<WorkpaperRecord>
        └─ method ReadQuery [L55]
      └─ uses_service IMapper
        └─ method ConfigurationProvider [L58]
      └─ uses_service MatterCountQueryProcessor
        └─ method ProcessMatterCounts [L72]
      └─ uses_service RequestProcessor
        └─ method ProcessAsync [L53]

