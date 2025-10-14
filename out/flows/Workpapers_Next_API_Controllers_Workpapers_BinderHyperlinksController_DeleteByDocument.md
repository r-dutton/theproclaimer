[web] DELETE /api/binders/{binderId:guid}/hyperlinks/by-document/{documentId:guid}  (Workpapers.Next.API.Controllers.Workpapers.BinderHyperlinksController.DeleteByDocument)  [L29–L38] [auth=AuthorizationPolicies.User]
  └─ sends_request DeleteHyperlinksLinkedToDocumentCommand [L35]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Workpapers.Next.ApplicationService.Commands.Workpapers.Binders.DeleteHyperlinksLinkedToDocumentCommandHandler.Handle [L42–L134]
      └─ uses_service IControlledRepository<Matter>
        └─ method WriteQuery [L113]
      └─ uses_service IControlledRepository<WorkpaperRecord>
        └─ method WriteQuery [L96]
      └─ uses_service IControlledRepository<Worksheet>
        └─ method WriteQuery [L79]
  └─ sends_request CanIAccessBinderQuery [L34]
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

