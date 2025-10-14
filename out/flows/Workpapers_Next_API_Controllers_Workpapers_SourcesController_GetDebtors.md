[web] GET /api/sources/{type}/debtors  (Workpapers.Next.API.Controllers.Workpapers.SourcesController.GetDebtors)  [L312–L339] [auth=AuthorizationPolicies.User]
  └─ calls WorkpaperRecordRepository.ReadQuery [L330]
  └─ queries WorkpaperRecord [L330]
    └─ reads_from WorkpaperRecords
  └─ uses_service IConnectionApiService (AddSingleton)
    └─ method GetApiMethods [L336]
  └─ uses_service IControlledRepository<WorkpaperRecord>
    └─ method ReadQuery [L330]
  └─ sends_request CanIAccessBinderQuery [L317]
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

