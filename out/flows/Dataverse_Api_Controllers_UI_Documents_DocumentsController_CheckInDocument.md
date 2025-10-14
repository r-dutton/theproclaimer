[web] POST /api/ui/documents/documents/{id}/check-in  (Dataverse.Api.Controllers.UI.Documents.DocumentsController.CheckInDocument)  [L234–L244] [auth=Authentication.UserPolicy]
  └─ sends_request CheckInDocumentCommand [L241]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Dataverse.ApplicationService.Commands.Documents.CheckInDocumentCommandHandler.Handle [L35–L109]
      └─ uses_service DocumentServiceFactory
        └─ method GetDefaultColdStorage [L93]
      └─ uses_service IControlledRepository<Contact>
        └─ method ReadQuery [L101]
      └─ uses_service IControlledRepository<DocumentVersion>
        └─ method WriteQuery [L67]
      └─ uses_service RequestProcessor
        └─ method ProcessAsync [L65]
      └─ uses_service UploadStorageService
        └─ method CopyDocument [L81]
      └─ uses_service UserService
        └─ method GetUserId [L83]
  └─ sends_request CanIAccessDocumentQuery [L240]
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

