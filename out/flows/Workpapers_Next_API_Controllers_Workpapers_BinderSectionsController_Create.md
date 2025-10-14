[web] POST /api/binder-sections/  (Workpapers.Next.API.Controllers.Workpapers.BinderSectionsController.Create)  [L68–L83] [auth=AuthorizationPolicies.User]
  └─ maps_to BinderSectionDto [L82]
  └─ calls BinderSectionRepository.Add [L80]
  └─ calls BinderSectionRepository.WriteQuery [L73]
  └─ writes_to BinderSection [L80]
    └─ reads_from BinderSections
  └─ writes_to BinderSection [L73]
    └─ reads_from BinderSections
  └─ uses_service IControlledRepository<BinderSection>
    └─ method WriteQuery [L73]
  └─ uses_service IMapper
    └─ method Map [L82]
  └─ sends_request CanIAccessBinderQuery [L71]
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

