[web] DELETE /api/ui/documents/{documentId:guid}/versions/{documentVersionId:guid}  (Dataverse.Api.Controllers.UI.Documents.DocumentVersionsController.RecycleDocumentVersion)  [L91–L97] [auth=Authentication.UserPolicy]
  └─ sends_request RecycleDocumentVersionCommand [L96]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Dataverse.ApplicationService.Commands.Documents.RecycleDocumentVersionCommandHandler.Handle [L26–L51]
      └─ uses_service IControlledRepository<DocumentVersion>
        └─ method WriteQuery [L41]
  └─ sends_request CanIAccessDocumentQuery [L95]
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

