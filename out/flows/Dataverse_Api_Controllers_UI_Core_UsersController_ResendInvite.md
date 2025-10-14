[web] POST /api/ui/users/{userId:guid}/resend-invite  (Dataverse.Api.Controllers.UI.Core.UsersController.ResendInvite)  [L307–L324] [auth=Authentication.UserPolicy,Authentication.AdminPolicy]
  └─ calls UserRepository.WriteQuery [L318]
  └─ calls UserRepository.ReadQuery [L311]
  └─ queries User [L311]
  └─ writes_to User [L318]
  └─ uses_service IControlledRepository<User>
    └─ method ReadQuery [L311]
  └─ sends_request ResendInviteCommand [L316]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Cirrus.Central.Commands.ResendInviteCommandHandler.Handle [L33–L74]
      └─ calls CentralRepository.Commit [L68]
      └─ calls CentralRepository.WriteTable [L50]
      └─ uses_service DataverseService
        └─ method Post [L59]
      └─ uses_service IRequestProcessor (InstancePerDependency)
        └─ method ProcessAsync [L64]

