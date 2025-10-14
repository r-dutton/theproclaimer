[web] GET /api/super/tenants/license-summary  (Dataverse.Api.Controllers.Super.TenantController.GetLicenseSummary)  [L122–L151] [auth=Authentication.MasterPolicy,Authentication.RequireTenantIdPolicy]
  └─ maps_to LicenseSummaryDto [L126]
  └─ calls UserRepository.ReadQuery [L133]
  └─ calls TenantRepository.ReadTable [L126]
  └─ queries User [L133]
  └─ queries Tenant [L126]
    └─ reads_from Tenants
  └─ uses_service IControlledRepository<User>
    └─ method ReadQuery [L133]
  └─ uses_service TenantRepository
    └─ method ReadTable [L126]

