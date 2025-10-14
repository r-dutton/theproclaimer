[web] GET /api/journals/for-binder-column/{binderColumnId:guid}  (Workpapers.Next.API.Controllers.Workpapers.JournalController.GetForBinderColumn)  [L118–L137] [auth=AuthorizationPolicies.User]
  └─ maps_to JournalLightDto [L131]
  └─ calls JournalRepository.ReadQuery [L129]
  └─ calls BinderRepository.ReadQuery [L121]
  └─ queries Binder [L121]
    └─ reads_from Binders
  └─ queries Journal [L129]
  └─ uses_service IControlledRepository<Binder>
    └─ method ReadQuery [L121]
  └─ uses_service IControlledRepository<Journal>
    └─ method ReadQuery [L129]
  └─ sends_request CanIAccessBinderQuery [L125]
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

