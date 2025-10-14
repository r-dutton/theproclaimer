[web] PUT /api/workpaper-records/{id:Guid}  (Workpapers.Next.API.Controllers.Workpapers.WorkpaperRecordsController.Update)  [L199–L225] [auth=AuthorizationPolicies.User]
  └─ calls WorkpaperRecordRepository.WriteQuery [L202]
  └─ writes_to WorkpaperRecord [L202]
    └─ reads_from WorkpaperRecords
  └─ uses_service IControlledRepository<WorkpaperRecord>
    └─ method WriteQuery [L202]
  └─ sends_request CanIAccessWorkpaperRecordQuery [L210]
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
  └─ sends_request GetWorkpaperRecordParamsQuery [L221]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Workpapers.Next.ApplicationService.Queries.WorkpaperRecords.GetWorkpaperRecordParamsQueryHandler.Handle [L76–L264]
      └─ uses_service IControlledRepository<Binder>
        └─ method ReadQuery [L153]
      └─ uses_service IControlledRepository<RecordStatus> (AddScoped)
        └─ method WriteQuery [L169]
      └─ uses_service IControlledRepository<SourceAccount>
        └─ method ReadQuery [L251]
      └─ uses_service IControlledRepository<WorkpaperRecordTemplate>
        └─ method ReadQuery [L204]
      └─ uses_service IControlledRepository<Worksheet>
        └─ method WriteQuery [L141]
      └─ uses_service RequestProcessor
        └─ method ProcessAsync [L189]
      └─ uses_service UserService
        └─ method GetUserId [L115]

