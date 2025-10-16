[web] GET /audit/find  (Dataverse.Api.Controllers.External.UsersController.FindAudit)  [L47–L51] status=200
  └─ calls UserRepository.ReadQuery [L50]
  └─ query User [L50]
  └─ uses_service IControlledRepository<User>
    └─ method ReadQuery [L50]
      └─ ... (no implementation details available)

