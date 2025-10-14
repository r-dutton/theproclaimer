[web] DELETE /api/ui/documents/documents/  (Dataverse.Api.Controllers.UI.Documents.DocumentsController.BulkDeleteDocuments)  [L350–L371] [auth=Authentication.UserPolicy]
  └─ calls DocumentRepository.WriteQuery [L354]
  └─ writes_to Document [L354]
    └─ reads_from Documents
  └─ uses_service IControlledRepository<Document>
    └─ method WriteQuery [L354]
  └─ sends_request RecycleDocumentCommand [L369]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Dataverse.ApplicationService.Commands.Documents.RecycleDocumentCommandHandler.Handle [L28–L66]
      └─ uses_service IControlledRepository<Document>
        └─ method WriteQuery [L49]
      └─ uses_service IControlledRepository<DocumentVersion>
        └─ method WriteQuery [L51]
      └─ uses_service RequestProcessor
        └─ method ProcessAsync [L60]
  └─ sends_request CanIAccessDocumentQuery [L368]
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

