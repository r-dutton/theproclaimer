[web] PUT /api/ui/support-users/revoke/{userId:guid}  (Dataverse.Api.Controllers.UI.Core.SupportUsersController.RevokeSupportUserAccess)  [L73–L78] [auth=Authentication.AdminPolicy]
  └─ sends_request RevokeSupportUserCommand [L76]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Dataverse.ApplicationService.Commands.Users.RevokeSupportUserCommandHanlder.Handle [L34–L85]
      └─ calls TenantRepository.WriteTable [L55]
      └─ calls TenantRepository.WriteTable [L53]
      └─ uses_service IControlledRepository<User>
        └─ method ReadQuery [L69]
      └─ uses_service RequestProcessor
        └─ method ProcessAsync [L81]

