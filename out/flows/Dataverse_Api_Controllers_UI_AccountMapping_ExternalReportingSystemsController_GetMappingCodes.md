[web] GET /api/ui/account-mapping/external-reporting-systems/{id:guid}/mapping-codes  (Dataverse.Api.Controllers.UI.AccountMapping.ExternalReportingSystemsController.GetMappingCodes)  [L101–L106] [auth=Authentication.UserPolicy]
  └─ sends_request GetExternalReportingSystemMappingCodesQuery [L105]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Dataverse.ApplicationService.Queries.AccountMapping.ExternalReportingSystems.GetExternalReportingSystemMappingCodesQueryHandler.Handle [L33–L72]
      └─ maps_to ExternalReportingSystemMappingCodeDto [L54]
        └─ automapper.registration DataverseMappingProfile (ExternalReportingSystemMappingCode->ExternalReportingSystemMappingCodeDto) [L242]
      └─ uses_service IControlledRepository<ExternalReportingSystemMappingCode>
        └─ method ReadQuery [L54]
      └─ uses_service IMapper
        └─ method ConfigurationProvider [L57]
      └─ uses_service UserService
        └─ method IsMaster [L53]

