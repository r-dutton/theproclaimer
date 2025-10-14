[web] POST /api/ui/documents/{documentId:guid}/versions/  (Dataverse.Api.Controllers.UI.Documents.DocumentVersionsController.AddDocumentVersion)  [L67–L73] [auth=Authentication.UserPolicy]
  └─ sends_request AddDocumentVersionCommand [L72]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Dataverse.ApplicationService.Commands.Documents.AddDocumentVersionCommandHandler.Handle [L27–L94]
      └─ uses_service IControlledRepository<Document>
        └─ method WriteQuery [L57]
      └─ uses_service IControlledRepository<DocumentStatus>
        └─ method WriteQuery [L78]
      └─ uses_service IControlledRepository<DocumentVersion>
        └─ method ReadQuery [L61]
      └─ uses_service RequestInfoService
        └─ method IsM2MRequest [L67]
      └─ uses_service RequestProcessor
        └─ method ProcessAsync [L88]
      └─ uses_service UserService
        └─ method GetUserId [L66]
  └─ sends_request CanIAccessDocumentQuery [L71]
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

