[web] DELETE /api/ui/documents/{documentId:guid}/versions/{documentVersionId:guid}  (Dataverse.Api.Controllers.UI.Documents.DocumentVersionsController.RecycleDocumentVersion)  [L91–L97] status=200 [auth=Authentication.UserPolicy]
  └─ sends_request RecycleDocumentVersionCommand [L96]
    └─ handled_by Dataverse.ApplicationService.Commands.Documents.RecycleDocumentVersionCommandHandler.Handle [L26–L51]
      └─ uses_service IControlledRepository<DocumentVersion>
        └─ method WriteQuery [L41]
          └─ ... (no implementation details available)
  └─ sends_request CanIAccessDocumentQuery [L95]
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

