[web] POST /api/super/users/{userId:Guid}/resend-invite  (Dataverse.Api.Controllers.Super.Core.UsersController.ResendInvite)  [L172–L182] [auth=Authentication.MasterPolicy,Authentication.RequireTenantIdPolicy]
  └─ calls UserRepository.ReadQuery [L176]
  └─ queries User [L176]
  └─ uses_service IControlledRepository<User>
    └─ method ReadQuery [L176]
  └─ sends_request ResendInviteCommand [L180]
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

