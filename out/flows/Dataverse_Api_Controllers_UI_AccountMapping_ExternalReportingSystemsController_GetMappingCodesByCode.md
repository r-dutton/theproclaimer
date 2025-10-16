[web] GET /api/ui/account-mapping/external-reporting-systems/mapping-codes  (Dataverse.Api.Controllers.UI.AccountMapping.ExternalReportingSystemsController.GetMappingCodesByCode)  [L108–L114] status=200 [auth=Authentication.UserPolicy]
  └─ sends_request GetExternalReportingSystemMappingCodesByCodeQuery -> GetExternalReportingSystemMappingCodesByCodeQueryHandler [L112]
    └─ handled_by Dataverse.ApplicationService.Queries.AccountMapping.ExternalReportingSystems.GetExternalReportingSystemMappingCodesByCodeQueryHandler.Handle [L32–L61]
      └─ maps_to ExternalReportingSystemMappingCodeDto [L49]
        └─ automapper.registration DataverseMappingProfile (ExternalReportingSystemMappingCode->ExternalReportingSystemMappingCodeDto) [L243]
      └─ uses_service IControlledRepository<ExternalReportingSystemMappingCode> (Scoped (inferred))
        └─ method ReadQuery [L49]
          └─ implementation Dataverse.Data.Repository.AccountMapping.ExternalReportingSystemMappingCodeRepository.ReadQuery
  └─ impact_summary
    └─ requests 1
      └─ GetExternalReportingSystemMappingCodesByCodeQuery
    └─ handlers 1
      └─ GetExternalReportingSystemMappingCodesByCodeQueryHandler
    └─ mappings 1
      └─ ExternalReportingSystemMappingCodeDto

