[web] POST /api/super/tenants/  (Dataverse.Api.Controllers.Super.TenantController.Create)  [L182–L188] [auth=Authentication.MasterPolicy]
  └─ sends_request CreateTenantCommand [L185]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Dataverse.Tenants.Commands.Tenants.CreateTenantCommandHandler.Handle [L32–L124]
      └─ calls TenantRepository.Add [L60]
      └─ calls TenantRepository.WriteTable [L48]

