[web] GET /api/ui/documents/documents/{id}/meta-data  (Dataverse.Api.Controllers.UI.Documents.DocumentsController.GetMetaData)  [L169–L187] status=200 [auth=Authentication.UserPolicy]
  └─ calls DocumentVersionRepository.ReadQuery [L174]
  └─ query DocumentVersion [L174]
    └─ reads_from DocumentVersions
  └─ uses_service IControlledRepository<DocumentVersion>
    └─ method ReadQuery [L174]
      └─ ... (no implementation details available)
  └─ sends_request CanIAccessDocumentQuery [L172]
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

