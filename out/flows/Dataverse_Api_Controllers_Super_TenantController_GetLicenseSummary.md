[web] GET /api/super/tenants/license-summary  (Dataverse.Api.Controllers.Super.TenantController.GetLicenseSummary)  [L122–L151] status=200 [auth=Authentication.MasterPolicy,Authentication.RequireTenantIdPolicy]
  └─ maps_to LicenseSummaryDto [L126]
  └─ calls UserRepository.ReadQuery [L133]
  └─ calls TenantRepository.ReadTable [L126]
  └─ query User [L133]
  └─ query Tenant [L126]
    └─ reads_from Tenants
  └─ uses_service UserRepository
    └─ method ReadQuery [L133]
      └─ implementation Dataverse.Data.Repository.Users.UserRepository.ReadQuery [L8-L51]
  └─ uses_service TenantRepository
    └─ method ReadTable [L126]
      └─ implementation Dataverse.Tenants.Data.TenantRepository.ReadTable [L15-L180]
  └─ impact_summary
    └─ entities 2 (writes=0, reads=2)
      └─ Tenant writes=0 reads=1
      └─ User writes=0 reads=1
    └─ services 2
      └─ TenantRepository
      └─ UserRepository
    └─ mappings 1
      └─ LicenseSummaryDto

