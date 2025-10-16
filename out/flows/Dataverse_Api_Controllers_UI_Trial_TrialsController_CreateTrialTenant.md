[web] POST /api/ui/trials/  (Dataverse.Api.Controllers.UI.Trial.TrialsController.CreateTrialTenant)  [L39–L44] status=201 [AllowAnonymous]
  └─ sends_request CreateTrialTenantCommand [L43]
    └─ handled_by Dataverse.ApplicationService.Commands.Trials.CreateTrialTenantCommandHandler.Handle [L32–L138]
      └─ calls TenantRepository.WriteTable [L53]
      └─ uses_service RequestProcessor
        └─ method ProcessAsync [L58]
          └─ implementation Dataverse.Services.Features.Requests.RequestProcessor.ProcessAsync [L8-L45]
            └─ constructs RequestProcessorWrapper<TRequest,TResult>
            └─ resolves IPipelineBehavior<TRequest,TResult> chain
            └─ invokes IAsyncRequestHandler<TRequest,TResult>.Handle
      └─ uses_service TenantCreationConfig
        └─ method AutomatedCreationUserLimit [L71]
          └─ ... (no implementation details available)
      └─ uses_service TrialConfig
        └─ method TestingMode [L60]
          └─ ... (no implementation details available)

