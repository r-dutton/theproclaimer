[web] GET /api/ui/account-mapping/external-reporting-systems/{id:guid}  (Dataverse.Api.Controllers.UI.AccountMapping.ExternalReportingSystemsController.GetExternalReportingSystem)  [L32–L37] [auth=Authentication.UserPolicy]
  └─ sends_request GetExternalReportingSystemQuery [L36]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Dataverse.ApplicationService.Queries.AccountMapping.ExternalReportingSystems.GetExternalReportingSystemQueryHandler.Handle [L28–L47]
      └─ maps_to ExternalReportingSystemDto [L43]
        └─ automapper.registration DataverseMappingProfile (ExternalReportingSystem->ExternalReportingSystemDto) [L240]
      └─ uses_service IControlledRepository<ExternalReportingSystem>
        └─ method ReadQuery [L43]
      └─ uses_service UserService
        └─ method IsMaster [L44]

