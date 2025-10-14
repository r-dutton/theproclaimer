[web] DELETE /api/binder-sections/{id:Guid}  (Workpapers.Next.API.Controllers.Workpapers.BinderSectionsController.Delete)  [L100–L114] [auth=AuthorizationPolicies.User]
  └─ calls BinderSectionRepository.Remove [L111]
  └─ calls BinderSectionRepository.WriteQuery [L103]
  └─ writes_to BinderSection [L111]
    └─ reads_from BinderSections
  └─ writes_to BinderSection [L103]
    └─ reads_from BinderSections
  └─ uses_service IControlledRepository<BinderSection>
    └─ method WriteQuery [L103]
  └─ sends_request CanIAccessBinderQuery [L108]
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
  └─ sends_request CanIDeleteBinderSectionQuery [L109]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Workpapers.Next.ApplicationService.Queries.Workpapers.CanIDeleteBinderSectionQueryHandler.Handle [L17–L34]

