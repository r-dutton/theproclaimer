[web] GET /api/audit-trail/  (Cirrus.Api.Controllers.Firm.AuditHistoriesController.GetForAuditedEntity)  [L34–L44] [auth=Authentication.UserPolicy]
  └─ maps_to AuditHistoryDto [L37]
    └─ automapper.registration DataverseMappingProfile (AuditHistory->AuditHistoryDto) [L256]
    └─ automapper.registration CirrusMappingProfile (AuditHistory->AuditHistoryDto) [L187]
    └─ converts_to ExportAuditHistoryDto [L114]
  └─ calls AuditHistoryRepository.ReadQuery [L37]
  └─ queries AuditHistory [L37]
    └─ reads_from AuditHistories
  └─ uses_service IControlledRepository<AuditHistory>
    └─ method ReadQuery [L37]

