[web] PUT /api/workpaper-records/{id:guid}/external-value/refresh  (Workpapers.Next.API.Controllers.Workpapers.WorkpaperRecordsController.RefreshRecordExternalValue)  [L250–L271] [auth=AuthorizationPolicies.User]
  └─ calls BinderRepository.ReadQuery [L259]
  └─ calls WorkpaperRecordRepository.ReadQuery [L253]
  └─ queries Binder [L259]
    └─ reads_from Binders
  └─ queries WorkpaperRecord [L253]
    └─ reads_from WorkpaperRecords
  └─ uses_service IControlledRepository<Binder>
    └─ method ReadQuery [L259]
  └─ uses_service IControlledRepository<WorkpaperRecord>
    └─ method ReadQuery [L253]
  └─ sends_request RefreshExternalValuesCommand [L268]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Workpapers.Next.ApplicationService.Commands.Workpapers.WorkpaperRecords.RefreshExternalValuesCommandHandler.Handle [L43–L282]
      └─ uses_service ICirrusQueryService (AddScoped)
        └─ method GetDataset [L225]
      └─ uses_service IControlledRepository<Binder>
        └─ method ReadQuery [L125]
      └─ uses_service IControlledRepository<WorkpaperRecord>
        └─ method WriteQuery [L70]
      └─ uses_service IControlledRepository<Worksheet>
        └─ method ReadQuery [L116]
      └─ uses_service RequestProcessor
        └─ method ProcessAsync [L68]
  └─ sends_request CanIAccessBinderQuery [L267]
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

