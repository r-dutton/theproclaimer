[web] PUT /api/accounting/ledger/auto-journals/distributions/{datasetId:Guid}  (Cirrus.Api.Controllers.Accounting.Ledger.AutoJournals.DistributionController.Save)  [L77–L103] status=200 [auth=user]
  └─ calls DistributionRepository (methods: Add,WriteQuery) [L96]
  └─ calls DatasetRepository.WriteQuery [L80]
  └─ insert Distribution [L96]
    └─ reads_from Distributions
  └─ write Distribution [L89]
    └─ reads_from Distributions
  └─ write Dataset [L80]
  └─ sends_request MapSourceAccountsCommand -> MapSourceAccountsCommandHandler [L87]
    └─ handled_by Cirrus.ApplicationService.Accounting.Commands.MapSourceAccountsCommandHandler.Handle [L52–L259]
      └─ calls SourceAccountsCachedRepository (methods: AddTrackedRange,InDatabaseOnly,InMemoryOnly) [L92]
      └─ uses_service IControlledRepository<Source> (Scoped (inferred))
        └─ method WriteQuery [L151]
          └─ implementation Cirrus.Data.Repository.Accounting.SourceData.SourceRepository.WriteQuery
      └─ uses_service IRequestProcessor (InstancePerDependency)
        └─ method ProcessAsync [L123]
          └─ implementation DataGet.Services.Features.Requests.RequestProcessor.ProcessAsync [L7-L35]
            └─ ... (no dispatches detected)
  └─ sends_request CanIAccessDatasetQuery -> CanIAccessDatasetQueryHandler [L81]
    └─ handled_by Cirrus.ApplicationService.Accounting.Queries.CanIAccessDatasetQueryHandler.Handle [L58–L140]
      └─ uses_service IRequestProcessor (InstancePerDependency)
        └─ method ProcessAsync [L129]
          └─ implementation DataGet.Services.Features.Requests.RequestProcessor.ProcessAsync (see previous expansion)
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
    └─ entities 2 (writes=3, reads=0)
      └─ Distribution writes=2 reads=0
      └─ Dataset writes=1 reads=0
    └─ requests 2
      └─ CanIAccessDatasetQuery
      └─ MapSourceAccountsCommand
    └─ handlers 2
      └─ CanIAccessDatasetQueryHandler
      └─ MapSourceAccountsCommandHandler
    └─ caches 1
      └─ IMemoryCache

