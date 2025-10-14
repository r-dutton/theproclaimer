[web] POST /api/ui/trials/{type}/process-callback  (Dataverse.Api.Controllers.UI.Trial.TrialsController.ProcessCallback)  [L59–L69] [AllowAnonymous]
  └─ uses_service ConnectionService
    └─ method GetApiMethods [L63]
  └─ sends_request CreateNewUserReferralFromSignUpInfoCommand [L64]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Dataverse.ApplicationService.Commands.Trials.CreateNewUserReferralFromSignUpInfoCommandHandler.Handle [L21–L44]
      └─ calls TenantRepository.Add [L40]

