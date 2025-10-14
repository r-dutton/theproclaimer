[web] GET /api/ui/tenants/subscriptions  (Dataverse.Api.Controllers.UI.TenantController.GetSubscriptions)  [L58–L68] [auth=Authentication.UserPolicy]
  └─ maps_to SubscriptionLightDto [L62]
  └─ calls TenantRepository.ReadTable [L62]
  └─ queries Tenant [L62]
    └─ reads_from Tenants
  └─ uses_service TenantRepository
    └─ method ReadTable [L62]
  └─ uses_service TenantService
    └─ method GetCurrentTenantAsync [L61]

