[web] PUT /api/matters/bulk  (Workpapers.Next.API.Controllers.Workpapers.MattersController.BulkUpdate)  [L122–L142] [auth=AuthorizationPolicies.User]
  └─ calls MatterRepository.WriteQuery [L128]
  └─ writes_to Matter [L128]
    └─ reads_from Matters
  └─ uses_service IControlledRepository<Matter>
    └─ method WriteQuery [L128]
  └─ sends_request CanIAccessBinderQuery [L134]
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

