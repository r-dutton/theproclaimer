[web] GET /api/ui/tenants/subscriptions  (Dataverse.Api.Controllers.UI.TenantController.GetSubscriptions)  [L58–L68] status=200 [auth=Authentication.UserPolicy]
  └─ maps_to SubscriptionLightDto [L62]
  └─ calls TenantRepository.ReadTable [L62]
  └─ query Tenant [L62]
    └─ reads_from Tenants
  └─ uses_service TenantService
    └─ method GetCurrentTenantAsync [L61]
      └─ implementation Dataverse.Services.Features.Tenants.TenantService.GetCurrentTenantAsync [L6-L27]
  └─ uses_service TenantRepository
    └─ method ReadTable [L62]
      └─ implementation Dataverse.Tenants.Data.TenantRepository.ReadTable [L15-L180]

