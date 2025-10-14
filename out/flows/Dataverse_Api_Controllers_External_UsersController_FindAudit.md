[web] GET /audit/find  (Dataverse.Api.Controllers.External.UsersController.FindAudit)  [L47–L51]
  └─ calls UserRepository.ReadQuery [L50]
  └─ queries User [L50]
  └─ uses_service IControlledRepository<User>
    └─ method ReadQuery [L50]

