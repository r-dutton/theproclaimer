[web] POST /api/super/tenants/  (Dataverse.Api.Controllers.Super.TenantController.Create)  [L182–L188] status=201 [auth=Authentication.MasterPolicy]
  └─ sends_request CreateTenantCommand -> CreateTenantCommandHandler [L185]
    └─ handled_by Dataverse.Tenants.Commands.Tenants.CreateTenantCommandHandler.Handle [L32–L124]
      └─ calls TenantRepository (methods: Add,WriteTable) [L60]
  └─ impact_summary
    └─ requests 1
      └─ CreateTenantCommand
    └─ handlers 1
      └─ CreateTenantCommandHandler

