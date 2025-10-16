[web] POST /api/ui/users/{userId:guid}/resend-invite  (Dataverse.Api.Controllers.UI.Core.UsersController.ResendInvite)  [L307–L324] status=201 [auth=Authentication.UserPolicy,Authentication.AdminPolicy]
  └─ calls UserRepository.WriteQuery [L318]
  └─ calls UserRepository.ReadQuery [L311]
  └─ query User [L311]
  └─ write User [L318]
  └─ uses_service IControlledRepository<User>
    └─ method ReadQuery [L311]
      └─ ... (no implementation details available)
  └─ sends_request ResendInviteCommand [L316]
    └─ handled_by Cirrus.Central.Commands.ResendInviteCommandHandler.Handle [L33–L74]
      └─ calls CentralRepository.Commit [L68]
      └─ calls CentralRepository.WriteTable [L50]
      └─ uses_service DataverseService
        └─ method Post [L59]
          └─ implementation Cirrus.Central.Features.MachineToMachine.DataverseService.Post [L14-L66]
      └─ uses_service IRequestProcessor (InstancePerDependency)
        └─ method ProcessAsync [L64]
          └─ implementation IRequestProcessor.ProcessAsync [L9-L9]
          └─ ... (no implementation details available)

