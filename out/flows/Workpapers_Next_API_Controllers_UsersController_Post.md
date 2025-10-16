[web] POST /api/users  (Workpapers.Next.API.Controllers.UsersController.Post)  [L157–L164] status=201 [auth=AuthorizationPolicies.Administrator]
  └─ sends_request CreateUserCommand [L161]
    └─ handled_by Workpapers.Next.Data.Commands.Users.CreateUserCommandHandler.Handle [L12–L33]
      └─ uses_service UnitOfWork
        └─ method Table [L25]
          └─ ... (no implementation details available)

