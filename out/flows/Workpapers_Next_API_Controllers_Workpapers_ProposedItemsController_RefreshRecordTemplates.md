[web] PUT /api/proposed-items/record-templates/refresh  (Workpapers.Next.API.Controllers.Workpapers.ProposedItemsController.RefreshRecordTemplates)  [L117–L139] [auth=AuthorizationPolicies.User]
  └─ calls BinderRepository.WriteQuery [L120]
  └─ writes_to Binder [L120]
    └─ reads_from Binders
  └─ uses_service IControlledRepository<Binder>
    └─ method WriteQuery [L120]
  └─ sends_request CreateUpdateProposedItemsCommand [L136]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Workpapers.Next.ApplicationService.Commands.Workpapers.ProposedItems.CreateUpdateProposedItemsCommandHandler.Handle [L36–L184]
      └─ uses_service IControlledRepository<ProposedItem>
        └─ method WriteQuery [L63]
  └─ sends_request CanIAccessBinderQuery [L128]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Workpapers.Next.ApplicationService.Queries.Binders.CanIAccessBinderQueryHandler.Handle [L60–L126]
      └─ uses_service IControlledRepository<Binder>
        └─ method ReadQuery [L101]
      └─ uses_service RequestInfoService
        └─ method IsValidServiceAccountRequest [L89]
      └─ uses_service RequestProcessor
        └─ method ProcessAsync [L117]
      └─ uses_service TenantService
        └─ method GetCurrentTenant [L92]
      └─ uses_service UserService
        └─ method GetUserId [L91]
      └─ uses_cache IDistributedCache [L121]
        └─ method SetRecordAsync [write] [L121]
      └─ uses_cache IDistributedCache [L109]
        └─ method DoesRecordExistAsync [access] [L109]
      └─ uses_cache IDistributedCache [L92]
        └─ method CreateAccessKey [write] [L92]
  └─ sends_request GetProposedRecordTemplatesQuery [L135]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
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
      └─ uses_service IControlledRepository<BinderTemplate>
        └─ method ReadQuery [L95]
      └─ uses_service IControlledRepository<Client>
        └─ method ReadQuery [L89]
      └─ uses_service IControlledRepository<RollOverRecord>
        └─ method ReadQuery [L156]
      └─ uses_service IControlledRepository<SourceAccount>
        └─ method ReadQuery [L106]
      └─ uses_service IControlledRepository<WorkpaperRecordTemplate>
        └─ method ReadQuery [L129]
      └─ uses_service IMapper
        └─ method ConfigurationProvider [L109]
      └─ uses_service RequestProcessor
        └─ method ProcessAsync [L98]
      └─ uses_service UnitOfWork
        └─ method LocalTable [L154]

