[web] GET /api/ui/documents/documents/{id}  (Dataverse.Api.Controllers.UI.Documents.DocumentsController.GetDocument)  [L138–L147] status=200 [auth=Authentication.UserPolicy]
  └─ maps_to DocumentDto [L142]
    └─ automapper.registration DataverseMappingProfile (Document->DocumentDto) [L405]
  └─ calls DocumentRepository.ReadQuery [L142]
  └─ query Document [L142]
    └─ reads_from Documents
  └─ uses_service IControlledRepository<Document>
    └─ method ReadQuery [L142]
      └─ ... (no implementation details available)
  └─ sends_request CanIAccessDocumentQuery [L141]
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

