[web] PUT /api/ui/documents/{documentId:guid}/versions/{documentVersionId:guid}  (Dataverse.Api.Controllers.UI.Documents.DocumentVersionsController.SetVersionToActive)  [L83–L89] status=200 [auth=Authentication.UserPolicy]
  └─ sends_request SetDocumentVersionToActiveCommand [L88]
    └─ handled_by Dataverse.ApplicationService.Commands.Documents.SetDocumentVersionToActiveCommandHandler.Handle [L25–L49]
      └─ uses_service IControlledRepository<DocumentVersion>
        └─ method WriteQuery [L40]
          └─ ... (no implementation details available)
  └─ sends_request CanIAccessDocumentQuery [L87]
    └─ handled_by Dataverse.ApplicationService.Queries.Documents.CanIAccessDocumentQueryHandler.Handle [L37–L82]
      └─ uses_service FirmSettingsService
        └─ method GetCurrentSettingsAsync [L66]
          └─ implementation Dataverse.ApplicationService.Services.FirmSettingsService.GetCurrentSettingsAsync [L18-L97]
      └─ uses_service UserService
        └─ method GetUserAsync [L61]
          └─ implementation Dataverse.ApplicationService.Services.UserService.GetUserAsync [L15-L185]
      └─ uses_service RequestInfoService
        └─ method IsValidServiceAccountRequest [L58]
          └─ implementation Dataverse.Services.Features.RequestInfoService.IsValidServiceAccountRequest [L11-L92]
          └─ implementation Dataverse.Services.Features.RequestInfoService.IsValidServiceAccountRequest [L11-L92]
          └─ implementation Dataverse.Services.Features.RequestInfoService.IsValidServiceAccountRequest [L11-L92]
      └─ uses_service IControlledRepository<Document>
        └─ method ReadQuery [L68]
          └─ ... (no implementation details available)

