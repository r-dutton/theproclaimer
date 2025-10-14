[web] GET /core/v1/users/detailed  (Dataverse.Api.External.Controllers.v1.Core.UsersController.FullQuery)  [L102–L108]
  └─ calls UserRepository.ReadQuery [L105]
  └─ queries User [L105]
  └─ uses_service IControlledRepository<User>
    └─ method ReadQuery [L105]

