[web] POST /api/ui/trials/{type}/process-callback  (Dataverse.Api.Controllers.UI.Trial.TrialsController.ProcessCallback)  [L59–L69] status=201 [AllowAnonymous]
  └─ uses_service ConnectionService
    └─ method GetApiMethods [L63]
      └─ implementation Dataverse.ApplicationService.Services.ConnectionService.GetApiMethods [L9-L19]
  └─ sends_request CreateNewUserReferralFromSignUpInfoCommand -> CreateNewUserReferralFromSignUpInfoCommandHandler [L64]
    └─ handled_by Dataverse.ApplicationService.Commands.Trials.CreateNewUserReferralFromSignUpInfoCommandHandler.Handle [L21–L44]
      └─ calls TenantRepository.Add [L40]
  └─ impact_summary
    └─ services 1
      └─ ConnectionService
    └─ requests 1
      └─ CreateNewUserReferralFromSignUpInfoCommand
    └─ handlers 1
      └─ CreateNewUserReferralFromSignUpInfoCommandHandler

