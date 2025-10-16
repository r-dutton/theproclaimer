[web] POST /api/super/tenants/  (Dataverse.Api.Controllers.Super.TenantController.Create)  [L182–L188] status=201 [auth=Authentication.MasterPolicy]
  └─ sends_request CreateTenantCommand [L185]
    └─ handled_by Dataverse.Tenants.Commands.Tenants.CreateTenantCommandHandler.Handle [L32–L124]
      └─ calls TenantRepository.Add [L60]
      └─ calls TenantRepository.WriteTable [L48]

