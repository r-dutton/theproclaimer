[web] GET /api/super/users/all  (Dataverse.Api.Controllers.Super.Core.UsersController.Search)  [L78–L89] [auth=Authentication.MasterPolicy]
  └─ sends_request FindCentralUsersQuery [L88]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Dataverse.ApplicationService.Queries.Firms.Users.FindCentralUsersQueryHandler.Handle [L48–L116]
      └─ calls TenantRepository.ReadTable [L69]
      └─ calls TenantRepository.ReadTable [L63]
      └─ uses_service UserService
        └─ method GetIdentityId [L73]

