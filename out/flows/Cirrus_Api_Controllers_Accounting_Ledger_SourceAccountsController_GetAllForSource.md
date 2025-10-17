[web] GET /api/accounting/ledger/source-accounts/for-source/{sourceId}  (Cirrus.Api.Controllers.Accounting.Ledger.SourceAccountsController.GetAllForSource)  [L111–L133] status=200 [auth=user]
  └─ calls SourceRepository.ReadQuery [L119]
  └─ query Source [L119]
  └─ sends_request GetSourceAccountsWithCachedQuery -> GetSourceAccountsWithCachedQueryHandler [L127]
    └─ handled_by Cirrus.ApplicationService.Accounting.Queries.GetSourceAccountsWithCachedQueryHandler.Handle [L34–L96]
      └─ maps_to SourceAccountWithCachedDto [L53]
        └─ automapper.registration CirrusMappingProfile (SourceAccount->SourceAccountWithCachedDto) [L289]
      └─ uses_service IControlledRepository<CachedSourceAccount> (Scoped (inferred))
        └─ method ReadQuery [L68]
          └─ implementation Cirrus.Data.Repository.Accounting.Ledger.CachedSourceAccountRepository.ReadQuery
      └─ uses_service IControlledRepository<SourceAccount> (Scoped (inferred))
        └─ method ReadQuery [L53]
          └─ implementation Cirrus.Data.Repository.Accounting.Ledger.SourceAccountRepository.ReadQuery
  └─ sends_request CanIAccessFileQuery -> CanIAccessFileQueryHandler [L123]
    └─ handled_by Cirrus.ApplicationService.Firm.Queries.CanIAccessFileQueryHandler.Handle [L43–L101]
      └─ uses_service IRequestProcessor (InstancePerDependency)
        └─ method ProcessAsync [L90]
          └─ implementation DataGet.Services.Features.Requests.RequestProcessor.ProcessAsync [L7-L35]
            └─ ... (no dispatches detected)
      └─ uses_service IUserService (InstancePerLifetimeScope)
        └─ method GetUserId [L68]
          └─ implementation Workpapers.Next.ApplicationService.Services.UserService.GetUserId [L20-L295]
            └─ uses_service User
              └─ method GetUserId [L67]
                └─ implementation Workpapers.Next.DomainModel.Model.Firms.User.GetUserId [L18-L368]
            └─ uses_service Guid?
              └─ method GetUserId [L64]
                └─ ... (no implementation details available)
            └─ uses_cache IMemoryCache.GetOrCreate [read] [L280]
      └─ uses_service ITenantService (AddScoped)
        └─ method GetCurrentTenant [L68]
          └─ implementation Dataverse.Services.Features.Tenants.TenantService.GetCurrentTenant [L6-L27]
            └─ uses_service TenantIdentificationService
              └─ method GetCurrentTenant [L20]
                └─ implementation Dataverse.Tenants.Tenants.TenantIdentificationService.GetCurrentTenant [L27-L149]
                  └─ uses_cache IMemoryCache.GetOrCreateAsync [read] [L117]
                  └─ uses_cache IMemoryCache.GetOrCreate [read] [L96]
                  └─ logs ILogger<ITenantIdentificationService> [Warning] [L53]
      └─ uses_service IRequestInfoService (AddScoped)
        └─ method IsValidServiceAccountRequest [L66]
          └─ implementation Dataverse.Services.Features.RequestInfoService.IsValidServiceAccountRequest [L11-L92]
      └─ uses_cache IDistributedCache.SetRecordAsync [write] [L79]
      └─ uses_cache IDistributedCache.DoesRecordExistAsync [access] [L71]
      └─ uses_cache IDistributedCache.CreateAccessKey [write] [L68]
  └─ impact_summary
    └─ entities 1 (writes=0, reads=1)
      └─ Source writes=0 reads=1
    └─ requests 2
      └─ CanIAccessFileQuery
      └─ GetSourceAccountsWithCachedQuery
    └─ handlers 2
      └─ CanIAccessFileQueryHandler
      └─ GetSourceAccountsWithCachedQueryHandler
    └─ caches 1
      └─ IMemoryCache
    └─ mappings 1
      └─ SourceAccountWithCachedDto

