[web] PUT /api/accounting/assets/pooling/write-off  (Cirrus.Api.Controllers.Accounting.Assets.AssetPoolingController.SetPoolBalance)  [L45–L51] status=200 [auth=user]
  └─ sends_request WriteOffPoolBalanceCommand -> WriteOffPoolBalanceCommandHandler [L49]
    └─ handled_by Cirrus.ApplicationService.Accounting.Commands.Assets.WriteOffPoolBalanceCommandHandler.Handle [L34–L88]
      └─ uses_service IControlledRepository<DepreciationRecord> (Scoped (inferred))
        └─ method WriteQuery [L75]
          └─ implementation Cirrus.Data.Repository.Accounting.Assets.DepreciationRecordRepository.WriteQuery
      └─ uses_service IRequestProcessor (InstancePerDependency)
        └─ method ProcessAsync [L73]
          └─ implementation DataGet.Services.Features.Requests.RequestProcessor.ProcessAsync [L7-L35]
            └─ ... (no dispatches detected)
      └─ uses_service IControlledRepository<Asset> (Scoped (inferred))
        └─ method OptimisedWriteQuery [L58]
          └─ implementation Cirrus.Data.Repository.Accounting.Assets.AssetRepository.OptimisedWriteQuery
  └─ sends_request CanIAccessFileQuery -> CanIAccessFileQueryHandler [L48]
    └─ handled_by Cirrus.ApplicationService.Firm.Queries.CanIAccessFileQueryHandler.Handle [L43–L101]
      └─ uses_service IRequestProcessor (InstancePerDependency)
        └─ method ProcessAsync [L90]
          └─ implementation DataGet.Services.Features.Requests.RequestProcessor.ProcessAsync (see previous expansion)
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
    └─ requests 2
      └─ CanIAccessFileQuery
      └─ WriteOffPoolBalanceCommand
    └─ handlers 2
      └─ CanIAccessFileQueryHandler
      └─ WriteOffPoolBalanceCommandHandler
    └─ caches 1
      └─ IMemoryCache

