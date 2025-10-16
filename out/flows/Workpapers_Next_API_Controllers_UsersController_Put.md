[web] PUT /api/users/{id}  (Workpapers.Next.API.Controllers.UsersController.Put)  [L192–L205] status=200 [auth=AuthorizationPolicies.User]
  └─ calls UserRepository.WriteQuery [L196]
  └─ write User [L196]
  └─ uses_service IControlledRepository<User>
    └─ method WriteQuery [L196]
      └─ ... (no implementation details available)

