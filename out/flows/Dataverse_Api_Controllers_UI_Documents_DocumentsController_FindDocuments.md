[web] GET /api/ui/documents/documents/  (Dataverse.Api.Controllers.UI.Documents.DocumentsController.FindDocuments)  [L80–L136] [auth=Authentication.UserPolicy]
  └─ sends_request FindDocumentsQuery [L110]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Dataverse.ApplicationService.Queries.Documents.FindDocumentsQueryHandler.Handle [L77–L428]
      └─ uses_service FirmSettingsService
        └─ method GetCurrentSettingsAsync [L104]
      └─ uses_service IControlledRepository<Document>
        └─ method ReadQuery [L103]
      └─ uses_service IControlledRepository<DocumentTag>
        └─ method ReadQuery [L373]
      └─ uses_service UserService
        └─ method GetUserAsync [L104]

