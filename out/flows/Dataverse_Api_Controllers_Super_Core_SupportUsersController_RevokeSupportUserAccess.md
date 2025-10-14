[web] PUT /api/super/support-users/revoke  (Dataverse.Api.Controllers.Super.Core.SupportUsersController.RevokeSupportUserAccess)  [L43–L48] [auth=Authentication.MasterPolicy,Authentication.RequireTenantIdPolicy]
  └─ sends_request RevokeSupportUserCommand [L46]
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

