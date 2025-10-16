[web] GET /core/v1/users/audits/{entityId:Guid}  (Dataverse.Api.External.Controllers.v1.Core.UsersController.GetAuditQuery)  [L129–L134] status=200
  └─ maps_to EntityAuditDto [L132]
  └─ calls UserRepository.ReadQuery [L132]
  └─ query User [L132]
  └─ uses_service IControlledRepository<User>
    └─ method ReadQuery [L132]
      └─ ... (no implementation details available)

