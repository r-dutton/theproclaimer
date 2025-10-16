[web] POST /api/users/{id}/request-identity-account  (Workpapers.Next.API.Controllers.UsersController.Invite)  [L178–L190] status=201 [auth=AuthorizationPolicies.Administrator]
  └─ calls UserRepository.WriteQuery [L182]
  └─ write User [L182]
  └─ uses_service IControlledRepository<User>
    └─ method WriteQuery [L182]
      └─ ... (no implementation details available)

