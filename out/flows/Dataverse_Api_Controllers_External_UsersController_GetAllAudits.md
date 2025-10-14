[web] GET /audit  (Dataverse.Api.Controllers.External.UsersController.GetAllAudits)  [L40–L45]
  └─ calls UserRepository.ReadQuery [L44]
  └─ queries User [L44]
  └─ uses_service IControlledRepository<User>
    └─ method ReadQuery [L44]

