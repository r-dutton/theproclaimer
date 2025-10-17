[web] GET /api/accounting/reports/notes/policies/variants  (Cirrus.Api.Controllers.Accounting.Reports.Notes.PolicyVariantsController.CanIEditPolicyVariant)  [L159–L170] status=200 [auth=Authentication.UserPolicy]
  └─ uses_service IUserService (InstancePerLifetimeScope)
    └─ method IsInRole [L163]
      └─ implementation Workpapers.Next.ApplicationService.Services.UserService.IsInRole [L20-L295]
        └─ uses_service RequestProcessor
          └─ method GetUserByDataverseId [L287]
            └─ resolves_request Workpapers.Next.Services.Features.Requests.RequestProcessor.GetUserByDataverseId
            └─ resolves_request Cirrus.Services.Features.Requests.RequestProcessor.GetUserByDataverseId
            └─ resolves_request DataGet.Services.Features.Requests.RequestProcessor.GetUserByDataverseId
            └─ +1 additional_requests elided
        └─ uses_service bool?
          └─ method IsSuperUser [L174]
            └─ ... (no implementation details available)
        └─ uses_service Firm>
          └─ method GetFirmId [L139]
            └─ ... (no implementation details available)
        └─ uses_service User>
          └─ method GetUserIdFromToken [L106]
            └─ ... (no implementation details available)
        └─ uses_service User
          └─ method GetUserId [L67]
            └─ implementation Workpapers.Next.DomainModel.Model.Firms.User.GetUserId [L18-L368]
        └─ uses_service Guid?
          └─ method GetUserId [L64]
            └─ ... (no implementation details available)
        └─ uses_cache IMemoryCache.GetOrCreate [read] [L280]
  └─ sends_request CanIAccessFileQuery -> CanIAccessFileQueryHandler [L168]
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
                └─ implementation Workpapers.Next.DomainModel.Model.Firms.User.GetUserId (see previous expansion)
            └─ uses_service Guid?
              └─ method GetUserId [L64]
                └─ ... (no implementation details available)
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
    └─ services 1
      └─ IUserService
    └─ requests 1
      └─ CanIAccessFileQuery
    └─ handlers 1
      └─ CanIAccessFileQueryHandler
    └─ caches 1
      └─ IMemoryCache

