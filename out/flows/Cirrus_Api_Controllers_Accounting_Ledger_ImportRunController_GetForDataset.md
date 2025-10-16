[web] GET /api/accounting/ledger/import-runs/for-dataset  (Cirrus.Api.Controllers.Accounting.Ledger.ImportRunController.GetForDataset)  [L68–L88] status=200 [auth=user]
  └─ maps_to UserUltraLightDto [L80]
    └─ automapper.registration DataverseMappingProfile (User->UserUltraLightDto) [L86]
    └─ automapper.registration WorkpapersMappingProfile (User->UserUltraLightDto) [L96]
    └─ automapper.registration CirrusMappingProfile (User->UserUltraLightDto) [L103]
  └─ maps_to ImportRunLightDto [L73]
    └─ automapper.registration CirrusMappingProfile (ImportRun->ImportRunLightDto) [L513]
    └─ automapper.registration WorkpapersMappingProfile (ImportRun->ImportRunLightDto) [L816]
  └─ calls UserRepository.ReadQuery [L80]
  └─ calls ImportRunRepository.ReadQuery [L73]
  └─ query ImportRun [L73]
    └─ reads_from ImportRuns
  └─ query User [L80]
  └─ uses_service IControlledRepository<ImportRun>
    └─ method ReadQuery [L73]
      └─ ... (no implementation details available)
  └─ uses_service IControlledRepository<User>
    └─ method ReadQuery [L80]
      └─ ... (no implementation details available)
  └─ sends_request CanIAccessDatasetQuery [L71]
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

