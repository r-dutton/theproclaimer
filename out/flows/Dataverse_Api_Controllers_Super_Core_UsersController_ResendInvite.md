[web] POST /api/super/users/{userId:Guid}/resend-invite  (Dataverse.Api.Controllers.Super.Core.UsersController.ResendInvite)  [L172–L182] status=201 [auth=Authentication.MasterPolicy,Authentication.RequireTenantIdPolicy]
  └─ calls UserRepository.ReadQuery [L176]
  └─ query User [L176]
  └─ uses_service IControlledRepository<User>
    └─ method ReadQuery [L176]
      └─ ... (no implementation details available)
  └─ sends_request ResendInviteCommand [L180]
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

