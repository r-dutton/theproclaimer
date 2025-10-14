[web] PUT /api/ui/documents/documents/{id}  (Dataverse.Api.Controllers.UI.Documents.DocumentsController.UpdateDocument)  [L285–L290] [auth=Authentication.UserPolicy]
  └─ sends_request UpdateDocumentCommand [L289]
  └─ sends_request CanIAccessDocumentQuery [L288]
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

