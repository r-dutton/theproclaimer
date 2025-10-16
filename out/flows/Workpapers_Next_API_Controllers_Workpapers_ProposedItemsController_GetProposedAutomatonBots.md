[web] GET /api/proposed-items/automation-bots  (Workpapers.Next.API.Controllers.Workpapers.ProposedItemsController.GetProposedAutomatonBots)  [L79–L96] status=200 [auth=AuthorizationPolicies.User]
  └─ maps_to ProposedAutomationRunDto [L86]
    └─ automapper.registration WorkpapersMappingProfile (ProposedItem->ProposedAutomationRunDto) [L974]
  └─ calls BinderRepository.ReadQuery [L83]
  └─ calls ProposedItemRepository.ReadQuery [L86]
  └─ query Binder [L83]
    └─ reads_from Binders
  └─ query ProposedItem [L86]
    └─ reads_from ProposedItems
  └─ uses_service IControlledRepository<Binder>
    └─ method ReadQuery [L83]
      └─ ... (no implementation details available)
  └─ uses_service IControlledRepository<ProposedItem>
    └─ method ReadQuery [L86]
      └─ ... (no implementation details available)
  └─ sends_request CanIAccessBinderQuery [L82]
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

