[web] PUT /api/proposed-items/decline  (Workpapers.Next.API.Controllers.Workpapers.ProposedItemsController.DeclineProposal)  [L160–L175] [auth=AuthorizationPolicies.User]
  └─ calls BinderRepository.ReadQuery [L163]
  └─ calls ProposedItemRepository.WriteQuery [L169]
  └─ queries Binder [L163]
    └─ reads_from Binders
  └─ writes_to ProposedItem [L169]
    └─ reads_from ProposedItems
  └─ uses_service IControlledRepository<Binder>
    └─ method ReadQuery [L163]
  └─ uses_service IControlledRepository<ProposedItem>
    └─ method WriteQuery [L169]
  └─ sends_request CanIAccessBinderQuery [L167]
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

