[web] GET /api/super/tenants/{id}/subscriptions  (Dataverse.Api.Controllers.Super.TenantController.GetSubscriptions)  [L112–L120] [auth=Authentication.MasterPolicy]
  └─ maps_to SubscriptionDto [L115]
  └─ calls TenantRepository.ReadTable [L115]
  └─ queries Tenant [L115]
    └─ reads_from Tenants
  └─ uses_service TenantRepository
    └─ method ReadTable [L115]

