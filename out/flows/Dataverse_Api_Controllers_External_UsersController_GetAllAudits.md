[web] GET /audit  (Dataverse.Api.Controllers.External.UsersController.GetAllAudits)  [L40–L45] status=200
  └─ calls UserRepository.ReadQuery [L44]
  └─ query User [L44]
  └─ uses_service IControlledRepository<User>
    └─ method ReadQuery [L44]
      └─ ... (no implementation details available)

