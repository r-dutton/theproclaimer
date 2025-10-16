[web] GET /api/accounting/ledger/source-accounts/  (Cirrus.Api.Controllers.Accounting.Ledger.SourceAccountsController.GetAllForDataset)  [L63–L84] status=200 [auth=user]
  └─ calls DatasetRepository.ReadQuery [L71]
  └─ query Dataset [L71]
  └─ sends_request GetSourceAccountsWithCachedQuery -> GetSourceAccountsWithCachedQueryHandler [L80]
    └─ handled_by Cirrus.ApplicationService.Accounting.Queries.GetSourceAccountsWithCachedQueryHandler.Handle [L34–L96]
      └─ maps_to SourceAccountWithCachedDto [L53]
        └─ automapper.registration CirrusMappingProfile (SourceAccount->SourceAccountWithCachedDto) [L289]
      └─ uses_service IControlledRepository<CachedSourceAccount> (Scoped (inferred))
        └─ method ReadQuery [L68]
          └─ implementation Cirrus.Data.Repository.Accounting.Ledger.CachedSourceAccountRepository.ReadQuery
      └─ uses_service IControlledRepository<SourceAccount> (Scoped (inferred))
        └─ method ReadQuery [L53]
          └─ implementation Cirrus.Data.Repository.Accounting.Ledger.SourceAccountRepository.ReadQuery
  └─ sends_request CanIAccessDatasetQuery -> CanIAccessDatasetQueryHandler [L70]
    └─ handled_by Cirrus.ApplicationService.Accounting.Queries.CanIAccessDatasetQueryHandler.Handle [L58–L140]
      └─ uses_service IRequestProcessor (InstancePerDependency)
        └─ method ProcessAsync [L129]
          └─ implementation DataGet.Services.Features.Requests.RequestProcessor.ProcessAsync [L7-L35]
            └─ ... (no dispatches detected)
      └─ uses_service IControlledRepository<Dataset> (Scoped (inferred))
        └─ method ReadQuery [L127]
          └─ implementation Cirrus.Data.Repository.Accounting.DatasetRepository.ReadQuery
      └─ uses_service IUserService (InstancePerLifetimeScope)
        └─ method GetUserId [L103]
          └─ implementation Workpapers.Next.ApplicationService.Services.UserService.GetUserId [L20-L295]
            └─ uses_service User
              └─ method GetUserId [L67]
                └─ implementation Workpapers.Next.DomainModel.Model.Firms.User.GetUserId [L18-L368]
            └─ uses_service Guid?
              └─ method GetUserId [L64]
                └─ ... (no implementation details available)
            └─ uses_cache IMemoryCache.GetOrCreate [read] [L280]
      └─ uses_service IRequestInfoService (AddScoped)
        └─ method IsValidServiceAccountRequest [L101]
          └─ implementation Dataverse.Services.Features.RequestInfoService.IsValidServiceAccountRequest [L11-L92]
      └─ uses_service ITenantService (AddScoped)
        └─ method GetCurrentTenant [L88]
          └─ implementation Dataverse.Services.Features.Tenants.TenantService.GetCurrentTenant [L6-L27]
            └─ uses_service TenantIdentificationService
              └─ method GetCurrentTenant [L20]
                └─ implementation Dataverse.Tenants.Tenants.TenantIdentificationService.GetCurrentTenant [L27-L149]
                  └─ uses_cache IMemoryCache.GetOrCreateAsync [read] [L117]
                  └─ uses_cache IMemoryCache.GetOrCreate [read] [L96]
                  └─ logs ILogger<ITenantIdentificationService> [Warning] [L53]
      └─ uses_cache IDistributedCache.SetRecordAsync [write] [L116]
      └─ uses_cache IDistributedCache.DoesRecordExistAsync [access] [L106]
      └─ uses_cache IDistributedCache.CreateAccessKey [write] [L103]
      └─ uses_cache IDistributedCache.CreateDatasetLockKey [write] [L88]
  └─ impact_summary
    └─ entities 1 (writes=0, reads=1)
      └─ Dataset writes=0 reads=1
    └─ requests 2
      └─ CanIAccessDatasetQuery
      └─ GetSourceAccountsWithCachedQuery
    └─ handlers 2
      └─ CanIAccessDatasetQueryHandler
      └─ GetSourceAccountsWithCachedQueryHandler
    └─ caches 1
      └─ IMemoryCache
    └─ mappings 1
      └─ SourceAccountWithCachedDto

