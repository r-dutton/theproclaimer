[web] POST /api/users/{id}/resendinvite  (Workpapers.Next.API.Controllers.UsersController.ResendInvite)  [L166–L176] status=201 [auth=AuthorizationPolicies.Administrator]
  └─ calls UserRepository.WriteQuery [L170]
  └─ write User [L170]
  └─ uses_service IControlledRepository<User>
    └─ method WriteQuery [L170]
      └─ ... (no implementation details available)
  └─ sends_request ResendIdentityInviteCommand [L173]
    └─ handled_by Workpapers.Next.ApplicationService.Commands.Identity.ResendIdentityInviteCommandHandler.Handle [L25–L50]
      └─ uses_service DataverseService
        └─ method Post [L39]
          └─ implementation Workpapers.Next.Dataverse.Service.DataverseService.Post [L17-L127]

