[web] POST /api/users  (Workpapers.Next.API.Controllers.UsersController.Post)  [L157–L164] [auth=AuthorizationPolicies.Administrator]
  └─ sends_request CreateUserCommand [L161]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Workpapers.Next.Data.Commands.Users.CreateUserCommandHandler.Handle [L12–L33]
      └─ uses_service UnitOfWork
        └─ method Table [L25]

