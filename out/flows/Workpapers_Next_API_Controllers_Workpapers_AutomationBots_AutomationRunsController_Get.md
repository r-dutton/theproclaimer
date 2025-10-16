[web] GET /api/binders/{binderId:guid}/automation-runs/{id:guid}  (Workpapers.Next.API.Controllers.Workpapers.AutomationBots.AutomationRunsController.Get)  [L34–L42] status=200 [auth=AuthorizationPolicies.User]
  └─ sends_request CanIAccessBinderQuery [L37]
    └─ handled_by Workpapers.Next.ApplicationService.Queries.Binders.CanIAccessBinderQueryHandler.Handle [L60–L126]
      └─ uses_service UserService
        └─ method GetUserId [L91]
          └─ implementation Workpapers.Next.ApplicationService.Services.UserService.GetUserId [L20-L295]
          └─ implementation Workpapers.Next.ApplicationService.Services.UserService.GetUserId [L20-L295]
      └─ uses_service RequestInfoService
        └─ method IsValidServiceAccountRequest [L89]
          └─ implementation Workpapers.Next.Services.Features.RequestInfoService.IsValidServiceAccountRequest [L11-L84]
          └─ implementation Workpapers.Next.Services.Features.RequestInfoService.IsValidServiceAccountRequest [L11-L84]
          └─ implementation Workpapers.Next.Services.Features.RequestInfoService.IsValidServiceAccountRequest [L11-L84]
      └─ uses_service TenantService
        └─ method GetCurrentTenant [L92]
          └─ implementation Workpapers.Next.Services.Features.Tenants.TenantService.GetCurrentTenant [L5-L22]
            └─ uses_service TenantIdentificationService
              └─ method GetCurrentTenant [L20]
                └─ implementation Workpapers.Next.ApplicationService.Services.TenantIdentificationService.GetCurrentTenant [L15-L131]
      └─ uses_service IControlledRepository<Binder>
        └─ method ReadQuery [L101]
          └─ ... (no implementation details available)
      └─ uses_service RequestProcessor
        └─ method ProcessAsync [L117]
          └─ implementation Workpapers.Next.Services.Features.Requests.RequestProcessor.ProcessAsync [L9-L32]
            └─ constructs RequestProcessorWrapper<TRequest,TResult>
            └─ resolves IPipelineBehavior<TRequest,TResult> chain
            └─ invokes IAsyncRequestHandler<TRequest,TResult>.Handle
      └─ uses_cache IDistributedCache.SetRecordAsync [write] [L121]
      └─ uses_cache IDistributedCache.DoesRecordExistAsync [access] [L109]
      └─ uses_cache IDistributedCache.CreateAccessKey [write] [L92]
  └─ sends_request GetAutomationRunLightQuery [L40]
  └─ sends_request GetAutomationRunQuery [L41]
    └─ handled_by Workpapers.Next.ApplicationService.Queries.Workpapers.AutomationBots.GetAutomationRunQueryHandler.Handle [L49–L118]
      └─ maps_to AutomationRunDto [L70]
        └─ automapper.registration WorkpapersMappingProfile (AutomationRun->AutomationRunDto) [L920]
        └─ converts_to AutomationRunUpdateModel [L960]
      └─ maps_to AutomationRunLightDto [L85]
        └─ automapper.registration WorkpapersMappingProfile (AutomationRun->AutomationRunLightDto) [L914]
      └─ maps_to AutomationRunStageDto [L89]
        └─ converts_to AutomationRunStageModel [L961]
      └─ uses_service IControlledRepository<AutomationRun>
        └─ method ReadQuery [L70]
          └─ ... (no implementation details available)
      └─ uses_service IControlledRepository<Binder>
        └─ method ReadQuery [L74]
          └─ ... (no implementation details available)
      └─ uses_service IMapper
        └─ method ConfigurationProvider [L93]
          └─ ... (no implementation details available)
      └─ uses_service BotService
        └─ method GetStageDefinitions [L107]
          └─ implementation Workpapers.Next.ApplicationService.Features.AutomationBots.Services.BotService.GetStageDefinitions [L22-L111]

