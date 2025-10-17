[web] POST /api/internal/tenants/  (Dataverse.Api.Controllers.Internal.TenantController.Create)  [L62–L68] status=201 [auth=Authentication.MachineToMachinePolicy]
  └─ sends_request CreateTenantCommand -> CreateTenantCommandHandler [L65]
    └─ handled_by Dataverse.Tenants.Commands.Tenants.CreateTenantCommandHandler.Handle [L32–L124]
      └─ calls TenantRepository (methods: Add,WriteTable) [L60]
  └─ impact_summary
    └─ requests 1
      └─ CreateTenantCommand
    └─ handlers 1
      └─ CreateTenantCommandHandler

