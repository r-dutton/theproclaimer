[web] POST /api/ui/trials/  (Dataverse.Api.Controllers.UI.Trial.TrialsController.CreateTrialTenant)  [L39–L44] status=201 [AllowAnonymous]
  └─ sends_request CreateTrialTenantCommand -> CreateTrialTenantCommandHandler [L43]
    └─ handled_by Dataverse.ApplicationService.Commands.Trials.CreateTrialTenantCommandHandler.Handle [L32–L138]
      └─ calls TenantRepository.WriteTable [L53]
      └─ uses_service TenantCreationConfig
        └─ method AutomatedCreationUserLimit [L71]
          └─ ... (no implementation details available)
      └─ uses_service TrialConfig
        └─ method TestingMode [L60]
          └─ ... (no implementation details available)
      └─ uses_service RequestProcessor
        └─ method ProcessAsync [L58]
          └─ resolves_request Dataverse.Services.Features.Requests.RequestProcessor.ProcessAsync
          └─ resolves_request Cirrus.Services.Features.Requests.RequestProcessor.ProcessAsync
          └─ resolves_request DataGet.Services.Features.Requests.RequestProcessor.ProcessAsync
          └─ +1 additional_requests elided
  └─ impact_summary
    └─ requests 1
      └─ CreateTrialTenantCommand
    └─ handlers 1
      └─ CreateTrialTenantCommandHandler

