[web] GET /api/ui/tenants/license-summary  (Dataverse.Api.Controllers.UI.TenantController.GetLicenseSummary)  [L70–L95] [auth=Authentication.UserPolicy]
  └─ maps_to LicenseSummaryDto [L74]
  └─ calls UserRepository.ReadQuery [L81]
  └─ calls TenantRepository.ReadTable [L74]
  └─ queries User [L81]
  └─ queries Tenant [L74]
    └─ reads_from Tenants
  └─ uses_service IControlledRepository<User>
    └─ method ReadQuery [L81]
  └─ uses_service TenantRepository
    └─ method ReadTable [L74]
  └─ uses_service TenantService
    └─ method GetCurrentTenantAsync [L73]

