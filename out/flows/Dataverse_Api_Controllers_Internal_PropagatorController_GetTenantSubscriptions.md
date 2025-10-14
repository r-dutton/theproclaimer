[web] GET /api/internal/Propagator/subscriptions  (Dataverse.Api.Controllers.Internal.PropagatorController.GetTenantSubscriptions)  [L122–L128] [auth=Authentication.MachineToMachinePolicy,Authentication.RequireTenantIdPolicy]
  └─ maps_to SubscriptionLightDto [L125]
  └─ calls TenantRepository.ReadTable [L125]
  └─ queries Tenant [L125]
    └─ reads_from Tenants
  └─ uses_service TenantRepository
    └─ method ReadTable [L125]

