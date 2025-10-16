[web] GET /api/accounting/ledger/import-runs/{id:int}  (Cirrus.Api.Controllers.Accounting.Ledger.ImportRunController.Get)  [L50–L62] status=200 [auth=user]
  └─ maps_to JournalLightDto [L58]
  └─ maps_to ImportRunDto [L53]
    └─ automapper.registration CirrusMappingProfile (ImportRun->ImportRunDto) [L517]
    └─ automapper.registration WorkpapersMappingProfile (ImportRun->ImportRunDto) [L817]
  └─ calls ImportRunRepository.ReadQuery [L53]
  └─ query ImportRun [L53]
    └─ reads_from ImportRuns
  └─ uses_service IControlledRepository<ImportRun>
    └─ method ReadQuery [L53]
      └─ ... (no implementation details available)
  └─ uses_service IMapper
    └─ method Map [L58]
      └─ ... (no implementation details available)
  └─ sends_request CanIAccessDatasetQuery [L55]
    └─ handled_by Cirrus.ApplicationService.Accounting.Queries.CanIAccessDatasetQueryHandler.Handle [L58–L140]
      └─ uses_service IControlledRepository<Dataset>
        └─ method ReadQuery [L127]
          └─ ... (no implementation details available)
      └─ uses_service IRequestInfoService (AddScoped)
        └─ method IsValidServiceAccountRequest [L101]
          └─ implementation IRequestInfoService.IsValidServiceAccountRequest [L20-L20]
          └─ ... (no implementation details available)
      └─ uses_service IRequestProcessor (InstancePerDependency)
        └─ method ProcessAsync [L129]
          └─ implementation IRequestProcessor.ProcessAsync [L9-L9]
          └─ ... (no implementation details available)
      └─ uses_service ITenantService (AddScoped)
        └─ method GetCurrentTenant [L88]
          └─ implementation ITenantService.GetCurrentTenant [L14-L14]
          └─ ... (no implementation details available)
      └─ uses_service IUserService (InstancePerLifetimeScope)
        └─ method GetUserId [L103]
          └─ implementation IUserService.GetUserId [L18-L18]
          └─ ... (no implementation details available)
      └─ uses_cache IDistributedCache.SetRecordAsync [write] [L116]
      └─ uses_cache IDistributedCache.DoesRecordExistAsync [access] [L106]
      └─ uses_cache IDistributedCache.CreateAccessKey [write] [L103]
      └─ uses_cache IDistributedCache.DoesRecordExistAsync [access] [L90]
      └─ uses_cache IDistributedCache.CreateDatasetLockKey [write] [L88]

