[web] GET /api/accounting/assets/reports/monthly  (Cirrus.Api.Controllers.Accounting.Assets.AssetReportController.GetMonthlyDepreciationReport)  [L123–L137] status=200 [auth=user]
  └─ sends_request GetMonthlyDepreciationQuery -> GetMonthlyDepreciationQueryHandler [L128]
    └─ handled_by Cirrus.ApplicationService.Accounting.Queries.Assets.GetMonthlyDepreciationQueryHandler.Handle [L19–L183]
      └─ uses_service IControlledRepository<DepreciationYear> (Scoped (inferred))
        └─ method ReadQuery [L38]
          └─ implementation Cirrus.Data.Repository.Accounting.Assets.DepreciationYearRepository.ReadQuery
      └─ uses_service GetDepreciationDetailForMonthlyQuery
        └─ method Execute [L37]
          └─ ... (no implementation details available)
  └─ sends_request CanIAccessFileQuery -> CanIAccessFileQueryHandler [L126]
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
    └─ requests 2
      └─ CanIAccessFileQuery
      └─ GetMonthlyDepreciationQuery
    └─ handlers 2
      └─ CanIAccessFileQueryHandler
      └─ GetMonthlyDepreciationQueryHandler
    └─ caches 1
      └─ IMemoryCache

