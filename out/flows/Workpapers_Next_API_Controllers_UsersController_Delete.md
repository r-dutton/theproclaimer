[web] DELETE /api/users/{id}  (Workpapers.Next.API.Controllers.UsersController.Delete)  [L207–L219] [auth=AuthorizationPolicies.Administrator]
  └─ calls UserRepository.WriteQuery [L211]
  └─ writes_to User [L211]
  └─ uses_service IControlledRepository<User>
    └─ method WriteQuery [L211]

