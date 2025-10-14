[web] PUT /api/workpaper-records/bulk  (Workpapers.Next.API.Controllers.Workpapers.WorkpaperRecordsController.BulkUpdate)  [L273–L298] [auth=AuthorizationPolicies.User]
  └─ calls WorkpaperRecordRepository.WriteQuery [L276]
  └─ writes_to WorkpaperRecord [L276]
    └─ reads_from WorkpaperRecords
  └─ uses_service IControlledRepository<WorkpaperRecord>
    └─ method WriteQuery [L276]
  └─ sends_request CanIAccessBinderQuery [L284]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Workpapers.Next.ApplicationService.Queries.Binders.CanIAccessBinderQueryHandler.Handle [L60–L126]
      └─ uses_service IControlledRepository<Binder>
        └─ method ReadQuery [L101]
      └─ uses_service RequestInfoService
        └─ method IsValidServiceAccountRequest [L89]
      └─ uses_service RequestProcessor
        └─ method ProcessAsync [L117]
      └─ uses_service TenantService
        └─ method GetCurrentTenant [L92]
      └─ uses_service UserService
        └─ method GetUserId [L91]
      └─ uses_cache IDistributedCache [L121]
        └─ method SetRecordAsync [write] [L121]
      └─ uses_cache IDistributedCache [L109]
        └─ method DoesRecordExistAsync [access] [L109]
      └─ uses_cache IDistributedCache [L92]
        └─ method CreateAccessKey [write] [L92]
  └─ sends_request GetWorkpaperRecordParamsQuery [L295]
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

