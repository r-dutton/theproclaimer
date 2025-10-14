[web] POST /api/users/{id}/request-identity-account  (Workpapers.Next.API.Controllers.UsersController.Invite)  [L178–L190] [auth=AuthorizationPolicies.Administrator]
  └─ calls UserRepository.WriteQuery [L182]
  └─ writes_to User [L182]
  └─ uses_service IControlledRepository<User>
    └─ method WriteQuery [L182]

