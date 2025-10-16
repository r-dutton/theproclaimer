[web] PUT /api/proposed-items/automation-bots/refresh  (Workpapers.Next.API.Controllers.Workpapers.ProposedItemsController.RefreshAutomationBots)  [L141–L158] status=200 [auth=AuthorizationPolicies.User]
  └─ calls BinderRepository.WriteQuery [L144]
  └─ write Binder [L144]
    └─ reads_from Binders
  └─ uses_service IControlledRepository<Binder>
    └─ method WriteQuery [L144]
      └─ ... (no implementation details available)
  └─ sends_request CreateUpdateProposedItemsCommand [L155]
    └─ handled_by Workpapers.Next.ApplicationService.Commands.Workpapers.ProposedItems.CreateUpdateProposedItemsCommandHandler.Handle [L36–L184]
      └─ uses_service IControlledRepository<ProposedItem>
        └─ method WriteQuery [L63]
          └─ ... (no implementation details available)
  └─ sends_request CanIAccessBinderQuery [L152]
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
  └─ sends_request GetAutomationProposalsQuery [L154]
    └─ handled_by Workpapers.Next.ApplicationService.Queries.Workpapers.AutomationBots.GetAutomationProposalsQueryHandler.Handle [L35–L222]
      └─ uses_service IControlledRepository<AutomationRun>
        └─ method ReadQuery [L96]
          └─ ... (no implementation details available)
      └─ uses_service IControlledRepository<Binder>
        └─ method ReadQuery [L59]
          └─ ... (no implementation details available)
      └─ uses_service IControlledRepository<SourceAccount>
        └─ method ReadQuery [L103]
          └─ ... (no implementation details available)
      └─ uses_service RequestProcessor
        └─ method ProcessAsync [L78]
          └─ implementation Workpapers.Next.Services.Features.Requests.RequestProcessor.ProcessAsync [L9-L32]
            └─ constructs RequestProcessorWrapper<TRequest,TResult>
            └─ resolves IPipelineBehavior<TRequest,TResult> chain
            └─ invokes IAsyncRequestHandler<TRequest,TResult>.Handle
      └─ uses_service BotService
        └─ method GetBotDefinitions [L66]
          └─ implementation Workpapers.Next.ApplicationService.Features.AutomationBots.Services.BotService.GetBotDefinitions [L22-L111]

