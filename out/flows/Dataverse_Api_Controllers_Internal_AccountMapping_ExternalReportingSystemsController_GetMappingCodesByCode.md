[web] GET /api/internal/account-mapping/external-reporting-systems/mapping-codes  (Dataverse.Api.Controllers.Internal.AccountMapping.ExternalReportingSystemsController.GetMappingCodesByCode)  [L63–L68] status=200 [auth=Authentication.MachineToMachinePolicy,Authentication.RequireTenantIdPolicy]
  └─ sends_request GetExternalReportingSystemMappingCodesByCodeQuery [L66]
    └─ handled_by Dataverse.ApplicationService.Queries.AccountMapping.ExternalReportingSystems.GetExternalReportingSystemMappingCodesByCodeQueryHandler.Handle [L32–L61]
      └─ maps_to ExternalReportingSystemMappingCodeDto [L49]
        └─ automapper.registration DataverseMappingProfile (ExternalReportingSystemMappingCode->ExternalReportingSystemMappingCodeDto) [L243]
      └─ uses_service IControlledRepository<ExternalReportingSystemMappingCode>
        └─ method ReadQuery [L49]
          └─ ... (no implementation details available)
      └─ uses_service IMapper
        └─ method ConfigurationProvider [L52]
          └─ ... (no implementation details available)

