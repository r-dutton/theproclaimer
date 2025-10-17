[web] PUT /api/users/{id}  (Workpapers.Next.API.Controllers.UsersController.Put)  [L192–L205] status=200 [auth=AuthorizationPolicies.User]
  └─ calls UserRepository.WriteQuery [L196]
  └─ write User [L196]
  └─ uses_service UserRepository
    └─ method WriteQuery [L196]
      └─ implementation Workpapers.Next.Data.Repository.Firms.UserRepository.WriteQuery [L9-L41]
  └─ impact_summary
    └─ entities 1 (writes=1, reads=0)
      └─ User writes=1 reads=0
    └─ services 1
      └─ UserRepository

