[web] GET /api/audit-trail/  (Cirrus.Api.Controllers.Firm.AuditHistoriesController.GetForAuditedEntity)  [L34–L44] status=200 [auth=Authentication.UserPolicy]
  └─ maps_to AuditHistoryDto [L37]
    └─ automapper.registration DataverseMappingProfile (AuditHistory->AuditHistoryDto) [L257]
    └─ automapper.registration CirrusMappingProfile (AuditHistory->AuditHistoryDto) [L187]
    └─ converts_to ExportAuditHistoryDto [L114]
  └─ calls AuditHistoryRepository.ReadQuery [L37]
  └─ query AuditHistory [L37]
    └─ reads_from AuditHistories
  └─ uses_service IControlledRepository<AuditHistory>
    └─ method ReadQuery [L37]
      └─ ... (no implementation details available)

