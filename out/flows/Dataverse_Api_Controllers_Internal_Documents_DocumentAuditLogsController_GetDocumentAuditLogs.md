[web] GET /api/internal/document-audit-trail/  (Dataverse.Api.Controllers.Internal.Documents.DocumentAuditLogsController.GetDocumentAuditLogs)  [L42–L55] [auth=Authentication.UserPolicy]
  └─ maps_to DocumentAuditLogDto [L49]
    └─ automapper.registration DataverseMappingProfile (DocumentAuditLog->DocumentAuditLogDto) [L443]
  └─ calls DocumentAuditLogRepository.ReadQuery [L49]
  └─ queries DocumentAuditLog [L49]
    └─ reads_from DVS_DocumentAuditLogs
  └─ uses_service IControlledRepository<DocumentAuditLog>
    └─ method ReadQuery [L49]
  └─ sends_request CanIAccessDocumentQuery [L47]
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

