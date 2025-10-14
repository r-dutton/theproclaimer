[web] PUT /api/binders/{binderId}/columns  (Workpapers.Next.API.Controllers.Workpapers.BindersController.UpdateColumns)  [L258–L279] [auth=AuthorizationPolicies.User]
  └─ calls BinderRepository.WriteQuery [L264]
  └─ calls BinderColumnDataRepository.WriteQuery [L266]
  └─ writes_to Binder [L264]
    └─ reads_from Binders
  └─ writes_to BinderColumnData [L266]
    └─ reads_from BinderColumnData
  └─ uses_service IControlledRepository<Binder>
    └─ method WriteQuery [L264]
  └─ uses_service IControlledRepository<BinderColumnData>
    └─ method WriteQuery [L266]
  └─ sends_request AreBinderColumnDatasetsValidQuery [L273]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Workpapers.Next.ApplicationService.Queries.Binders.AreBinderColumnDatasetsValidQueryHandler.Handle [L29–L76]
      └─ uses_service ICirrusQueryService (AddScoped)
        └─ method GetDatasetsForFile [L42]
  └─ sends_request CanIAccessBinderQuery [L262]
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

