[web] POST /api/users/{id}/resendinvite  (Workpapers.Next.API.Controllers.UsersController.ResendInvite)  [L166–L176] [auth=AuthorizationPolicies.Administrator]
  └─ calls UserRepository.WriteQuery [L170]
  └─ writes_to User [L170]
  └─ uses_service IControlledRepository<User>
    └─ method WriteQuery [L170]
  └─ sends_request ResendIdentityInviteCommand [L173]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Workpapers.Next.ApplicationService.Commands.Identity.ResendIdentityInviteCommandHandler.Handle [L25–L50]
      └─ uses_service DataverseService
        └─ method Post [L39]

