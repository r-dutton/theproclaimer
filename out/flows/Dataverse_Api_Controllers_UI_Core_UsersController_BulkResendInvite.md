[web] POST /api/ui/users/bulk/invite  (Dataverse.Api.Controllers.UI.Core.UsersController.BulkResendInvite)  [L292–L305] [auth=Authentication.UserPolicy,Authentication.AdminPolicy]
  └─ sends_request ProcessInviteCommand [L300]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Dataverse.ApplicationService.Commands.Users.ProcessInviteCommandHandler.Handle [L29–L65]
      └─ uses_service IControlledRepository<User>
        └─ method ReadQuery [L46]
      └─ uses_service RequestProcessor
        └─ method ProcessAsync [L53]

