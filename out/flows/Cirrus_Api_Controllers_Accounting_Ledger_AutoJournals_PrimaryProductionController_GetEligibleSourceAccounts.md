[web] GET /api/accounting/ledger/auto-journals/primary-production/{datasetId}/{tradingAccountId}/source-accounts  (Cirrus.Api.Controllers.Accounting.Ledger.AutoJournals.PrimaryProductionController.GetEligibleSourceAccounts)  [L69–L89] status=200 [auth=user]
  └─ maps_to SourceAccountLightDto [L80]
    └─ automapper.registration CirrusMappingProfile (SourceAccount->SourceAccountLightDto) [L262]
    └─ converts_to LinkedSourceAccount [L72]
    └─ converts_to SourceAccountInJournalModel [L269]
    └─ converts_to MappingSourceAccountModel [L830]
  └─ calls SourceAccountRepository.ReadQuery [L80]
  └─ calls DatasetRepository.ReadQuery [L73]
  └─ query SourceAccount [L80]
    └─ reads_from SourceAccounts
  └─ query Dataset [L73]
  └─ sends_request CanIAccessDatasetQuery -> CanIAccessDatasetQueryHandler [L72]
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
    └─ entities 2 (writes=0, reads=2)
      └─ Dataset writes=0 reads=1
      └─ SourceAccount writes=0 reads=1
    └─ requests 1
      └─ CanIAccessDatasetQuery
    └─ handlers 1
      └─ CanIAccessDatasetQueryHandler
    └─ caches 1
      └─ IMemoryCache
    └─ mappings 1
      └─ SourceAccountLightDto

