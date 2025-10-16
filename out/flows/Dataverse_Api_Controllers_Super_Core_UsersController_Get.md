[web] GET /api/super/users/all/{id:guid}  (Dataverse.Api.Controllers.Super.Core.UsersController.Get)  [L98–L102] status=200 [auth=Authentication.MasterPolicy]
  └─ sends_request GetCentralUserQuery [L101]
    └─ handled_by Dataverse.ApplicationService.Queries.Firms.Users.GetCentralUserQueryHandler.Handle [L32–L144]
      └─ calls TenantRepository.WriteTable [L77]
      └─ calls TenantRepository.ReadTable [L50]
      └─ maps_to CentralUserWithTenantsDto [L50]

