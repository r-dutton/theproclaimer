[web] GET /api/super/tenants/license-summary  (Dataverse.Api.Controllers.Super.TenantController.GetLicenseSummary)  [L122–L151] status=200 [auth=Authentication.MasterPolicy,Authentication.RequireTenantIdPolicy]
  └─ maps_to LicenseSummaryDto [L126]
  └─ calls UserRepository.ReadQuery [L133]
  └─ calls TenantRepository.ReadTable [L126]
  └─ query User [L133]
  └─ query Tenant [L126]
    └─ reads_from Tenants
  └─ uses_service IControlledRepository<User>
    └─ method ReadQuery [L133]
      └─ ... (no implementation details available)
  └─ uses_service TenantRepository
    └─ method ReadTable [L126]
      └─ implementation Dataverse.Tenants.Data.TenantRepository.ReadTable [L15-L180]

