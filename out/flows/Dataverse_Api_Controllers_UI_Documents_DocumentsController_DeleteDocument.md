[web] DELETE /api/ui/documents/documents/{id:guid}/hard  (Dataverse.Api.Controllers.UI.Documents.DocumentsController.DeleteDocument)  [L343–L348] status=200 [auth=Authentication.UserPolicy,Authentication.AdminPolicy]
  └─ sends_request DeleteDocumentCommand [L347]
    └─ handled_by Dataverse.ApplicationService.Commands.Documents.DeleteDocumentCommandHandler.Handle [L22–L45]
      └─ uses_service IControlledRepository<Document>
        └─ method WriteQuery [L37]
          └─ ... (no implementation details available)
  └─ sends_request CanIAccessDocumentQuery [L346]
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

