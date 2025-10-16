[web] GET /audit/find  (Dataverse.Api.Controllers.External.UsersController.FindAudit)  [L47–L51] status=200
  └─ calls UserRepository.ReadQuery [L50]
  └─ query User [L50]
  └─ uses_service UserRepository
    └─ method ReadQuery [L50]
      └─ implementation Dataverse.Data.Repository.Users.UserRepository.ReadQuery [L8-L51]
  └─ impact_summary
    └─ entities 1 (writes=0, reads=1)
      └─ User writes=0 reads=1
    └─ services 1
      └─ UserRepository

