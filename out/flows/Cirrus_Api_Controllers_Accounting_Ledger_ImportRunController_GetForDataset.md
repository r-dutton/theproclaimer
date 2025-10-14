[web] GET /api/accounting/ledger/import-runs/for-dataset  (Cirrus.Api.Controllers.Accounting.Ledger.ImportRunController.GetForDataset)  [L68–L88] [auth=user]
  └─ maps_to UserUltraLightDto [L80]
    └─ automapper.registration DataverseMappingProfile (User->UserUltraLightDto) [L85]
    └─ automapper.registration WorkpapersMappingProfile (User->UserUltraLightDto) [L96]
    └─ automapper.registration CirrusMappingProfile (User->UserUltraLightDto) [L103]
  └─ maps_to ImportRunLightDto [L73]
    └─ automapper.registration CirrusMappingProfile (ImportRun->ImportRunLightDto) [L513]
    └─ automapper.registration WorkpapersMappingProfile (ImportRun->ImportRunLightDto) [L816]
  └─ calls UserRepository.ReadQuery [L80]
  └─ calls ImportRunRepository.ReadQuery [L73]
  └─ queries ImportRun [L73]
    └─ reads_from ImportRuns
  └─ queries User [L80]
  └─ uses_service IControlledRepository<ImportRun>
    └─ method ReadQuery [L73]
  └─ uses_service IControlledRepository<User>
    └─ method ReadQuery [L80]
  └─ sends_request CanIAccessDatasetQuery [L71]
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

