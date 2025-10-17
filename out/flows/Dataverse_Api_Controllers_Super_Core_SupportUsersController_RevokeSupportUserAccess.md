[web] PUT /api/super/support-users/revoke  (Dataverse.Api.Controllers.Super.Core.SupportUsersController.RevokeSupportUserAccess)  [L43–L48] status=200 [auth=Authentication.MasterPolicy,Authentication.RequireTenantIdPolicy]
  └─ sends_request RevokeSupportUserCommand -> RevokeSupportUserCommandHanlder [L46]
    └─ handled_by Dataverse.ApplicationService.Commands.Users.RevokeSupportUserCommandHanlder.Handle [L34–L85]
      └─ calls UserRepository.ReadQuery [L69]
      └─ calls TenantRepository.WriteTable [L55]
      └─ uses_service RequestProcessor
        └─ method ProcessAsync [L81]
          └─ resolves_request Dataverse.Services.Features.Requests.RequestProcessor.ProcessAsync
          └─ resolves_request Cirrus.Services.Features.Requests.RequestProcessor.ProcessAsync
          └─ resolves_request DataGet.Services.Features.Requests.RequestProcessor.ProcessAsync
          └─ +1 additional_requests elided
  └─ impact_summary
    └─ requests 1
      └─ RevokeSupportUserCommand
    └─ handlers 1
      └─ RevokeSupportUserCommandHanlder

