[web] GET /api/super/users/all/{id:guid}  (Dataverse.Api.Controllers.Super.Core.UsersController.Get)  [L98–L102] status=200 [auth=Authentication.MasterPolicy]
  └─ sends_request GetCentralUserQuery -> GetCentralUserQueryHandler [L101]
    └─ handled_by Dataverse.ApplicationService.Queries.Firms.Users.GetCentralUserQueryHandler.Handle [L32–L144]
      └─ calls TenantRepository (methods: WriteTable,ReadTable) [L77]
      └─ maps_to CentralUserWithTenantsDto [L50]
  └─ impact_summary
    └─ requests 1
      └─ GetCentralUserQuery
    └─ handlers 1
      └─ GetCentralUserQueryHandler
    └─ mappings 1
      └─ CentralUserWithTenantsDto

