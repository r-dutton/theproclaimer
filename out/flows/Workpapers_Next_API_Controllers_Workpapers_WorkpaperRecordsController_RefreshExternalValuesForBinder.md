[web] PUT /api/workpaper-records/external-values/refresh/{binderId:guid}  (Workpapers.Next.API.Controllers.Workpapers.WorkpaperRecordsController.RefreshExternalValuesForBinder)  [L227–L242] [auth=AuthorizationPolicies.User]
  └─ calls BinderRepository.ReadQuery [L230]
  └─ queries Binder [L230]
    └─ reads_from Binders
  └─ uses_service IControlledRepository<Binder>
    └─ method ReadQuery [L230]
  └─ sends_request RefreshExternalValuesCommand [L239]
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
  └─ sends_request CanIAccessBinderQuery [L238]
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

