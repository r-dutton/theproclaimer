[web] GET /api/super/users/all  (Dataverse.Api.Controllers.Super.Core.UsersController.Search)  [L78–L89] status=200 [auth=Authentication.MasterPolicy]
  └─ sends_request FindCentralUsersQuery [L88]
    └─ handled_by Dataverse.ApplicationService.Queries.Firms.Users.FindCentralUsersQueryHandler.Handle [L48–L116]
      └─ calls TenantRepository.ReadTable [L69]
      └─ calls TenantRepository.ReadTable [L63]
      └─ uses_service UserService
        └─ method GetIdentityId [L73]
          └─ implementation Dataverse.ApplicationService.Services.UserService.GetIdentityId [L15-L185]

