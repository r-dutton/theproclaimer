[web] GET /api/users/fromcentral/{id}  (Workpapers.Next.API.Controllers.UsersController.GetByCentralId)  [L119–L129] status=200 [auth=AuthorizationPolicies.User]
  └─ maps_to UserDto (var user) [L123]
  └─ calls UserRepository.ReadQuery [L123]
  └─ query User [L123]
  └─ uses_service UserRepository
    └─ method ReadQuery [L123]
      └─ implementation Workpapers.Next.Data.Repository.Firms.UserRepository.ReadQuery [L9-L41]
  └─ impact_summary
    └─ entities 1 (writes=0, reads=1)
      └─ User writes=0 reads=1
    └─ services 1
      └─ UserRepository
    └─ mappings 1
      └─ UserDto

