[web] GET /api/ui/account-mapping/external-reporting-systems/{id:guid}/mapping-codes  (Dataverse.Api.Controllers.UI.AccountMapping.ExternalReportingSystemsController.GetMappingCodes)  [L101–L106] status=200 [auth=Authentication.UserPolicy]
  └─ sends_request GetExternalReportingSystemMappingCodesQuery [L105]
    └─ handled_by Dataverse.ApplicationService.Queries.AccountMapping.ExternalReportingSystems.GetExternalReportingSystemMappingCodesQueryHandler.Handle [L33–L72]
      └─ maps_to ExternalReportingSystemMappingCodeDto [L54]
        └─ automapper.registration DataverseMappingProfile (ExternalReportingSystemMappingCode->ExternalReportingSystemMappingCodeDto) [L243]
      └─ uses_service UserService
        └─ method IsMaster [L53]
          └─ implementation Dataverse.ApplicationService.Services.UserService.IsMaster [L15-L185]
      └─ uses_service IControlledRepository<ExternalReportingSystemMappingCode>
        └─ method ReadQuery [L54]
          └─ ... (no implementation details available)
      └─ uses_service IMapper
        └─ method ConfigurationProvider [L57]
          └─ ... (no implementation details available)

