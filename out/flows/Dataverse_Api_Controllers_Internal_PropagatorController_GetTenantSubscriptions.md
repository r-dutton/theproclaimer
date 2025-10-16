[web] GET /api/internal/Propagator/subscriptions  (Dataverse.Api.Controllers.Internal.PropagatorController.GetTenantSubscriptions)  [L122–L128] status=200 [auth=Authentication.MachineToMachinePolicy,Authentication.RequireTenantIdPolicy]
  └─ maps_to SubscriptionLightDto [L125]
  └─ calls TenantRepository.ReadTable [L125]
  └─ query Tenant [L125]
    └─ reads_from Tenants
  └─ uses_service TenantRepository
    └─ method ReadTable [L125]
      └─ implementation Dataverse.Tenants.Data.TenantRepository.ReadTable [L15-L180]
  └─ impact_summary
    └─ entities 1 (writes=0, reads=1)
      └─ Tenant writes=0 reads=1
    └─ services 1
      └─ TenantRepository
    └─ mappings 1
      └─ SubscriptionLightDto

