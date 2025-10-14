[web] GET /api/internal/account-mapping/external-reporting-systems  (Dataverse.Api.Controllers.Internal.AccountMapping.ExternalReportingSystemsController.GetExternalReportingSystems)  [L45–L49] [auth=Authentication.MachineToMachinePolicy,Authentication.RequireTenantIdPolicy]
  └─ sends_request GetExternalReportingSystemsQuery [L48]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Dataverse.ApplicationService.Queries.AccountMapping.ExternalReportingSystems.GetExternalReportingSystemsQueryHandler.Handle [L29–L56]
      └─ maps_to ExternalReportingSystemLightDto [L52]
      └─ uses_service IControlledRepository<ExternalReportingSystem>
        └─ method ReadQuery [L44]
      └─ uses_service IMapper
        └─ method ConfigurationProvider [L53]
      └─ uses_service UserService
        └─ method IsMaster [L46]

