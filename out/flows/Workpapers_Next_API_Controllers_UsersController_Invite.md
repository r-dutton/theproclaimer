[web] POST /api/users/{id}/request-identity-account  (Workpapers.Next.API.Controllers.UsersController.Invite)  [L178–L190] status=201 [auth=AuthorizationPolicies.Administrator]
  └─ calls UserRepository.WriteQuery [L182]
  └─ write User [L182]
  └─ uses_service UserRepository
    └─ method WriteQuery [L182]
      └─ implementation Workpapers.Next.Data.Repository.Firms.UserRepository.WriteQuery [L9-L41]
  └─ impact_summary
    └─ entities 1 (writes=1, reads=0)
      └─ User writes=1 reads=0
    └─ services 1
      └─ UserRepository

