[web] GET /api/accounting/ledger/source-accounts/  (Cirrus.Api.Controllers.Accounting.Ledger.SourceAccountsController.GetAllForDataset)  [L63–L84] status=200 [auth=user]
  └─ calls DatasetRepository.ReadQuery [L71]
  └─ query Dataset [L71]
  └─ uses_service IControlledRepository<Dataset>
    └─ method ReadQuery [L71]
      └─ ... (no implementation details available)
  └─ sends_request CanIAccessDatasetQuery [L70]
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
  └─ sends_request GetSourceAccountsWithCachedQuery [L80]
    └─ handled_by Cirrus.ApplicationService.Accounting.Queries.GetSourceAccountsWithCachedQueryHandler.Handle [L34–L96]
      └─ maps_to SourceAccountWithCachedDto [L53]
        └─ automapper.registration CirrusMappingProfile (SourceAccount->SourceAccountWithCachedDto) [L289]
      └─ maps_to SourceAccountWithCachedDto [L84]
      └─ uses_service IControlledRepository<CachedSourceAccount>
        └─ method ReadQuery [L68]
          └─ ... (no implementation details available)
      └─ uses_service IControlledRepository<SourceAccount>
        └─ method ReadQuery [L53]
          └─ ... (no implementation details available)
      └─ uses_service IMapper
        └─ method ConfigurationProvider [L58]
          └─ ... (no implementation details available)

