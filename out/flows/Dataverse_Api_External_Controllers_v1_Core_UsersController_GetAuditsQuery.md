[web] GET /core/v1/users/audits  (Dataverse.Api.External.Controllers.v1.Core.UsersController.GetAuditsQuery)  [L115–L121] status=200
  └─ maps_to EntityAuditDto [L118]
  └─ calls UserRepository.ReadQuery [L118]
  └─ query User [L118]
  └─ uses_service UserRepository
    └─ method ReadQuery [L118]
      └─ implementation Dataverse.Data.Repository.Users.UserRepository.ReadQuery [L8-L51]
  └─ impact_summary
    └─ entities 1 (writes=0, reads=1)
      └─ User writes=0 reads=1
    └─ services 1
      └─ UserRepository
    └─ mappings 1
      └─ EntityAuditDto

