[web] PUT /api/super/support-users  (Dataverse.Api.Controllers.Super.Core.SupportUsersController.UpdateSupportUserAccess)  [L50–L55] status=200 [auth=Authentication.MasterPolicy,Authentication.RequireTenantIdPolicy]
  └─ sends_request UpdateSupportUserAccessCommand [L53]
    └─ handled_by Dataverse.ApplicationService.Commands.Users.UpdateSupportUserAccessCommandHandler.Handle [L22–L45]
      └─ calls TenantRepository.WriteTable [L33]

