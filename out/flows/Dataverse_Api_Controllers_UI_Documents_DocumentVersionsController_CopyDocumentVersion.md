[web] POST /api/ui/documents/{documentId:guid}/versions/copy  (Dataverse.Api.Controllers.UI.Documents.DocumentVersionsController.CopyDocumentVersion)  [L75–L81] [auth=Authentication.UserPolicy]
  └─ sends_request CanIAccessDocumentQuery [L79]
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
  └─ sends_request CopyDocumentVersionCommand [L80]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Dataverse.Services.DocumentStorage.Commands.CopyDocumentVersionCommandHandler.Handle [L30–L124]
      └─ uses_service DocumentServiceFactory
        └─ method GetDocumentWithService [L89]
      └─ uses_service IControlledRepository<DocumentVersion>
        └─ method WriteQuery [L54]
      └─ uses_service UserService
        └─ method GetUserId [L79]

