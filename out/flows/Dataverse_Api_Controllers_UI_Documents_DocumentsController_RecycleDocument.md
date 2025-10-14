[web] DELETE /api/ui/documents/documents/{id:guid}  (Dataverse.Api.Controllers.UI.Documents.DocumentsController.RecycleDocument)  [L325–L341] [auth=Authentication.UserPolicy]
  └─ calls DocumentRepository.WriteQuery [L329]
  └─ writes_to Document [L329]
    └─ reads_from Documents
  └─ uses_service IControlledRepository<Document>
    └─ method WriteQuery [L329]
  └─ sends_request RecycleDocumentCommand [L340]
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
  └─ sends_request CanIAccessDocumentQuery [L339]
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

