[web] PUT /api/ui/support-users  (Dataverse.Api.Controllers.UI.Core.SupportUsersController.UpdateSupportUserAccess)  [L66–L71] [auth=Authentication.AdminPolicy]
  └─ sends_request UpdateSupportUserAccessCommand [L69]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Dataverse.ApplicationService.Commands.Users.UpdateSupportUserAccessCommandHandler.Handle [L22–L45]
      └─ calls TenantRepository.WriteTable [L33]

