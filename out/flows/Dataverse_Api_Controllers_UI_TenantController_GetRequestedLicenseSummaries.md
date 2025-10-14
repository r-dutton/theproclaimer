[web] GET /api/ui/tenants/license-summary/trials/requested  (Dataverse.Api.Controllers.UI.TenantController.GetRequestedLicenseSummaries)  [L97–L118] [auth=Authentication.UserPolicy]
  └─ uses_cache IDistributedCache [L113]
    └─ method GetRecordAsync [read] [L113]
  └─ calls TenantRepository.ReadTable [L101]
  └─ queries Tenant [L101]
    └─ reads_from Tenants
  └─ uses_service TenantRepository
    └─ method ReadTable [L101]
  └─ uses_service TenantService
    └─ method GetCurrentTenantAsync [L100]

