[web] PUT /api/ui/support-users  (Dataverse.Api.Controllers.UI.Core.SupportUsersController.UpdateSupportUserAccess)  [L66–L71] status=200 [auth=Authentication.AdminPolicy]
  └─ sends_request UpdateSupportUserAccessCommand [L69]
    └─ handled_by Dataverse.ApplicationService.Commands.Users.UpdateSupportUserAccessCommandHandler.Handle [L22–L45]
      └─ calls TenantRepository.WriteTable [L33]

