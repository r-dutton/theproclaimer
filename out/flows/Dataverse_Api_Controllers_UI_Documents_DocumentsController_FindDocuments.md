[web] GET /api/ui/documents/documents/  (Dataverse.Api.Controllers.UI.Documents.DocumentsController.FindDocuments)  [L80–L136] status=200 [auth=Authentication.UserPolicy]
  └─ sends_request FindDocumentsQuery [L110]
    └─ handled_by Dataverse.ApplicationService.Queries.Documents.FindDocumentsQueryHandler.Handle [L77–L428]
      └─ uses_service FirmSettingsService
        └─ method GetCurrentSettingsAsync [L104]
          └─ implementation Dataverse.ApplicationService.Services.FirmSettingsService.GetCurrentSettingsAsync [L18-L97]
      └─ uses_service UserService
        └─ method GetUserAsync [L104]
          └─ implementation Dataverse.ApplicationService.Services.UserService.GetUserAsync [L15-L185]
      └─ uses_service IControlledRepository<Document>
        └─ method ReadQuery [L103]
          └─ ... (no implementation details available)
      └─ uses_service IControlledRepository<DocumentTag>
        └─ method ReadQuery [L373]
          └─ ... (no implementation details available)

