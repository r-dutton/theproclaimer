[web] GET /api/ui/tenants/license-summary/trials/requested  (Dataverse.Api.Controllers.UI.TenantController.GetRequestedLicenseSummaries)  [L97–L118] status=200 [auth=Authentication.UserPolicy]
  └─ uses_cache IDistributedCache.GetRecordAsync [read] [L113]
  └─ calls TenantRepository.ReadTable [L101]
  └─ query Tenant [L101]
    └─ reads_from Tenants
  └─ uses_service TenantService
    └─ method GetCurrentTenantAsync [L100]
      └─ implementation Dataverse.Services.Features.Tenants.TenantService.GetCurrentTenantAsync [L6-L27]
  └─ uses_service TenantRepository
    └─ method ReadTable [L101]
      └─ implementation Dataverse.Tenants.Data.TenantRepository.ReadTable [L15-L180]

