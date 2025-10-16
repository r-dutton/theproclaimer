[web] GET /api/binders/{binderId:guid}/automation-runs/{id:guid}/stages  (Workpapers.Next.API.Controllers.Workpapers.AutomationBots.AutomationRunsController.GetStages)  [L60–L68] status=200 [auth=AuthorizationPolicies.User]
  └─ sends_request CanIAccessBinderQuery [L63]
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
  └─ sends_request GetStageForAutomationRunQuery [L66]
    └─ handled_by Workpapers.Next.ApplicationService.Queries.Workpapers.AutomationBots.GetStagesForAutomationRunQueryHandler.Handle [L49–L138]
      └─ maps_to AutomationRunAccountDto [L83]
        └─ converts_to AutomationRunAccountModel [L962]
      └─ maps_to AutomationRunDatumDto [L95]
        └─ converts_to AutomationRunDatumModel [L965]
      └─ maps_to AutomationRunDocumentDto [L99]
        └─ converts_to AutomationRunDocumentModel [L968]
      └─ maps_to AutomationRunJournalDto [L87]
        └─ converts_to AutomationRunJournalModel [L963]
      └─ maps_to AutomationRunRecordDto [L91]
        └─ converts_to AutomationRunRecordModel [L964]
      └─ maps_to AutomationRunStageDto [L70]
        └─ converts_to AutomationRunStageModel [L961]
      └─ maps_to AutomationRunStageDto [L63]
        └─ converts_to AutomationRunStageModel [L961]
      └─ uses_service IControlledRepository<AutomationRun>
        └─ method ReadQuery [L122]
          └─ ... (no implementation details available)
      └─ uses_service IMapper
        └─ method ConfigurationProvider [L64]
          └─ ... (no implementation details available)
  └─ sends_request GetStageForAutomationRunWithDataQuery [L67]

