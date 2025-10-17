[web] GET /api/super/tenants/{id}/subscriptions  (Dataverse.Api.Controllers.Super.TenantController.GetSubscriptions)  [L112–L120] status=200 [auth=Authentication.MasterPolicy]
  └─ maps_to SubscriptionDto [L115]
  └─ calls TenantRepository.ReadTable [L115]
  └─ query Tenant [L115]
    └─ reads_from Tenants
  └─ uses_service TenantRepository
    └─ method ReadTable [L115]
      └─ implementation Dataverse.Tenants.Data.TenantRepository.ReadTable [L15-L180]
  └─ impact_summary
    └─ entities 1 (writes=0, reads=1)
      └─ Tenant writes=0 reads=1
    └─ services 1
      └─ TenantRepository
    └─ mappings 1
      └─ SubscriptionDto

