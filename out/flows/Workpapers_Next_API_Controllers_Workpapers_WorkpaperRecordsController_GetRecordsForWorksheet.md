[web] GET /api/workpaper-records/for-worksheet  (Workpapers.Next.API.Controllers.Workpapers.WorkpaperRecordsController.GetRecordsForWorksheet)  [L153–L165] [auth=AuthorizationPolicies.User]
  └─ maps_to WorkpaperRecordLightDto [L159]
    └─ automapper.registration WorkpapersMappingProfile (WorkpaperRecord->WorkpaperRecordLightDto) [L515]
  └─ calls WorkpaperRecordRepository.ReadQuery [L159]
  └─ queries WorkpaperRecord [L159]
    └─ reads_from WorkpaperRecords
  └─ uses_service IControlledRepository<WorkpaperRecord>
    └─ method ReadQuery [L159]
  └─ sends_request CanIAccessBinderQuery [L157]
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

