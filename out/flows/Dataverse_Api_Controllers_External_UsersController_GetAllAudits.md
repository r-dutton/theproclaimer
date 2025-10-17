[web] GET /audit  (Dataverse.Api.Controllers.External.UsersController.GetAllAudits)  [L40–L45] status=200
  └─ calls UserRepository.ReadQuery [L44]
  └─ query User [L44]
  └─ uses_service UserRepository
    └─ method ReadQuery [L44]
      └─ implementation Dataverse.Data.Repository.Users.UserRepository.ReadQuery [L8-L51]
  └─ impact_summary
    └─ entities 1 (writes=0, reads=1)
      └─ User writes=0 reads=1
    └─ services 1
      └─ UserRepository

