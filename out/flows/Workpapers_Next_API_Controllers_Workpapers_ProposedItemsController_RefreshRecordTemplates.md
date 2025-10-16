[web] PUT /api/proposed-items/record-templates/refresh  (Workpapers.Next.API.Controllers.Workpapers.ProposedItemsController.RefreshRecordTemplates)  [L117–L139] status=200 [auth=AuthorizationPolicies.User]
  └─ calls BinderRepository.WriteQuery [L120]
  └─ write Binder [L120]
    └─ reads_from Binders
  └─ uses_service IControlledRepository<Binder>
    └─ method WriteQuery [L120]
      └─ ... (no implementation details available)
  └─ sends_request CreateUpdateProposedItemsCommand [L136]
    └─ handled_by Workpapers.Next.ApplicationService.Commands.Workpapers.ProposedItems.CreateUpdateProposedItemsCommandHandler.Handle [L36–L184]
      └─ uses_service IControlledRepository<ProposedItem>
        └─ method WriteQuery [L63]
          └─ ... (no implementation details available)
  └─ sends_request CanIAccessBinderQuery [L128]
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
  └─ sends_request GetProposedRecordTemplatesQuery [L135]
    └─ handled_by Workpapers.Next.ApplicationService.Queries.Workpapers.GetProposedRecordTemplatesQueryHandler.Handle [L45–L242]
      └─ maps_to SourceAccountUltraLightDto [L106]
        └─ automapper.registration CirrusMappingProfile (SourceAccount->SourceAccountUltraLightDto) [L260]
        └─ automapper.registration WorkpapersMappingProfile (SourceAccount->SourceAccountUltraLightDto) [L615]
      └─ maps_to BinderTemplateDto [L95]
        └─ automapper.registration WorkpapersMappingProfile (BinderTemplate->BinderTemplateDto) [L726]
      └─ maps_to WorkpaperRecordTemplateUltraLightDto [L167]
        └─ automapper.registration WorkpapersMappingProfile (WorkpaperRecordTemplate->WorkpaperRecordTemplateUltraLightDto) [L216]
      └─ maps_to WorkpaperRecordTemplateUltraLightDto [L129]
        └─ automapper.registration WorkpapersMappingProfile (WorkpaperRecordTemplate->WorkpaperRecordTemplateUltraLightDto) [L216]
      └─ uses_service IControlledRepository<Binder>
        └─ method ReadQuery [L83]
          └─ ... (no implementation details available)
      └─ uses_service IControlledRepository<BinderTemplate>
        └─ method ReadQuery [L95]
          └─ ... (no implementation details available)
      └─ uses_service IControlledRepository<Client>
        └─ method ReadQuery [L89]
          └─ ... (no implementation details available)
      └─ uses_service IControlledRepository<RollOverRecord>
        └─ method ReadQuery [L156]
          └─ ... (no implementation details available)
      └─ uses_service IControlledRepository<SourceAccount>
        └─ method ReadQuery [L106]
          └─ ... (no implementation details available)
      └─ uses_service IControlledRepository<WorkpaperRecordTemplate>
        └─ method ReadQuery [L129]
          └─ ... (no implementation details available)
      └─ uses_service IMapper
        └─ method ConfigurationProvider [L109]
          └─ ... (no implementation details available)
      └─ uses_service RequestProcessor
        └─ method ProcessAsync [L98]
          └─ implementation Workpapers.Next.Services.Features.Requests.RequestProcessor.ProcessAsync [L9-L32]
            └─ constructs RequestProcessorWrapper<TRequest,TResult>
            └─ resolves IPipelineBehavior<TRequest,TResult> chain
            └─ invokes IAsyncRequestHandler<TRequest,TResult>.Handle
      └─ uses_service UnitOfWork
        └─ method LocalTable [L154]
          └─ ... (no implementation details available)

