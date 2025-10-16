[web] GET /api/ui/tenants/license-summary  (Dataverse.Api.Controllers.UI.TenantController.GetLicenseSummary)  [L70–L95] status=200 [auth=Authentication.UserPolicy]
  └─ maps_to LicenseSummaryDto [L74]
  └─ calls UserRepository.ReadQuery [L81]
  └─ calls TenantRepository.ReadTable [L74]
  └─ query User [L81]
  └─ query Tenant [L74]
    └─ reads_from Tenants
  └─ uses_service TenantService
    └─ method GetCurrentTenantAsync [L73]
      └─ implementation Dataverse.Services.Features.Tenants.TenantService.GetCurrentTenantAsync [L6-L27]
  └─ uses_service IControlledRepository<User>
    └─ method ReadQuery [L81]
      └─ ... (no implementation details available)
  └─ uses_service TenantRepository
    └─ method ReadTable [L74]
      └─ implementation Dataverse.Tenants.Data.TenantRepository.ReadTable [L15-L180]

