[web] GET /core/v1/users/audits  (Dataverse.Api.External.Controllers.v1.Core.UsersController.GetAuditsQuery)  [L115–L121] status=200
  └─ maps_to EntityAuditDto [L118]
  └─ calls UserRepository.ReadQuery [L118]
  └─ query User [L118]
  └─ uses_service IControlledRepository<User>
    └─ method ReadQuery [L118]
      └─ ... (no implementation details available)

