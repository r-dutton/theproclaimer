[web] GET /api/ui/account-mapping/external-reporting-systems/{id:guid}/mapping-codes/{mappingCodeId:guid}  (Dataverse.Api.Controllers.UI.AccountMapping.ExternalReportingSystemsController.GetMappingCode)  [L94–L99] status=200 [auth=Authentication.UserPolicy]
  └─ sends_request GetExternalReportingSystemMappingCodeQuery [L98]
    └─ handled_by Dataverse.ApplicationService.Queries.AccountMapping.ExternalReportingSystems.GetExternalReportingSystemMappingCodeQueryHandler.Handle [L30–L50]
      └─ maps_to ExternalReportingSystemMappingCodeDto [L46]
        └─ automapper.registration DataverseMappingProfile (ExternalReportingSystemMappingCode->ExternalReportingSystemMappingCodeDto) [L243]
      └─ uses_service IControlledRepository<ExternalReportingSystemMappingCode>
        └─ method ReadQuery [L46]
          └─ ... (no implementation details available)

