[web] GET /api/internal/account-mapping/external-reporting-systems/{id:guid}/mapping-codes/{mappingCodeId:guid}  (Dataverse.Api.Controllers.Internal.AccountMapping.ExternalReportingSystemsController.GetMappingCode)  [L51–L55] status=200 [auth=Authentication.MachineToMachinePolicy,Authentication.RequireTenantIdPolicy]
  └─ sends_request GetExternalReportingSystemMappingCodeQuery -> GetExternalReportingSystemMappingCodeQueryHandler [L54]
    └─ handled_by Dataverse.ApplicationService.Queries.AccountMapping.ExternalReportingSystems.GetExternalReportingSystemMappingCodeQueryHandler.Handle [L30–L50]
      └─ maps_to ExternalReportingSystemMappingCodeDto [L46]
        └─ automapper.registration DataverseMappingProfile (ExternalReportingSystemMappingCode->ExternalReportingSystemMappingCodeDto) [L243]
      └─ uses_service IControlledRepository<ExternalReportingSystemMappingCode> (Scoped (inferred))
        └─ method ReadQuery [L46]
          └─ implementation Dataverse.Data.Repository.AccountMapping.ExternalReportingSystemMappingCodeRepository.ReadQuery
  └─ impact_summary
    └─ requests 1
      └─ GetExternalReportingSystemMappingCodeQuery
    └─ handlers 1
      └─ GetExternalReportingSystemMappingCodeQueryHandler
    └─ mappings 1
      └─ ExternalReportingSystemMappingCodeDto

