[web] DELETE /api/ui/documents/documents/{id:guid}/hard  (Dataverse.Api.Controllers.UI.Documents.DocumentsController.DeleteDocument)  [L343–L348] [auth=Authentication.UserPolicy,Authentication.AdminPolicy]
  └─ sends_request DeleteDocumentCommand [L347]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Dataverse.ApplicationService.Commands.Documents.DeleteDocumentCommandHandler.Handle [L22–L45]
      └─ uses_service IControlledRepository<Document>
        └─ method WriteQuery [L37]
  └─ sends_request CanIAccessDocumentQuery [L346]
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

