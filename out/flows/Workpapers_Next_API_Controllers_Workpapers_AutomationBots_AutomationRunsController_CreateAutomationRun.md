[web] POST /api/binders/{binderId:guid}/automation-runs  (Workpapers.Next.API.Controllers.Workpapers.AutomationBots.AutomationRunsController.CreateAutomationRun)  [L70–L80] status=201 [auth=AuthorizationPolicies.User]
  └─ sends_request CreateAutomationRunCommand [L77]
    └─ handled_by Workpapers.Next.ApplicationService.Commands.Workpapers.AutomationBots.CreateAutomationRunCommandHandler.Handle [L30–L86]
      └─ maps_to AutomationRunDto [L72]
        └─ converts_to AutomationRunUpdateModel [L960]
      └─ uses_service IBotService (AddScoped)
        └─ method GetBot [L62]
          └─ implementation Workpapers.Next.ApplicationService.Features.AutomationBots.Services.BotService.GetBot [L22-L111]
      └─ uses_service IControlledRepository<AutomationRun>
        └─ method Add [L70]
          └─ ... (no implementation details available)
      └─ uses_service IControlledRepository<Binder>
        └─ method ReadQuery [L54]
          └─ ... (no implementation details available)
      └─ uses_service IControlledRepository<SourceAccount>
        └─ method ReadQuery [L64]
          └─ ... (no implementation details available)
      └─ uses_service IMapper
        └─ method Map [L72]
          └─ ... (no implementation details available)
  └─ sends_request CanIAccessBinderQuery [L74]
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

