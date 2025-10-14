[web] GET /api/internal/Propagator/request-audit  (Dataverse.Api.Controllers.Internal.PropagatorController.RequestAudit)  [L88–L98] [auth=Authentication.MachineToMachinePolicy,Authentication.RequireTenantIdPolicy]
  └─ calls TenantRepository.ReadTable [L92]
  └─ queries Tenant [L92]
    └─ reads_from Tenants
  └─ uses_service MockMessagingService
    └─ method RequestAudit [L97]
  └─ uses_service TenantRepository
    └─ method ReadTable [L92]

