[web] GET /api/ui/account-mapping/external-reporting-systems  (Dataverse.Api.Controllers.UI.AccountMapping.ExternalReportingSystemsController.GetExternalReportingSystems)  [L45–L50] status=200 [auth=Authentication.UserPolicy]
  └─ sends_request GetExternalReportingSystemsQuery [L49]
    └─ handled_by Dataverse.ApplicationService.Queries.AccountMapping.ExternalReportingSystems.GetExternalReportingSystemsQueryHandler.Handle [L29–L56]
      └─ maps_to ExternalReportingSystemLightDto [L52]
      └─ uses_service UserService
        └─ method IsMaster [L46]
          └─ implementation Dataverse.ApplicationService.Services.UserService.IsMaster [L15-L185]
      └─ uses_service IControlledRepository<ExternalReportingSystem>
        └─ method ReadQuery [L44]
          └─ ... (no implementation details available)
      └─ uses_service IMapper
        └─ method ConfigurationProvider [L53]
          └─ ... (no implementation details available)

