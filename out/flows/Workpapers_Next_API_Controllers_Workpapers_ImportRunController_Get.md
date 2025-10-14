[web] GET /api/import-runs/{id:guid}  (Workpapers.Next.API.Controllers.Workpapers.ImportRunController.Get)  [L51–L64] [auth=AuthorizationPolicies.User]
  └─ maps_to JournalLightDto [L60]
  └─ maps_to ImportRunDto [L54]
    └─ automapper.registration CirrusMappingProfile (ImportRun->ImportRunDto) [L517]
    └─ automapper.registration WorkpapersMappingProfile (ImportRun->ImportRunDto) [L817]
  └─ calls ImportRunRepository.ReadQuery [L54]
  └─ queries ImportRun [L54]
    └─ reads_from ImportRuns
  └─ uses_service IControlledRepository<ImportRun>
    └─ method ReadQuery [L54]
  └─ uses_service IMapper
    └─ method Map [L60]
  └─ sends_request GetImportRunJournalsQuery [L59]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Cirrus.ApplicationService.Accounting.Queries.Ledger.GetImportRunJournalsQueryHandler.Handle [L28–L83]
      └─ maps_to JournalCreateModel [L59]
        └─ converts_to JournalLightDto [L861]
        └─ converts_to JournalLightDto [L491]
  └─ sends_request CanIAccessBinderQuery [L57]
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

