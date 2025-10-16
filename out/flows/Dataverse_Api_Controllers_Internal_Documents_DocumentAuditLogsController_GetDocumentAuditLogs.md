[web] GET /api/internal/document-audit-trail/  (Dataverse.Api.Controllers.Internal.Documents.DocumentAuditLogsController.GetDocumentAuditLogs)  [L42–L55] status=200 [auth=Authentication.UserPolicy]
  └─ maps_to DocumentAuditLogDto [L49]
    └─ automapper.registration DataverseMappingProfile (DocumentAuditLog->DocumentAuditLogDto) [L445]
  └─ calls DocumentAuditLogRepository.ReadQuery [L49]
  └─ query DocumentAuditLog [L49]
    └─ reads_from DVS_DocumentAuditLogs
  └─ uses_service IControlledRepository<DocumentAuditLog>
    └─ method ReadQuery [L49]
      └─ ... (no implementation details available)
  └─ sends_request CanIAccessDocumentQuery [L47]
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

