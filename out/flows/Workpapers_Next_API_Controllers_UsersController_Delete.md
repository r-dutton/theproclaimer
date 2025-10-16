[web] DELETE /api/users/{id}  (Workpapers.Next.API.Controllers.UsersController.Delete)  [L207–L219] status=200 [auth=AuthorizationPolicies.Administrator]
  └─ calls UserRepository.WriteQuery [L211]
  └─ write User [L211]
  └─ uses_service UserRepository
    └─ method WriteQuery [L211]
      └─ implementation Workpapers.Next.Data.Repository.Firms.UserRepository.WriteQuery [L9-L41]
  └─ impact_summary
    └─ entities 1 (writes=1, reads=0)
      └─ User writes=1 reads=0
    └─ services 1
      └─ UserRepository

