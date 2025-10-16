[web] GET /api/ui/documents/documents/{id}/can-i-check-in  (Dataverse.Api.Controllers.UI.Documents.DocumentsController.CanICheckInDocument)  [L225–L232] status=200 [auth=Authentication.UserPolicy]
  └─ sends_request CanIAccessDocumentQuery [L228]
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
  └─ sends_request CanICheckInDocumentQuery [L229]
    └─ handled_by Dataverse.ApplicationService.Queries.Documents.CanICheckInDocumentQueryHandler.Handle [L29–L74]
      └─ uses_service UserService
        └─ method IsInRole [L47]
          └─ implementation Dataverse.ApplicationService.Services.UserService.IsInRole [L15-L185]
      └─ uses_service IControlledRepository<Document>
        └─ method ReadQuery [L49]
          └─ ... (no implementation details available)

