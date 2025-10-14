[web] GET /api/proposed-items/automation-bots  (Workpapers.Next.API.Controllers.Workpapers.ProposedItemsController.GetProposedAutomatonBots)  [L79–L96] [auth=AuthorizationPolicies.User]
  └─ maps_to ProposedAutomationRunDto [L86]
    └─ automapper.registration WorkpapersMappingProfile (ProposedItem->ProposedAutomationRunDto) [L974]
  └─ calls BinderRepository.ReadQuery [L83]
  └─ calls ProposedItemRepository.ReadQuery [L86]
  └─ queries Binder [L83]
    └─ reads_from Binders
  └─ queries ProposedItem [L86]
    └─ reads_from ProposedItems
  └─ uses_service IControlledRepository<Binder>
    └─ method ReadQuery [L83]
  └─ uses_service IControlledRepository<ProposedItem>
    └─ method ReadQuery [L86]
  └─ sends_request CanIAccessBinderQuery [L82]
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

