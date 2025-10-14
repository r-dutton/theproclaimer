[web] PUT /api/binders/{id:Guid}  (Workpapers.Next.API.Controllers.Workpapers.BindersController.Update)  [L281–L311] [auth=AuthorizationPolicies.User]
  └─ calls BinderRepository.WriteQuery [L285]
  └─ calls SourceRepository.ReadQuery [L297]
  └─ writes_to Binder [L285]
    └─ reads_from Binders
  └─ queries Source [L297]
  └─ uses_service IControlledRepository<Binder>
    └─ method WriteQuery [L285]
  └─ uses_service IControlledRepository<Source>
    └─ method ReadQuery [L297]
  └─ sends_request AreBinderColumnDatasetsValidQuery [L304]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Workpapers.Next.ApplicationService.Queries.Binders.AreBinderColumnDatasetsValidQueryHandler.Handle [L29–L76]
      └─ uses_service ICirrusQueryService (AddScoped)
        └─ method GetDatasetsForFile [L42]
  └─ sends_request CanIAccessBinderQuery [L291]
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

