[web] GET /core/v1/users/audits/{entityId:Guid}  (Dataverse.Api.External.Controllers.v1.Core.UsersController.GetAuditQuery)  [L129–L134]
  └─ maps_to EntityAuditDto [L132]
  └─ calls UserRepository.ReadQuery [L132]
  └─ queries User [L132]
  └─ uses_service IControlledRepository<User>
    └─ method ReadQuery [L132]

