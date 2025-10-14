[web] GET /api/ui/documents/documents/{id}/can-i-check-in  (Dataverse.Api.Controllers.UI.Documents.DocumentsController.CanICheckInDocument)  [L225–L232] [auth=Authentication.UserPolicy]
  └─ sends_request CanIAccessDocumentQuery [L228]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Dataverse.ApplicationService.Queries.Documents.CanIAccessDocumentQueryHandler.Handle [L37–L82]
      └─ uses_service FirmSettingsService
        └─ method GetCurrentSettingsAsync [L66]
      └─ uses_service IControlledRepository<Document>
        └─ method ReadQuery [L68]
      └─ uses_service RequestInfoService
        └─ method IsValidServiceAccountRequest [L58]
      └─ uses_service UserService
        └─ method GetUserAsync [L61]
  └─ sends_request CanICheckInDocumentQuery [L229]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Dataverse.ApplicationService.Queries.Documents.CanICheckInDocumentQueryHandler.Handle [L29–L74]
      └─ uses_service IControlledRepository<Document>
        └─ method ReadQuery [L49]
      └─ uses_service UserService
        └─ method IsInRole [L47]

