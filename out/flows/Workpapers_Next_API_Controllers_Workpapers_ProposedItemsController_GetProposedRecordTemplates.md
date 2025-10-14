[web] GET /api/proposed-items/record-templates  (Workpapers.Next.API.Controllers.Workpapers.ProposedItemsController.GetProposedRecordTemplates)  [L49–L77] [auth=AuthorizationPolicies.User]
  └─ maps_to ProposedRecordTemplateDto [L54]
    └─ automapper.registration WorkpapersMappingProfile (ProposedItem->ProposedRecordTemplateDto) [L984]
  └─ calls ProposedItemRepository.ReadQuery [L54]
  └─ queries ProposedItem [L54]
    └─ reads_from ProposedItems
  └─ uses_service IControlledRepository<ProposedItem>
    └─ method ReadQuery [L54]
  └─ sends_request CanIAccessBinderQuery [L52]
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
  └─ sends_request FilterValidRecordTemplatesForBinderQuery [L69]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Workpapers.Next.ApplicationService.Queries.Workpapers.GetValidRecordTemplatesForBinderQueryHandler.Handle [L34–L103]
      └─ uses_service IControlledRepository<Binder>
        └─ method ReadQuery [L59]
      └─ uses_service IControlledRepository<WorkpaperRecordTemplate>
        └─ method ReadQuery [L65]
      └─ uses_service UnitOfWork
        └─ method Table [L75]
      └─ uses_service UserService
        └─ method GetFirmId [L71]

