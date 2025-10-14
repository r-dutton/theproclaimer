[web] POST /api/internal/tenants/  (Dataverse.Api.Controllers.Internal.TenantController.Create)  [L62–L68] [auth=Authentication.MachineToMachinePolicy]
  └─ sends_request CreateTenantCommand [L65]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Dataverse.Tenants.Commands.Tenants.CreateTenantCommandHandler.Handle [L32–L124]
      └─ calls TenantRepository.Add [L60]
      └─ calls TenantRepository.WriteTable [L48]

