[web] GET /api/workpaper-records/{id:guid}/hyperlinks  (Workpapers.Next.API.Controllers.Workpapers.WorkpaperRecordsController.GetHyperlinks)  [L399–L423] [auth=AuthorizationPolicies.User]
  └─ maps_to HyperlinkDto [L411]
  └─ maps_to HyperlinkDto [L404]
  └─ calls WorkpaperRecordRepository.ReadQuery [L404]
  └─ queries WorkpaperRecord [L404]
    └─ reads_from WorkpaperRecords
  └─ uses_service IControlledRepository<WorkpaperRecord>
    └─ method ReadQuery [L404]
  └─ sends_request CanIAccessWorkpaperRecordQuery [L402]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Workpapers.Next.ApplicationService.Queries.WorkpaperRecords.CanIAccessWorkpaperRecordQueryHandler.Handle [L35–L59]
      └─ uses_service IControlledRepository<WorkpaperRecord>
        └─ method ReadQuery [L55]
      └─ uses_service RequestInfoService
        └─ method IsValidServiceAccountRequest [L53]
      └─ uses_service RequestProcessor
        └─ method ProcessAsync [L57]

