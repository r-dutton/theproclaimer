[web] GET /core/v1/users/audits/{entityId:Guid}  (Dataverse.Api.External.Controllers.v1.Core.UsersController.GetAuditQuery)  [L129–L134] status=200
  └─ maps_to EntityAuditDto [L132]
  └─ calls UserRepository.ReadQuery [L132]
  └─ query User [L132]
  └─ uses_service UserRepository
    └─ method ReadQuery [L132]
      └─ implementation Dataverse.Data.Repository.Users.UserRepository.ReadQuery [L8-L51]
  └─ impact_summary
    └─ entities 1 (writes=0, reads=1)
      └─ User writes=0 reads=1
    └─ services 1
      └─ UserRepository
    └─ mappings 1
      └─ EntityAuditDto

