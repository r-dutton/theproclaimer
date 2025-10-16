[web] GET /api/users/fromcentral/{id}  (Workpapers.Next.API.Controllers.UsersController.GetByCentralId)  [L119–L129] status=200 [auth=AuthorizationPolicies.User]
  └─ maps_to UserDto (var user) [L123]
  └─ calls UserRepository.ReadQuery [L123]
  └─ query User [L123]
  └─ uses_service IControlledRepository<User>
    └─ method ReadQuery [L123]
      └─ ... (no implementation details available)
  └─ uses_service IMapper
    └─ method Map [L123]
      └─ ... (no implementation details available)

