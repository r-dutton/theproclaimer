[web] DELETE /api/ui/documents/{documentId:guid}/versions/{documentVersionId:guid}/hard  (Dataverse.Api.Controllers.UI.Documents.DocumentVersionsController.DeleteDocumentVersion)  [L99–L106] [auth=Authentication.UserPolicy,Authentication.AdminPolicy]
  └─ sends_request DeleteDocumentVersionCommand [L105]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Dataverse.ApplicationService.Commands.Documents.DeleteDocumentVersionCommandHandler.Handle [L25–L50]
      └─ uses_service IControlledRepository<DocumentVersion>
        └─ method WriteQuery [L40]
  └─ sends_request CanIAccessDocumentQuery [L104]
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

