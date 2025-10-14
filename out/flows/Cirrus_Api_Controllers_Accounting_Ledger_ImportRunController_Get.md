[web] GET /api/accounting/ledger/import-runs/{id:int}  (Cirrus.Api.Controllers.Accounting.Ledger.ImportRunController.Get)  [L50–L62] [auth=user]
  └─ maps_to JournalLightDto [L58]
  └─ maps_to ImportRunDto [L53]
    └─ automapper.registration CirrusMappingProfile (ImportRun->ImportRunDto) [L517]
    └─ automapper.registration WorkpapersMappingProfile (ImportRun->ImportRunDto) [L817]
  └─ calls ImportRunRepository.ReadQuery [L53]
  └─ queries ImportRun [L53]
    └─ reads_from ImportRuns
  └─ uses_service IControlledRepository<ImportRun>
    └─ method ReadQuery [L53]
  └─ uses_service IMapper
    └─ method Map [L58]
  └─ sends_request CanIAccessDatasetQuery [L55]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Cirrus.ApplicationService.Accounting.Queries.CanIAccessDatasetQueryHandler.Handle [L58–L140]
      └─ uses_service IControlledRepository<Dataset>
        └─ method ReadQuery [L127]
      └─ uses_service IRequestInfoService (AddScoped)
        └─ method IsValidServiceAccountRequest [L101]
      └─ uses_service IRequestProcessor (InstancePerDependency)
        └─ method ProcessAsync [L129]
      └─ uses_service ITenantService (AddScoped)
        └─ method GetCurrentTenant [L88]
      └─ uses_service IUserService (InstancePerLifetimeScope)
        └─ method GetUserId [L103]
      └─ uses_cache IDistributedCache [L116]
        └─ method SetRecordAsync [write] [L116]
      └─ uses_cache IDistributedCache [L106]
        └─ method DoesRecordExistAsync [access] [L106]
      └─ uses_cache IDistributedCache [L103]
        └─ method CreateAccessKey [write] [L103]
      └─ uses_cache IDistributedCache [L90]
        └─ method DoesRecordExistAsync [access] [L90]
      └─ uses_cache IDistributedCache [L88]
        └─ method CreateDatasetLockKey [write] [L88]

