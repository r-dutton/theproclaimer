[web] POST /api/ui/trials/  (Dataverse.Api.Controllers.UI.Trial.TrialsController.CreateTrialTenant)  [L39–L44] [AllowAnonymous]
  └─ sends_request CreateTrialTenantCommand [L43]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Dataverse.ApplicationService.Commands.Trials.CreateTrialTenantCommandHandler.Handle [L32–L138]
      └─ calls TenantRepository.WriteTable [L53]
      └─ uses_service RequestProcessor
        └─ method ProcessAsync [L58]
      └─ uses_service TenantCreationConfig
        └─ method AutomatedCreationUserLimit [L71]
      └─ uses_service TrialConfig
        └─ method TestingMode [L60]

