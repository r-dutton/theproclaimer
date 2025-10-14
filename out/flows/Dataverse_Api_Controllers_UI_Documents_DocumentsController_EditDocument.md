[web] GET /api/ui/documents/documents/{id}/edit  (Dataverse.Api.Controllers.UI.Documents.DocumentsController.EditDocument)  [L292–L299] [auth=Authentication.UserPolicy]
  └─ sends_request EditDocumentCommand [L296]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Dataverse.ApplicationService.Commands.Documents.EditDocumentCommandHandler.Handle [L41–L125]
      └─ uses_service DocumentServiceFactory
        └─ method GetDocumentWithService [L83]
      └─ uses_service IControlledRepository<DocumentAuditLog>
        └─ method Add [L106]
      └─ uses_service IControlledRepository<DocumentVersion>
        └─ method WriteQuery [L71]
      └─ uses_service RequestProcessor
        └─ method ProcessAsync [L82]
      └─ uses_service UserService
        └─ method GetUserId [L111]
  └─ sends_request CanIAccessDocumentQuery [L295]
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

