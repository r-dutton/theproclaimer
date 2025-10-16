[web] GET /api/internal/Propagator/request-audit  (Dataverse.Api.Controllers.Internal.PropagatorController.RequestAudit)  [L88–L98] status=200 [auth=Authentication.MachineToMachinePolicy,Authentication.RequireTenantIdPolicy]
  └─ calls TenantRepository.ReadTable [L92]
  └─ query Tenant [L92]
    └─ reads_from Tenants
  └─ uses_service MockMessagingService
    └─ method RequestAudit [L97]
      └─ ... (no implementation details available)
  └─ uses_service TenantRepository
    └─ method ReadTable [L92]
      └─ implementation Dataverse.Tenants.Data.TenantRepository.ReadTable [L15-L180]

