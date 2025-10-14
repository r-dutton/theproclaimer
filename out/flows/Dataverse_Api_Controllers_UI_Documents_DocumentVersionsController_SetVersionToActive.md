[web] PUT /api/ui/documents/{documentId:guid}/versions/{documentVersionId:guid}  (Dataverse.Api.Controllers.UI.Documents.DocumentVersionsController.SetVersionToActive)  [L83–L89] [auth=Authentication.UserPolicy]
  └─ sends_request SetDocumentVersionToActiveCommand [L88]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Dataverse.ApplicationService.Commands.Documents.SetDocumentVersionToActiveCommandHandler.Handle [L25–L49]
      └─ uses_service IControlledRepository<DocumentVersion>
        └─ method WriteQuery [L40]
  └─ sends_request CanIAccessDocumentQuery [L87]
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

